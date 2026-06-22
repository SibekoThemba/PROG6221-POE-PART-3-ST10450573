using System;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CyberSecurityChatbot
{
    public partial class MainForm : Form
    {
        private DatabaseHelper db;
        private string userName;
        private SoundPlayer voicePlayer;
        private NLPSimulator nlp;
        private ActivityLogger logger;
        private TaskManager taskManager;
        private QuizManager quizManager;

        // Quiz variables
        private QuizQuestion[] quizQuestions;
        private int currentQuestionIndex = 0;
        private int quizScore = 0;

        public MainForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
            nlp = new NLPSimulator();
            logger = new ActivityLogger(db);
            taskManager = new TaskManager(db, logger);
            quizManager = new QuizManager(db, logger);

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Test database connection
            if (!db.TestConnection())
            {
                MessageBox.Show("Database connection failed!\n\nPlease check:\n1. SQL Server is running\n2. Database 'cyberchatbot_db' exists\n3. Connection string is correct",
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database connected successfully!");
            }

            // Style the UI
            StyleUI();

            // Play voice greeting
            PlayVoiceGreeting();

            // Display ASCII Art
            DisplayAsciiArt();

            // Show welcome message
            DisplayWelcomeMessage();

            // Setup tabs
            SetupTabs();

            // Display activity log
            RefreshActivityLog();

            // Show pending tasks
            RefreshTaskList();

            // Load quiz questions
            LoadQuizQuestions();
        }

        private void StyleUI()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            rtbChat.BackColor = Color.White;
            rtbChat.Font = new Font("Segoe UI", 10);
            rtbChat.BorderStyle = BorderStyle.None;

            txtUserInput.BackColor = Color.White;
            txtUserInput.Font = new Font("Segoe UI", 10);
            txtUserInput.BorderStyle = BorderStyle.FixedSingle;

            btnSend.BackColor = Color.FromArgb(67, 97, 238);
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            tabControlChatbot.BackColor = Color.FromArgb(240, 242, 245);
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = @"C:\ChatbotAudio\greeting.wav";
                if (System.IO.File.Exists(audioPath))
                {
                    voicePlayer = new SoundPlayer(audioPath);
                    voicePlayer.Play();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Voice greeting error: " + ex.Message);
            }
        }

        private void DisplayAsciiArt()
        {
            string asciiArt = @"
    ╔════════════════════════════════════════════════════════════╗
    ║                                                            ║
    ║    ╔═══════════════════════════════════════════════════╗   ║
    ║    ║               🔐  CYBER  🔐                      ║   ║
    ║    ║          ╔═══╗ ╔═══╗ ╔═══╗ ╔═══╗ ╔═══╗         ║   ║
    ║    ║          ║ C ║ ║ Y ║ ║ B ║ ║ E ║ ║ R ║         ║   ║
    ║    ║          ╚═══╝ ╚═══╝ ╚═══╝ ╚═══╝ ╚═══╝         ║   ║
    ║    ║          ╔═══╗ ╔═══╗ ╔═══╗ ╔═══╗ ╔═══╗ ╔═══╗   ║   ║
    ║    ║          ║ S ║ ║ E ║ ║ C ║ ║ U ║ ║ R ║ ║ E ║   ║   ║
    ║    ║          ╚═══╝ ╚═══╝ ╚═══╝ ╚═══╝ ╚═══╝ ╚═══╝   ║   ║
    ║    ║                                                   ║   ║
    ║    ║        🛡️ AWARENESS CHATBOT 🛡️                  ║   ║
    ║    ║          🔒 Stay Safe Online 🔒                   ║   ║
    ║    ╚═══════════════════════════════════════════════════╝   ║
    ║                                                            ║
    ╚════════════════════════════════════════════════════════════╝";

            txtAsciiArt.Text = asciiArt;
            txtAsciiArt.Font = new Font("Consolas", 8);
            txtAsciiArt.ForeColor = Color.Cyan;
            txtAsciiArt.BackColor = Color.FromArgb(10, 10, 20);
        }

        private void DisplayWelcomeMessage()
        {
            rtbChat.AppendText("\n" + new string('═', 60) + "\n");
            rtbChat.AppendText("🌐 Welcome to the Cybersecurity Awareness Chatbot\n");
            rtbChat.AppendText(new string('═', 60) + "\n\n");

            if (string.IsNullOrEmpty(userName))
            {
                rtbChat.AppendText("🤖: Hello! What is your name?\n");
                rtbChat.AppendText("👤: ");
            }
            else
            {
                rtbChat.AppendText($"🤖: Welcome back, {userName}! How can I help you today?\n");
                rtbChat.AppendText("🤖: You can ask me about:\n");
                rtbChat.AppendText("   • Cybersecurity tips (password, phishing, safe browsing)\n");
                rtbChat.AppendText("   • Add or manage tasks (click the Tasks tab)\n");
                rtbChat.AppendText("   • Take a cybersecurity quiz (click the Quiz tab)\n");
                rtbChat.AppendText("   • View your activity log (click the Activity Log tab)\n");
                rtbChat.AppendText("   • Type 'help' for more options\n");
            }
            rtbChat.AppendText("\n" + new string('─', 60) + "\n");
        }

        private void SetupTabs()
        {
            tabControlChatbot.SelectedIndex = 0;

            dgvTasks.AutoGenerateColumns = false;
            dgvTasks.Columns.Clear();

            // Add columns with DataPropertyName
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.Name = "task_id";
            colId.HeaderText = "ID";
            colId.DataPropertyName = "task_id";
            colId.Width = 50;
            dgvTasks.Columns.Add(colId);

            DataGridViewTextBoxColumn colTitle = new DataGridViewTextBoxColumn();
            colTitle.Name = "title";
            colTitle.HeaderText = "Title";
            colTitle.DataPropertyName = "title";
            colTitle.Width = 200;
            dgvTasks.Columns.Add(colTitle);

            DataGridViewTextBoxColumn colDesc = new DataGridViewTextBoxColumn();
            colDesc.Name = "description";
            colDesc.HeaderText = "Description";
            colDesc.DataPropertyName = "description";
            colDesc.Width = 250;
            dgvTasks.Columns.Add(colDesc);

            DataGridViewTextBoxColumn colReminder = new DataGridViewTextBoxColumn();
            colReminder.Name = "reminder_date";
            colReminder.HeaderText = "Reminder";
            colReminder.DataPropertyName = "reminder_date";
            colReminder.Width = 150;
            dgvTasks.Columns.Add(colReminder);

            DataGridViewCheckBoxColumn colDone = new DataGridViewCheckBoxColumn();
            colDone.Name = "is_completed";
            colDone.HeaderText = "Done";
            colDone.DataPropertyName = "is_completed";
            colDone.Width = 60;
            dgvTasks.Columns.Add(colDone);
        }

        // =====================================================
        // CHAT TAB
        // =====================================================

        private void btnSend_Click(object sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessUserInput();
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessUserInput()
        {
            string input = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            rtbChat.AppendText($"👤: {input}\n");
            txtUserInput.Clear();

            if (string.IsNullOrEmpty(userName) && !input.StartsWith("My name is", StringComparison.OrdinalIgnoreCase))
            {
                rtbChat.AppendText($"🤖: Please tell me your name by typing: My name is [YourName]\n");
                return;
            }

            if (input.StartsWith("My name is", StringComparison.OrdinalIgnoreCase))
            {
                userName = input.Substring(10).Trim();
                rtbChat.AppendText($"🤖: Nice to meet you, {userName}! I'm your Cybersecurity Awareness Assistant.\n");
                rtbChat.AppendText("🤖: You can ask me questions or use the tabs above for more features.\n");
                logger.LogActivity(userName, "Login", $"User '{userName}' logged in");
                RefreshActivityLog();
                return;
            }

            string response = nlp.ProcessInput(input, userName, taskManager);
            rtbChat.AppendText($"🤖: {response}\n");
            rtbChat.ScrollToCaret();

            logger.LogActivity(userName, "Chat", $"User said: {input}");
            RefreshActivityLog();
        }

        // =====================================================
        // TASK TAB
        // =====================================================

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDesc.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? reminderDate = null;
            if (chkReminder.Checked)
            {
                reminderDate = dtpReminder.Value;
            }

            if (taskManager.AddTask(userName, title, description, reminderDate))
            {
                RefreshTaskList();
                txtTaskTitle.Clear();
                txtTaskDesc.Clear();
                chkReminder.Checked = false;
                dtpReminder.Enabled = false;
                RefreshActivityLog();
                MessageBox.Show("Task added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add task.\n\nCheck:\n1. SQL Server is running\n2. Database exists\n3. Connection string is correct",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to complete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["task_id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["title"].Value.ToString();

            if (taskManager.CompleteTask(taskId))
            {
                RefreshTaskList();
                logger.LogActivity(userName, "Task_Completed", $"Completed task: '{title}'");
                RefreshActivityLog();
                MessageBox.Show($"Task '{title}' marked as completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to complete task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a task to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["task_id"].Value);
            string title = dgvTasks.SelectedRows[0].Cells["title"].Value.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete task '{title}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (taskManager.DeleteTask(taskId))
                {
                    RefreshTaskList();
                    logger.LogActivity(userName, "Task_Deleted", $"Deleted task: '{title}'");
                    RefreshActivityLog();
                    MessageBox.Show($"Task '{title}' deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkShowCompleted_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTaskList();
        }

        private void chkReminder_CheckedChanged(object sender, EventArgs e)
        {
            dtpReminder.Enabled = chkReminder.Checked;
        }

        private void RefreshTaskList()
        {
            if (string.IsNullOrEmpty(userName))
                return;

            bool showCompleted = chkShowCompleted.Checked;
            DataTable tasks = db.GetTasks(userName, showCompleted);

            if (tasks.Rows.Count == 0)
            {
                dgvTasks.DataSource = null;
                lblTaskCount.Text = "Total: 0 tasks";
                return;
            }

            dgvTasks.DataSource = tasks;
            lblTaskCount.Text = $"Total: {tasks.Rows.Count} task(s)";
        }

        // =====================================================
        // QUIZ TAB
        // =====================================================

        private void LoadQuizQuestions()
        {
            quizQuestions = new QuizQuestion[]
            {
                new QuizQuestion("What is the most common type of cyber attack?",
                    new string[] { "A) Phishing", "B) Ransomware", "C) DDoS", "D) Man-in-the-Middle" }, 0,
                    "Phishing is the most common type of cyber attack."),
                new QuizQuestion("What should you do if you receive a suspicious email?",
                    new string[] { "A) Reply to ask who sent it", "B) Click the link to check", "C) Report it as phishing", "D) Forward it to friends" }, 2,
                    "You should report suspicious emails as phishing."),
                new QuizQuestion("Which of the following is a strong password?",
                    new string[] { "A) 123456", "B) password", "C) P@ssw0rd!2024", "D) yourname" }, 2,
                    "A strong password contains uppercase, lowercase, numbers, and special characters."),
                new QuizQuestion("True or False: Using the same password for multiple accounts is safe.",
                    new string[] { "A) True", "B) False" }, 1,
                    "Using the same password for multiple accounts is dangerous."),
                new QuizQuestion("What is two-factor authentication (2FA)?",
                    new string[] { "A) A password manager", "B) A second layer of security", "C) A type of antivirus", "D) A VPN service" }, 1,
                    "2FA adds a second layer of security."),
                new QuizQuestion("What is social engineering in cybersecurity?",
                    new string[] { "A) Engineering software", "B) Manipulating people to reveal information", "C) Building social networks", "D) Creating engineering designs" }, 1,
                    "Social engineering is the psychological manipulation of people."),
                new QuizQuestion("True or False: Public Wi-Fi is always safe to use for online banking.",
                    new string[] { "A) True", "B) False" }, 1,
                    "Public Wi-Fi is often unsecured and can be intercepted."),
                new QuizQuestion("What should you do with software updates?",
                    new string[] { "A) Ignore them", "B) Install them immediately", "C) Wait a few months", "D) Only install paid updates" }, 1,
                    "Software updates often contain security patches."),
                new QuizQuestion("What is ransomware?",
                    new string[] { "A) Free software", "B) Software that holds your data hostage", "C) A type of antivirus", "D) A password manager" }, 1,
                    "Ransomware is malicious software that encrypts your files."),
                new QuizQuestion("True or False: Phishing attacks only happen via email.",
                    new string[] { "A) True", "B) False" }, 1,
                    "Phishing can happen via email, SMS, phone calls, social media, and QR codes."),
                new QuizQuestion("What is the best defense against phishing?",
                    new string[] { "A) Antivirus", "B) Firewall", "C) User awareness", "D) VPN" }, 2,
                    "User awareness is the best defense against phishing."),
                new QuizQuestion("True or False: HTTPS websites are always safe.",
                    new string[] { "A) True", "B) False" }, 1,
                    "HTTPS encrypts data, but the website itself could still be malicious."),
                new QuizQuestion("What is a zero-day vulnerability?",
                    new string[] { "A) A vulnerability that never existed", "B) A vulnerability discovered before a patch exists", "C) A vulnerability fixed immediately", "D) A vulnerability on Day 0 of a project" }, 1,
                    "A zero-day vulnerability is a flaw discovered before a patch exists."),
                new QuizQuestion("Why should you use a password manager?",
                    new string[] { "A) It remembers all your passwords", "B) It creates strong passwords", "C) It saves you time", "D) All of the above" }, 3,
                    "Password managers generate, store, and autofill strong passwords."),
                new QuizQuestion("True or False: It is safe to use the same password for work and personal accounts.",
                    new string[] { "A) True", "B) False" }, 1,
                    "Never reuse passwords between work and personal accounts.")
            };
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            currentQuestionIndex = 0;
            quizScore = 0;
            btnStartQuiz.Enabled = false;
            btnNextQuestion.Enabled = true;
            btnSubmitQuiz.Enabled = false;
            rtbQuizFeedback.Clear();
            lblQuizScore.Text = "Score: 0 / " + quizQuestions.Length;
            LoadQuestion();
            logger.LogActivity(userName, "Quiz_Started", "User started the cybersecurity quiz");
            RefreshActivityLog();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Length)
            {
                FinishQuiz();
                return;
            }

            var q = quizQuestions[currentQuestionIndex];
            lblQuestion.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Length}";
            rtbQuestion.Text = q.Question;

            panelOptions.Controls.Clear();
            int radioCount = 0;
            foreach (string option in q.Options)
            {
                RadioButton rb = new RadioButton();
                rb.Text = option;
                rb.Tag = radioCount;
                rb.Location = new Point(10, radioCount * 30);
                rb.AutoSize = true;
                rb.Width = 450;
                panelOptions.Controls.Add(rb);
                radioCount++;
            }

            rtbQuizFeedback.Clear();
            rtbQuizFeedback.BackColor = Color.White;
            btnNextQuestion.Enabled = true;
        }

        private void btnNextQuestion_Click(object sender, EventArgs e)
        {
            RadioButton selected = null;
            foreach (Control ctrl in panelOptions.Controls)
            {
                if (ctrl is RadioButton rb && rb.Checked)
                {
                    selected = rb;
                    break;
                }
            }

            if (selected == null)
            {
                MessageBox.Show("Please select an answer before continuing.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedIndex = Convert.ToInt32(selected.Tag);
            var q = quizQuestions[currentQuestionIndex];

            if (selectedIndex == q.CorrectAnswerIndex)
            {
                quizScore++;
                rtbQuizFeedback.BackColor = Color.LightGreen;
                rtbQuizFeedback.Text = "✅ Correct! " + q.Explanation;
                logger.LogActivity(userName, "Quiz_Answer", $"Correct answer for Q{currentQuestionIndex + 1}");
            }
            else
            {
                rtbQuizFeedback.BackColor = Color.LightPink;
                rtbQuizFeedback.Text = "❌ Incorrect. " + q.Explanation;
                logger.LogActivity(userName, "Quiz_Answer", $"Incorrect answer for Q{currentQuestionIndex + 1}");
            }
            RefreshActivityLog();

            currentQuestionIndex++;
            lblQuizScore.Text = $"Score: {quizScore} / {quizQuestions.Length}";

            if (currentQuestionIndex >= quizQuestions.Length)
            {
                btnNextQuestion.Enabled = false;
                btnSubmitQuiz.Enabled = true;
            }
            else
            {
                btnNextQuestion.Enabled = true;
                LoadQuestion();
            }
        }

        private void btnSubmitQuiz_Click(object sender, EventArgs e)
        {
            FinishQuiz();
        }

        private void FinishQuiz()
        {
            btnStartQuiz.Enabled = true;
            btnNextQuestion.Enabled = false;
            btnSubmitQuiz.Enabled = false;

            double percentage = ((double)quizScore / quizQuestions.Length) * 100;
            string feedback = percentage >= 80 ? "🌟 Great job! You're a cybersecurity pro!" :
                              percentage >= 60 ? "👍 Good effort! Keep learning!" :
                              "📚 Keep studying! Cybersecurity is important for everyone.";

            rtbQuizFeedback.BackColor = Color.LightYellow;
            rtbQuizFeedback.Text = $"🏆 Quiz Complete!\n\nFinal Score: {quizScore} / {quizQuestions.Length} ({percentage:F1}%)\n\n{feedback}";
            lblQuizScore.Text = $"Score: {quizScore} / {quizQuestions.Length}";

            db.SaveQuizResult(userName, quizScore, quizQuestions.Length);
            logger.LogActivity(userName, "Quiz_Completed", $"Quiz completed with score {quizScore}/{quizQuestions.Length}");
            RefreshActivityLog();
        }

        // =====================================================
        // ACTIVITY LOG TAB
        // =====================================================

        private void RefreshActivityLog()
        {
            if (string.IsNullOrEmpty(userName))
                return;

            LoadActivityLog();
        }

        private void LoadActivityLog()
        {
            DataTable logs = db.GetActivityLog(userName, 10);
            dgvActivityLog.DataSource = logs;
            if (logs.Rows.Count > 0)
            {
                lblLogCount.Text = $"Showing {logs.Rows.Count} activity entries";
            }
            else
            {
                lblLogCount.Text = "No activity logs found";
            }
        }

        private void btnRefreshLog_Click(object sender, EventArgs e)
        {
            RefreshActivityLog();
        }

        private void btnShowMore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userName))
                return;

            DataTable logs = db.GetFullActivityLog(userName);
            dgvActivityLog.DataSource = logs;
            lblLogCount.Text = $"Showing all {logs.Rows.Count} activity entries";
        }

        // =====================================================
        // QUIZ QUESTION CLASS
        // =====================================================

        public class QuizQuestion
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public int CorrectAnswerIndex { get; set; }
            public string Explanation { get; set; }

            public QuizQuestion(string question, string[] options, int correctIndex, string explanation)
            {
                Question = question;
                Options = options;
                CorrectAnswerIndex = correctIndex;
                Explanation = explanation;
            }
        }
    }
}