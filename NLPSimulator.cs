using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public class NLPSimulator
    {
        private Dictionary<string, List<string>> intentKeywords;
        private Dictionary<string, string> responseTemplates;

        public NLPSimulator()
        {
            InitializeIntents();
            InitializeResponses();
        }

        private void InitializeIntents()
        {
            intentKeywords = new Dictionary<string, List<string>>
            {
                { "add_task", new List<string> { "add task", "new task", "create task", "add to do", "add todo" } },
                { "reminder", new List<string> { "remind", "reminder", "notify", "alert" } },
                { "help", new List<string> { "help", "what can you do", "assist", "guide", "help me" } },
                { "greeting", new List<string> { "hello", "hi", "hey", "howdy", "good morning", "good afternoon", "good evening" } },
                { "goodbye", new List<string> { "bye", "goodbye", "see you", "exit", "quit", "close" } },
                { "thanks", new List<string> { "thank", "thanks", "appreciate", "thank you" } },
                { "task_status", new List<string> { "show tasks", "list tasks", "view tasks", "tasks", "my tasks" } },
                { "quiz", new List<string> { "quiz", "game", "play quiz", "start quiz", "take quiz" } },
                { "activity_log", new List<string> { "show log", "activity log", "what have you done", "recent actions", "log" } },
                { "password", new List<string> { "password", "passphrase", "login", "password safety", "secure password" } },
                { "phishing", new List<string> { "phishing", "scam", "fraud", "suspicious", "email scam" } },
                { "2fa", new List<string> { "2fa", "two-factor", "multi-factor", "mfa", "two factor", "authentication" } },
                { "safe_browsing", new List<string> { "safe browsing", "secure browsing", "browser", "browsing" } },
                { "social_engineering", new List<string> { "social engineering", "manipulation", "human hacking" } },
                { "malware", new List<string> { "malware", "virus", "ransomware", "trojan", "spyware" } },
                { "vpn", new List<string> { "vpn", "virtual private network", "private network" } },
                { "privacy", new List<string> { "privacy", "data privacy", "personal data" } },
                { "software_updates", new List<string> { "update", "software update", "patch", "security patch" } },
                { "public_wifi", new List<string> { "public wifi", "wifi", "wireless", "hotspot" } }
            };
        }

        private void InitializeResponses()
        {
            responseTemplates = new Dictionary<string, string>
            {
                { "add_task", "I can help you add a task! 🗂️ Go to the 'Tasks' tab and fill in the task details. You can add a title, description, and even set a reminder date!" },
                { "reminder", "I can set reminders for your tasks! 🔔 Go to the 'Tasks' tab, add a task and check 'Set Reminder' to schedule a notification." },
                { "help", "I'm your Cybersecurity Awareness Assistant! 🛡️ I can help you with:\n\n• Cybersecurity tips (password, phishing, safe browsing)\n• Managing your tasks\n• Taking a cybersecurity quiz\n• Viewing your activity log\n\nType a topic like 'password' or 'phishing' to learn more!" },
                { "greeting", "Hello! 👋 I'm your Cybersecurity Awareness Assistant. How can I help you today?" },
                { "goodbye", "Goodbye! 🚀 Remember to stay safe online. Here are some quick tips:\n✅ Use strong passwords\n✅ Enable 2FA\n✅ Be cautious of suspicious emails\n✅ Update your software regularly" },
                { "thanks", "You're welcome! 😊 I'm always here to help. Stay safe online! 🔒" },
                { "task_status", "You can view all your tasks in the 'Tasks' tab. 📋 I can help you add, complete, or delete tasks!" },
                { "quiz", "Great! 🎯 Go to the 'Quiz' tab and click 'Start Quiz' to test your cybersecurity knowledge!" },
                { "activity_log", "You can view your full activity log in the 'Activity Log' tab. 📜 I track all your actions!" },
                { "password", "🔐 Password Safety Tips:\n\n• Use strong passwords with uppercase, lowercase, numbers, and symbols\n• Never reuse passwords across different accounts\n• Use a password manager\n• Change passwords regularly\n• Enable 2FA for extra security" },
                { "phishing", "🎣 Phishing Prevention Tips:\n\n• Don't click suspicious links in emails or messages\n• Always verify sender email addresses\n• Look for spelling and grammar errors\n• Never share personal information via email\n• Report phishing emails to your IT department" },
                { "2fa", "🔑 Two-Factor Authentication (2FA):\n\n• Adds an extra layer of security to your accounts\n• Requires a code from your phone or authenticator app\n• Even if someone has your password, they can't access your account\n• Enable 2FA on all important accounts" },
                { "safe_browsing", "🌐 Safe Browsing Tips:\n\n• Always use HTTPS websites (look for the padlock icon)\n• Avoid public Wi-Fi for sensitive transactions\n• Use a VPN for secure browsing\n• Keep your browser updated\n• Don't download from untrusted sources" },
                { "social_engineering", "🧠 Social Engineering Awareness:\n\n• Be cautious of unsolicited requests for information\n• Verify identities before sharing sensitive data\n• Don't trust unexpected emails or phone calls\n• Hackers manipulate human psychology, not just computers" },
                { "malware", "🦠 Malware Protection Tips:\n\n• Install reputable antivirus software\n• Keep your operating system updated\n• Don't download files from untrusted sources\n• Be careful with email attachments\n• Regular system scans are important" },
                { "vpn", "🔒 VPN (Virtual Private Network):\n\n• Encrypts your internet traffic\n• Hides your IP address and location\n• Protects you on public Wi-Fi\n• Choose a reputable VPN provider" },
                { "privacy", "🔏 Data Privacy Tips:\n\n• Review privacy settings on all accounts\n• Limit what personal information you share online\n• Use privacy-focused browsers and search engines\n• Be careful what you post on social media" },
                { "software_updates", "📦 Software Update Tips:\n\n• Install updates as soon as they're available\n• Enable automatic updates where possible\n• Security patches fix known vulnerabilities\n• Don't ignore update notifications" },
                { "public_wifi", "📶 Public Wi-Fi Safety:\n\n• Avoid accessing sensitive accounts on public Wi-Fi\n• Use a VPN when connecting to public networks\n• Turn off file sharing when on public Wi-Fi\n• Use your mobile data for sensitive transactions" }
            };
        }

        public string ProcessInput(string input, string userName, TaskManager taskManager)
        {
            string lowerInput = input.ToLower().Trim();

            // Check for specific task commands
            if (lowerInput.StartsWith("add task") || lowerInput.Contains("add a task") || lowerInput.Contains("create task"))
            {
                return "I see you want to add a task! 🗂️ Please go to the 'Tasks' tab and fill in the task details. You can also add a reminder!";
            }

            if (lowerInput.Contains("complete task") || lowerInput.Contains("mark complete"))
            {
                return "To complete a task, go to the 'Tasks' tab, select a task, and click 'Mark Complete'. Great job staying organised! ✅";
            }

            if (lowerInput.Contains("delete task") || lowerInput.Contains("remove task"))
            {
                return "To delete a task, go to the 'Tasks' tab, select a task, and click 'Delete Task'. Be careful - this action cannot be undone! 🗑️";
            }

            // Check for activity log request
            if (lowerInput.Contains("activity log") || lowerInput.Contains("what have you done") || lowerInput.Contains("show log") || lowerInput.Contains("recent actions"))
            {
                return "📜 You can view your full activity log in the 'Activity Log' tab. I track all your tasks, quiz attempts, and conversations!";
            }

            // Check for quiz request
            if (lowerInput.Contains("quiz") || lowerInput.Contains("game") || lowerInput.Contains("test my knowledge") || lowerInput.Contains("play quiz"))
            {
                return "🎯 Ready to test your cybersecurity knowledge? Go to the 'Quiz' tab and click 'Start Quiz'! You'll get immediate feedback on each of the 15 questions.";
            }

            // Check for help
            if (lowerInput.Contains("help") || lowerInput.Contains("what can you do") || lowerInput.Contains("assist"))
            {
                return GetResponse("help");
            }

            // Check for cybersecurity topics FIRST (before greeting)
            foreach (var intent in intentKeywords)
            {
                if (intent.Key == "add_task" || intent.Key == "reminder" || intent.Key == "help" ||
                    intent.Key == "greeting" || intent.Key == "goodbye" || intent.Key == "thanks" ||
                    intent.Key == "task_status" || intent.Key == "quiz" || intent.Key == "activity_log")
                    continue;

                foreach (string keyword in intent.Value)
                {
                    if (lowerInput.Contains(keyword))
                    {
                        return GetResponse(intent.Key);
                    }
                }
            }

            // Check for greeting (only if input is short and only greeting)
            if (IsGreeting(lowerInput) && lowerInput.Length < 15)
            {
                return $"Hello, {userName}! 👋 I'm your Cybersecurity Awareness Assistant. How can I help you today? 💬 You can ask about cybersecurity, tasks, or take a quiz!";
            }

            // Check for goodbye
            if (lowerInput.Contains("bye") || lowerInput.Contains("goodbye") || lowerInput.Contains("exit") || lowerInput.Contains("quit"))
            {
                return GetResponse("goodbye");
            }

            // Check for thanks
            if (lowerInput.Contains("thank"))
            {
                return GetResponse("thanks");
            }

            // Default response
            return GetDefaultResponse();
        }

        private bool IsGreeting(string input)
        {
            string[] greetings = { "hello", "hi", "hey", "howdy", "good morning", "good afternoon", "good evening", "greetings" };
            foreach (string g in greetings)
            {
                if (input.Contains(g))
                    return true;
            }
            return false;
        }

        private string GetResponse(string intent)
        {
            if (responseTemplates.ContainsKey(intent))
                return responseTemplates[intent];
            return null;
        }

        private string GetDefaultResponse()
        {
            string[] defaultResponses = {
                "I'm not sure I understand that request. 🤔 Could you please rephrase? You can ask about:\n• Cybersecurity tips (password, phishing, safe browsing)\n• Adding or managing tasks\n• Taking the cybersecurity quiz\n• Viewing your activity log\n• Type 'help' for more options!",
                "Hmm, I didn't quite catch that. 😕 Try asking me about password safety, phishing, or tasks! I'm here to help you stay secure online.",
                "I'm still learning! 🧠 Could you try asking that differently? You can also use the tabs above for specific features like Tasks, Quiz, and Activity Log.",
                "That's a new one! 🤖 I can help with cybersecurity questions, tasks, and quizzes. What would you like to do? Try typing 'help' for suggestions."
            };
            Random rand = new Random();
            return defaultResponses[rand.Next(defaultResponses.Length)];
        }
    }
}