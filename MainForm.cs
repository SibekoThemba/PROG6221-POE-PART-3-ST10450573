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
        private int totalQuestions = 15;
        private int questionsAnswered = 0;
        private bool[] questionAnsweredCorrectly;

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

            // Initialize quiz UI
            InitializeQuizUI();
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
    ╔══════════════════════════════════════════════════════════════════════════╗
    ║                                                                          ║
    ║                          🔐  C H A T B O X  🔐                          ║
    ║                                                                          ║
    ║              ╔═══════════════════════════════════════════════╗            ║
    ║              ║       🛡️  CYBERSECURITY AWARENESS  🛡️       ║            ║
    ║              ║           🔒  Stay Safe Online  🔒           ║            ║
    ║              ╚═══════════════════════════════════════════════╝            ║
    ║                                                                          ║
    ║          Your Digital Safety Companion - Ask me anything about security!  ║
    ║                                                                          ║
    ╚══════════════════════════════════════════════════════════════════════════╝";

            txtAsciiArt.Text = asciiArt;
            txtAsciiArt.Font = new Font("Consolas", 10, FontStyle.Bold);
            txtAsciiArt.ForeColor = Color.Cyan;
            txtAsciiArt.BackColor = Color.FromArgb(10, 10, 30);
            txtAsciiArt.TextAlign = HorizontalAlignment.Center;
            txtAsciiArt.Size = new Size(970, 140);
        }

        private void DisplayWelcomeMessage()
        {
            rtbChat.Clear();
            rtbChat.AppendText("\n" + new string('═', 60) + "\n");
            rtbChat.AppendText("  🌐  Welcome to the Cybersecurity Awareness Chatbot  🌐\n");
            rtbChat.AppendText(new string('═', 60) + "\n\n");

            if (string.IsNullOrEmpty(userName))
            {
                rtbChat.AppendText("🤖: Hello! Welcome to the ChatBox. What is your name?\n");
                rtbChat.AppendText("👤: ");
            }
            else
            {
                rtbChat.AppendText($"🤖: Welcome back to the ChatBox, {userName}! 👋\n\n");
                rtbChat.AppendText("🤖: I can help you with:\n");
                rtbChat.AppendText("   • 🔐 Cybersecurity tips (password, phishing, safe browsing)\n");
                rtbChat.AppendText("   • 📋 Add or manage tasks (click the Tasks tab)\n");
                rtbChat.AppendText("   • 🎯 Take a cybersecurity quiz (click the Quiz tab)\n");
                rtbChat.AppendText("   • 📜 View your activity log (click the Activity Log tab)\n");
                rtbChat.AppendText("   • 💬 Type 'help' for more options\n");
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
                rtbChat.AppendText($"🤖: Nice to meet you, {userName}! Welcome to the ChatBox.\n");
                rtbChat.AppendText("🤖: I'm your Cybersecurity Awareness Assistant.\n");
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

            try
            {
                DateTime? reminderDate = null;
                if (chkReminder.Checked)
                {
                    reminderDate = dtpReminder.Value;
                }

                bool result = taskManager.AddTask(userName, title, description, reminderDate);

                if (result)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // QUIZ TAB - IMPROVED VERSION
        // =====================================================

        private void InitializeQuizUI()
        {
            lblQuestion.Text = "🎯 Ready to test your cybersecurity knowledge?";
            rtbQuestion.Text = "Click 'Start Quiz' to begin! You'll answer 15 questions covering:\n\n• Password Safety\n• Phishing Prevention\n• Social Engineering\n• Malware Protection\n• Safe Browsing\n• And more!";
            rtbQuestion.Font = new Font("Segoe UI", 11);
            rtbQuestion.ForeColor = Color.FromArgb(50, 50, 60);
            rtbQuestion.BackColor = Color.FromArgb(245, 245, 250);
            lblQuizScore.Text = "Score: 0 / 15";
            lblQuizScore.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuizScore.ForeColor = Color.FromArgb(67, 97, 238);
        }

        private void LoadQuizQuestions()
        {
            quizQuestions = new QuizQuestion[]
            {
                // 15 comprehensive cybersecurity questions covering all topics
                new QuizQuestion(
                    "What is the most common type of cyber attack?",
                    new string[] { "A) Phishing", "B) Ransomware", "C) DDoS", "D) Man-in-the-Middle" },
                    0,
                    "📌 Phishing is the most common type of cyber attack, where attackers trick users into revealing sensitive information. According to cybersecurity reports, over 80% of reported security incidents involve phishing."
                ),
                new QuizQuestion(
                    "What should you do if you receive a suspicious email?",
                    new string[] { "A) Reply to ask who sent it", "B) Click the link to check", "C) Report it as phishing", "D) Forward it to friends" },
                    2,
                    "📌 Report suspicious emails as phishing. Do NOT click links or reply. Most email providers have a 'Report Phishing' button. This helps protect you and others from scams."
                ),
                new QuizQuestion(
                    "Which of the following is a strong password?",
                    new string[] { "A) 123456", "B) password", "C) P@ssw0rd!2024", "D) yourname" },
                    2,
                    "📌 A strong password contains uppercase, lowercase, numbers, and special characters. 'P@ssw0rd!2024' is much stronger than common words or number sequences."
                ),
                new QuizQuestion(
                    "True or False: Using the same password for multiple accounts is safe.",
                    new string[] { "A) True", "B) False" },
                    1,
                    "📌 FALSE! Using the same password for multiple accounts is very dangerous. If one account is compromised, all others become vulnerable. Use unique passwords for each account."
                ),
                new QuizQuestion(
                    "What is two-factor authentication (2FA)?",
                    new string[] { "A) A password manager", "B) A second layer of security", "C) A type of antivirus", "D) A VPN service" },
                    1,
                    "📌 2FA adds a second layer of security, typically requiring a code from your phone or authenticator app in addition to your password. It significantly reduces the risk of unauthorised access."
                ),
                new QuizQuestion(
                    "What is social engineering in cybersecurity?",
                    new string[] { "A) Engineering software", "B) Manipulating people to reveal information", "C) Building social networks", "D) Creating engineering designs" },
                    1,
                    "📌 Social engineering is the psychological manipulation of people to divulge confidential information. It exploits human trust rather than technical vulnerabilities."
                ),
                new QuizQuestion(
                    "True or False: Public Wi-Fi is always safe to use for online banking.",
                    new string[] { "A) True", "B) False" },
                    1,
                    "📌 FALSE! Public Wi-Fi is often unsecured and can be intercepted by attackers. Always use a VPN or mobile data for sensitive transactions on public networks."
                ),
                new QuizQuestion(
                    "What should you do with software updates?",
                    new string[] { "A) Ignore them", "B) Install them immediately", "C) Wait a few months", "D) Only install paid updates" },
                    1,
                    "📌 Install software updates immediately! They often contain critical security patches that fix known vulnerabilities. Delaying updates leaves your devices exposed."
                ),
                new QuizQuestion(
                    "What is ransomware?",
                    new string[] { "A) Free software", "B) Software that holds your data hostage", "C) A type of antivirus", "D) A password manager" },
                    1,
                    "📌 Ransomware is malicious software that encrypts your files and demands payment for decryption. Never pay the ransom! Instead, maintain regular backups of your important data."
                ),
                new QuizQuestion(
                    "True or False: Phishing attacks only happen via email.",
                    new string[] { "A) True", "B) False" },
                    1,
                    "📌 FALSE! Phishing can happen via email, SMS (smishing), phone calls (vishing), social media, and even QR codes. Always be suspicious of unexpected requests for information."
                ),
                new QuizQuestion(
                    "What is the best defense against phishing?",
                    new string[] { "A) Antivirus", "B) Firewall", "C) User awareness", "D) VPN" },
                    2,
                    "📌 User awareness is the best defense against phishing. Always verify the source before clicking links or sharing information. Technology helps, but human vigilance is the strongest protection."
                ),
                new QuizQuestion(
                    "True or False: HTTPS websites are always safe.",
                    new string[] { "A) True", "B) False" },
                    1,
                    "📌 FALSE! HTTPS encrypts data in transit, but the website itself could still be malicious. Always check the URL carefully and look for signs of phishing or fake sites."
                ),
                new QuizQuestion(
                    "What is a zero-day vulnerability?",
                    new string[] { "A) A vulnerability that never existed", "B) A vulnerability discovered before a patch exists", "C) A vulnerability fixed immediately", "D) A vulnerability on Day 0 of a project" },
                    1,
                    "📌 A zero-day vulnerability is a flaw discovered before the vendor has created a patch, making it very dangerous. Attackers can exploit it before it's fixed."
                ),
                new QuizQuestion(
                    "Why should you use a password manager?",
                    new string[] { "A) It remembers all your passwords", "B) It creates strong passwords", "C) It saves you time", "D) All of the above" },
                    3,
                    "📌 All of the above! Password managers generate, store, and autofill strong, unique passwords for all your accounts. This makes security easier and more effective."
                ),
                new QuizQuestion(
                    "True or False: It is safe to use the same password for work and personal accounts.",
                    new string[] { "A) True", "B) False" },
                    1,
                    "📌 FALSE! Never reuse passwords between work and personal accounts. A breach in one could compromise the other. Keep them separate for better security."
                )
            };
            totalQuestions = quizQuestions.Length;
            questionAnsweredCorrectly = new bool[totalQuestions];
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            currentQuestionIndex = 0;
            quizScore = 0;
            questionsAnswered = 0;
            Array.Clear(questionAnsweredCorrectly, 0, questionAnsweredCorrectly.Length);
            btnStartQuiz.Enabled = false;
            btnNextQuestion.Enabled = true;
            btnSubmitQuiz.Enabled = false;
            rtbQuizFeedback.Clear();
            rtbQuizFeedback.BackColor = Color.White;
            lblQuizScore.Text = "Score: 0 / " + totalQuestions;
            lblQuizScore.ForeColor = Color.FromArgb(67, 97, 238);
            LoadQuestion();
            logger.LogActivity(userName, "Quiz_Started", "User started the cybersecurity quiz");
            RefreshActivityLog();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= totalQuestions)
            {
                FinishQuiz();
                return;
            }

            var q = quizQuestions[currentQuestionIndex];
            int questionNumber = currentQuestionIndex + 1;
            lblQuestion.Text = $"📝 Question {questionNumber} of {totalQuestions}";
            lblQuestion.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuestion.ForeColor = Color.FromArgb(50, 50, 60);

            // Show progress in question text
            rtbQuestion.Text = q.Question;
            rtbQuestion.Font = new Font("Segoe UI", 11);
            rtbQuestion.ForeColor = Color.FromArgb(40, 40, 50);
            rtbQuestion.BackColor = Color.FromArgb(245, 245, 250);

            // Clear and populate radio buttons with improved styling
            panelOptions.Controls.Clear();
            int radioCount = 0;
            foreach (string option in q.Options)
            {
                RadioButton rb = new RadioButton();
                rb.Text = option;
                rb.Tag = radioCount;
                rb.Location = new Point(15, radioCount * 35);
                rb.Size = new Size(460, 30);
                rb.Font = new Font("Segoe UI", 10);
                rb.AutoSize = false;
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
            questionsAnswered++;

            // Check answer and provide detailed feedback
            bool isCorrect = (selectedIndex == q.CorrectAnswerIndex);
            if (isCorrect)
            {
                quizScore++;
                questionAnsweredCorrectly[currentQuestionIndex] = true;
                rtbQuizFeedback.BackColor = Color.FromArgb(200, 255, 200); // Light green
                rtbQuizFeedback.Text = "✅ CORRECT! ";
                logger.LogActivity(userName, "Quiz_Answer", $"Correct answer for Q{currentQuestionIndex + 1}");
            }
            else
            {
                questionAnsweredCorrectly[currentQuestionIndex] = false;
                rtbQuizFeedback.BackColor = Color.FromArgb(255, 200, 200); // Light pink
                rtbQuizFeedback.Text = "❌ INCORRECT. ";
                logger.LogActivity(userName, "Quiz_Answer", $"Incorrect answer for Q{currentQuestionIndex + 1}");
            }

            // Show the explanation with the correct answer highlighted
            string correctAnswer = q.Options[q.CorrectAnswerIndex];
            rtbQuizFeedback.Text += $"\n\n📖 Explanation:\n{q.Explanation}\n\n";
            rtbQuizFeedback.Text += $"💡 The correct answer was: {correctAnswer}";
            rtbQuizFeedback.Font = new Font("Segoe UI", 10);
            rtbQuizFeedback.ForeColor = Color.FromArgb(30, 30, 40);

            // Update score with progress
            lblQuizScore.Text = $"Score: {quizScore} / {totalQuestions}";

            // Show progress bar (text-based)
            int progress = (int)((double)(currentQuestionIndex + 1) / totalQuestions * 100);
            string progressBar = new string('▓', progress / 5) + new string('░', 20 - (progress / 5));
            lblQuizScore.Text += $"  📊 [{progressBar}] {progress}%";

            RefreshActivityLog();
            currentQuestionIndex++;

            if (currentQuestionIndex >= totalQuestions)
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

            double percentage = ((double)quizScore / totalQuestions) * 100;

            // Detailed feedback based on score
            string feedback;
            if (percentage >= 90)
            {
                feedback = "🌟 EXCELLENT! You're a cybersecurity expert! Your knowledge of online safety is outstanding. Keep up the great work and help others stay safe too!";
            }
            else if (percentage >= 80)
            {
                feedback = "🌟 GREAT JOB! You have a strong understanding of cybersecurity. You're well-prepared to protect yourself online. Consider sharing your knowledge with friends and family.";
            }
            else if (percentage >= 60)
            {
                feedback = "👍 GOOD EFFORT! You have a solid foundation in cybersecurity. Review the topics you missed to strengthen your knowledge. Remember, security is a continuous learning process!";
            }
            else if (percentage >= 40)
            {
                feedback = "📚 KEEP LEARNING! You've made a good start. Focus on understanding phishing, password safety, and social engineering - these are the most common threats you'll face online.";
            }
            else
            {
                feedback = "📚 START WITH THE BASICS! Cybersecurity is important for everyone. Review the tips section of this chatbot to learn more about protecting yourself online. You can retake the quiz anytime!";
            }

            // Show detailed results
            string resultSummary = $"🏆 QUIZ COMPLETE!\n\n";
            resultSummary += $"Final Score: {quizScore} / {totalQuestions} ({percentage:F1}%)\n\n";
            resultSummary += $"📊 Performance Summary:\n";
            resultSummary += $"   • Correct: {quizScore}\n";
            resultSummary += $"   • Incorrect: {totalQuestions - quizScore}\n\n";
            resultSummary += $"💡 {feedback}\n\n";

            // Show question breakdown
            resultSummary += "📋 Question Breakdown:\n";
            for (int i = 0; i < totalQuestions; i++)
            {
                string status = questionAnsweredCorrectly[i] ? "✅" : "❌";
                resultSummary += $"   {status} Question {i + 1}: {quizQuestions[i].Question.Substring(0, Math.Min(40, quizQuestions[i].Question.Length))}...\n";
            }

            rtbQuizFeedback.BackColor = Color.FromArgb(255, 255, 220); // Light yellow
            rtbQuizFeedback.Text = resultSummary;
            rtbQuizFeedback.Font = new Font("Segoe UI", 10);
            rtbQuizFeedback.ForeColor = Color.FromArgb(30, 30, 40);

            lblQuizScore.Text = $"🏆 Final Score: {quizScore} / {totalQuestions}";
            lblQuizScore.ForeColor = percentage >= 70 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(220, 53, 69);

            db.SaveQuizResult(userName, quizScore, totalQuestions);
            logger.LogActivity(userName, "Quiz_Completed", $"Quiz completed with score {quizScore}/{totalQuestions}");
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