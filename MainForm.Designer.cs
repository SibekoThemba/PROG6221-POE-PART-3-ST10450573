namespace CyberSecurityChatbot
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlChatbot;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.TabPage tabQuiz;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtAsciiArt;

        // Task Tab Controls
        private System.Windows.Forms.Label lblTaskTitle;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.Label lblTaskDesc;
        private System.Windows.Forms.TextBox txtTaskDesc;
        private System.Windows.Forms.Label lblReminder;
        private System.Windows.Forms.DateTimePicker dtpReminder;
        private System.Windows.Forms.CheckBox chkReminder;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnCompleteTask;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.CheckBox chkShowCompleted;
        private System.Windows.Forms.Label lblTaskCount;

        // Quiz Tab Controls
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RichTextBox rtbQuestion;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.RichTextBox rtbQuizFeedback;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.Button btnNextQuestion;
        private System.Windows.Forms.Button btnSubmitQuiz;
        private System.Windows.Forms.Label lblQuizScore;

        // Activity Log Tab Controls
        private System.Windows.Forms.DataGridView dgvActivityLog;
        private System.Windows.Forms.Button btnRefreshLog;
        private System.Windows.Forms.Button btnShowMore;
        private System.Windows.Forms.Label lblLogCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlChatbot = new System.Windows.Forms.TabControl();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.txtAsciiArt = new System.Windows.Forms.TextBox();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.lblTaskCount = new System.Windows.Forms.Label();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnCompleteTask = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.dtpReminder = new System.Windows.Forms.DateTimePicker();
            this.chkReminder = new System.Windows.Forms.CheckBox();
            this.txtTaskDesc = new System.Windows.Forms.TextBox();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.lblTaskDesc = new System.Windows.Forms.Label();
            this.lblReminder = new System.Windows.Forms.Label();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.chkShowCompleted = new System.Windows.Forms.CheckBox();
            this.tabQuiz = new System.Windows.Forms.TabPage();
            this.lblQuizScore = new System.Windows.Forms.Label();
            this.btnSubmitQuiz = new System.Windows.Forms.Button();
            this.btnNextQuestion = new System.Windows.Forms.Button();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.rtbQuizFeedback = new System.Windows.Forms.RichTextBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.rtbQuestion = new System.Windows.Forms.RichTextBox();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.btnShowMore = new System.Windows.Forms.Button();
            this.btnRefreshLog = new System.Windows.Forms.Button();
            this.dgvActivityLog = new System.Windows.Forms.DataGridView();

            // tabControlChatbot
            this.tabControlChatbot.Controls.Add(this.tabChat);
            this.tabControlChatbot.Controls.Add(this.tabTasks);
            this.tabControlChatbot.Controls.Add(this.tabQuiz);
            this.tabControlChatbot.Controls.Add(this.tabLog);
            this.tabControlChatbot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlChatbot.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControlChatbot.Location = new System.Drawing.Point(0, 0);
            this.tabControlChatbot.Name = "tabControlChatbot";
            this.tabControlChatbot.Size = new System.Drawing.Size(1000, 700);
            this.tabControlChatbot.TabIndex = 0;

            // tabChat
            this.tabChat.Controls.Add(this.txtAsciiArt);
            this.tabChat.Controls.Add(this.rtbChat);
            this.tabChat.Controls.Add(this.txtUserInput);
            this.tabChat.Controls.Add(this.btnSend);
            this.tabChat.Text = "💬 Chat";
            this.tabChat.UseVisualStyleBackColor = true;

            // txtAsciiArt
            this.txtAsciiArt.BackColor = System.Drawing.Color.FromArgb(10, 10, 20);
            this.txtAsciiArt.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtAsciiArt.ForeColor = System.Drawing.Color.Cyan;
            this.txtAsciiArt.Location = new System.Drawing.Point(10, 10);
            this.txtAsciiArt.Multiline = true;
            this.txtAsciiArt.Name = "txtAsciiArt";
            this.txtAsciiArt.ReadOnly = true;
            this.txtAsciiArt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAsciiArt.Size = new System.Drawing.Size(970, 180);
            this.txtAsciiArt.TabIndex = 3;

            // rtbChat
            this.rtbChat.BackColor = System.Drawing.Color.White;
            this.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtbChat.Location = new System.Drawing.Point(10, 200);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(850, 400);
            this.rtbChat.TabIndex = 0;

            // txtUserInput
            this.txtUserInput.BackColor = System.Drawing.Color.White;
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserInput.Location = new System.Drawing.Point(10, 610);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(750, 30);
            this.txtUserInput.TabIndex = 1;
            this.txtUserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserInput_KeyDown);

            // btnSend
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(770, 605);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 35);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // tabTasks
            this.tabTasks.Controls.Add(this.lblTaskCount);
            this.tabTasks.Controls.Add(this.dgvTasks);
            this.tabTasks.Controls.Add(this.btnDeleteTask);
            this.tabTasks.Controls.Add(this.btnCompleteTask);
            this.tabTasks.Controls.Add(this.btnAddTask);
            this.tabTasks.Controls.Add(this.dtpReminder);
            this.tabTasks.Controls.Add(this.chkReminder);
            this.tabTasks.Controls.Add(this.txtTaskDesc);
            this.tabTasks.Controls.Add(this.txtTaskTitle);
            this.tabTasks.Controls.Add(this.lblTaskDesc);
            this.tabTasks.Controls.Add(this.lblReminder);
            this.tabTasks.Controls.Add(this.lblTaskTitle);
            this.tabTasks.Controls.Add(this.chkShowCompleted);
            this.tabTasks.Text = "📋 Tasks";
            this.tabTasks.UseVisualStyleBackColor = true;

            // lblTaskTitle
            this.lblTaskTitle.Text = "Task Title:";
            this.lblTaskTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTaskTitle.Size = new System.Drawing.Size(100, 25);

            // txtTaskTitle
            this.txtTaskTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTaskTitle.Location = new System.Drawing.Point(140, 20);
            this.txtTaskTitle.Size = new System.Drawing.Size(300, 30);

            // lblTaskDesc
            this.lblTaskDesc.Text = "Description:";
            this.lblTaskDesc.Location = new System.Drawing.Point(20, 60);
            this.lblTaskDesc.Size = new System.Drawing.Size(100, 25);

            // txtTaskDesc
            this.txtTaskDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTaskDesc.Location = new System.Drawing.Point(140, 60);
            this.txtTaskDesc.Size = new System.Drawing.Size(600, 30);

            // chkReminder
            this.chkReminder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkReminder.Text = "Set Reminder";
            this.chkReminder.Location = new System.Drawing.Point(20, 100);
            this.chkReminder.Size = new System.Drawing.Size(120, 25);
            this.chkReminder.CheckedChanged += new System.EventHandler(this.chkReminder_CheckedChanged);

            // dtpReminder
            this.dtpReminder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpReminder.Location = new System.Drawing.Point(140, 100);
            this.dtpReminder.Size = new System.Drawing.Size(200, 30);
            this.dtpReminder.Enabled = false;

            // btnAddTask
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnAddTask.FlatAppearance.BorderSize = 0;
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Location = new System.Drawing.Point(360, 95);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(120, 35);
            this.btnAddTask.TabIndex = 4;
            this.btnAddTask.Text = "➕ Add Task";
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);

            // chkShowCompleted
            this.chkShowCompleted.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkShowCompleted.Text = "Show completed tasks";
            this.chkShowCompleted.Location = new System.Drawing.Point(20, 150);
            this.chkShowCompleted.Size = new System.Drawing.Size(200, 25);
            this.chkShowCompleted.CheckedChanged += new System.EventHandler(this.chkShowCompleted_CheckedChanged);

            // dgvTasks
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(20, 190);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.Size = new System.Drawing.Size(950, 400);
            this.dgvTasks.TabIndex = 5;

            // btnCompleteTask
            this.btnCompleteTask.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCompleteTask.FlatAppearance.BorderSize = 0;
            this.btnCompleteTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleteTask.ForeColor = System.Drawing.Color.White;
            this.btnCompleteTask.Location = new System.Drawing.Point(20, 610);
            this.btnCompleteTask.Name = "btnCompleteTask";
            this.btnCompleteTask.Size = new System.Drawing.Size(150, 35);
            this.btnCompleteTask.TabIndex = 6;
            this.btnCompleteTask.Text = "✅ Mark Complete";
            this.btnCompleteTask.UseVisualStyleBackColor = false;
            this.btnCompleteTask.Click += new System.EventHandler(this.btnCompleteTask_Click);

            // btnDeleteTask
            this.btnDeleteTask.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnDeleteTask.FlatAppearance.BorderSize = 0;
            this.btnDeleteTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTask.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTask.Location = new System.Drawing.Point(180, 610);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(150, 35);
            this.btnDeleteTask.TabIndex = 7;
            this.btnDeleteTask.Text = "🗑️ Delete Task";
            this.btnDeleteTask.UseVisualStyleBackColor = false;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);

            // lblTaskCount
            this.lblTaskCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTaskCount.Location = new System.Drawing.Point(800, 150);
            this.lblTaskCount.Name = "lblTaskCount";
            this.lblTaskCount.Size = new System.Drawing.Size(150, 25);
            this.lblTaskCount.Text = "Total: 0 tasks";

            // tabQuiz
            this.tabQuiz.Controls.Add(this.lblQuizScore);
            this.tabQuiz.Controls.Add(this.btnSubmitQuiz);
            this.tabQuiz.Controls.Add(this.btnNextQuestion);
            this.tabQuiz.Controls.Add(this.btnStartQuiz);
            this.tabQuiz.Controls.Add(this.rtbQuizFeedback);
            this.tabQuiz.Controls.Add(this.panelOptions);
            this.tabQuiz.Controls.Add(this.rtbQuestion);
            this.tabQuiz.Controls.Add(this.lblQuestion);
            this.tabQuiz.Text = "🎯 Quiz";
            this.tabQuiz.UseVisualStyleBackColor = true;

            // lblQuestion
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuestion.Location = new System.Drawing.Point(20, 20);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(900, 30);
            this.lblQuestion.Text = "Click 'Start Quiz' to begin!";

            // rtbQuestion
            this.rtbQuestion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbQuestion.Location = new System.Drawing.Point(20, 60);
            this.rtbQuestion.Name = "rtbQuestion";
            this.rtbQuestion.ReadOnly = true;
            this.rtbQuestion.Size = new System.Drawing.Size(900, 80);
            this.rtbQuestion.TabIndex = 1;

            // panelOptions
            this.panelOptions.Location = new System.Drawing.Point(20, 160);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(500, 160);
            this.panelOptions.TabIndex = 2;

            // rtbQuizFeedback
            this.rtbQuizFeedback.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbQuizFeedback.Location = new System.Drawing.Point(20, 340);
            this.rtbQuizFeedback.Name = "rtbQuizFeedback";
            this.rtbQuizFeedback.ReadOnly = true;
            this.rtbQuizFeedback.Size = new System.Drawing.Size(900, 100);
            this.rtbQuizFeedback.TabIndex = 3;

            // btnStartQuiz
            this.btnStartQuiz.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnStartQuiz.FlatAppearance.BorderSize = 0;
            this.btnStartQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartQuiz.ForeColor = System.Drawing.Color.White;
            this.btnStartQuiz.Location = new System.Drawing.Point(20, 460);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(150, 40);
            this.btnStartQuiz.TabIndex = 4;
            this.btnStartQuiz.Text = "🚀 Start Quiz";
            this.btnStartQuiz.UseVisualStyleBackColor = false;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);

            // btnNextQuestion
            this.btnNextQuestion.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnNextQuestion.Enabled = false;
            this.btnNextQuestion.FlatAppearance.BorderSize = 0;
            this.btnNextQuestion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextQuestion.ForeColor = System.Drawing.Color.White;
            this.btnNextQuestion.Location = new System.Drawing.Point(180, 460);
            this.btnNextQuestion.Name = "btnNextQuestion";
            this.btnNextQuestion.Size = new System.Drawing.Size(150, 40);
            this.btnNextQuestion.TabIndex = 5;
            this.btnNextQuestion.Text = "➡️ Next";
            this.btnNextQuestion.UseVisualStyleBackColor = false;
            this.btnNextQuestion.Click += new System.EventHandler(this.btnNextQuestion_Click);

            // btnSubmitQuiz
            this.btnSubmitQuiz.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnSubmitQuiz.Enabled = false;
            this.btnSubmitQuiz.FlatAppearance.BorderSize = 0;
            this.btnSubmitQuiz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitQuiz.ForeColor = System.Drawing.Color.White;
            this.btnSubmitQuiz.Location = new System.Drawing.Point(340, 460);
            this.btnSubmitQuiz.Name = "btnSubmitQuiz";
            this.btnSubmitQuiz.Size = new System.Drawing.Size(150, 40);
            this.btnSubmitQuiz.TabIndex = 6;
            this.btnSubmitQuiz.Text = "🏆 Submit Quiz";
            this.btnSubmitQuiz.UseVisualStyleBackColor = false;
            this.btnSubmitQuiz.Click += new System.EventHandler(this.btnSubmitQuiz_Click);

            // lblQuizScore
            this.lblQuizScore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblQuizScore.Location = new System.Drawing.Point(20, 520);
            this.lblQuizScore.Name = "lblQuizScore";
            this.lblQuizScore.Size = new System.Drawing.Size(200, 30);
            this.lblQuizScore.Text = "Score: 0 / 0";

            // tabLog
            this.tabLog.Controls.Add(this.lblLogCount);
            this.tabLog.Controls.Add(this.btnShowMore);
            this.tabLog.Controls.Add(this.btnRefreshLog);
            this.tabLog.Controls.Add(this.dgvActivityLog);
            this.tabLog.Text = "📜 Activity Log";
            this.tabLog.UseVisualStyleBackColor = true;

            // dgvActivityLog
            this.dgvActivityLog.AllowUserToAddRows = false;
            this.dgvActivityLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivityLog.Location = new System.Drawing.Point(20, 20);
            this.dgvActivityLog.Name = "dgvActivityLog";
            this.dgvActivityLog.ReadOnly = true;
            this.dgvActivityLog.Size = new System.Drawing.Size(950, 550);
            this.dgvActivityLog.TabIndex = 0;

            // btnRefreshLog
            this.btnRefreshLog.BackColor = System.Drawing.Color.FromArgb(23, 162, 184);
            this.btnRefreshLog.FlatAppearance.BorderSize = 0;
            this.btnRefreshLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshLog.ForeColor = System.Drawing.Color.White;
            this.btnRefreshLog.Location = new System.Drawing.Point(20, 590);
            this.btnRefreshLog.Name = "btnRefreshLog";
            this.btnRefreshLog.Size = new System.Drawing.Size(120, 35);
            this.btnRefreshLog.TabIndex = 1;
            this.btnRefreshLog.Text = "🔄 Refresh";
            this.btnRefreshLog.UseVisualStyleBackColor = false;
            this.btnRefreshLog.Click += new System.EventHandler(this.btnRefreshLog_Click);

            // btnShowMore
            this.btnShowMore.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnShowMore.FlatAppearance.BorderSize = 0;
            this.btnShowMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowMore.ForeColor = System.Drawing.Color.White;
            this.btnShowMore.Location = new System.Drawing.Point(150, 590);
            this.btnShowMore.Name = "btnShowMore";
            this.btnShowMore.Size = new System.Drawing.Size(120, 35);
            this.btnShowMore.TabIndex = 2;
            this.btnShowMore.Text = "📄 Show More";
            this.btnShowMore.UseVisualStyleBackColor = false;
            this.btnShowMore.Click += new System.EventHandler(this.btnShowMore_Click);

            // lblLogCount
            this.lblLogCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLogCount.Location = new System.Drawing.Point(800, 590);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(170, 35);
            this.lblLogCount.Text = "No activity logs found";

            // MainForm
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControlChatbot);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🛡️ Cybersecurity Awareness Chatbot";
            this.ResumeLayout(false);
        }
    }
}