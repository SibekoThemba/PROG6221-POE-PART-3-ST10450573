using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CyberSecurityChatbot
{
    public class NLPSimulator
    {
        private Dictionary<string, List<string>> intentKeywords;
        private Dictionary<string, List<string>> intentSynonyms;
        private Dictionary<string, string> responseTemplates;
        private Dictionary<string, List<string>> followUpQuestions;
        private Random random;

        public NLPSimulator()
        {
            random = new Random();
            InitializeIntents();
            InitializeSynonyms();
            InitializeResponses();
            InitializeFollowUpQuestions();
        }

        private void InitializeIntents()
        {
            intentKeywords = new Dictionary<string, List<string>>
            {
                // Bot identity - CHECK THIS FIRST
                { "bot_identity", new List<string> { "who are you", "what are you", "about you", "what do you do", "tell me about yourself", "who is this" } },
                
                // Task related
                { "add_task", new List<string> { "add task", "new task", "create task", "add to do", "add todo", "create todo", "make task" } },
                { "view_tasks", new List<string> { "show tasks", "list tasks", "view tasks", "tasks", "my tasks", "what tasks", "pending tasks", "task list" } },
                { "complete_task", new List<string> { "complete task", "mark done", "finish task", "task done", "mark complete" } },
                { "delete_task", new List<string> { "delete task", "remove task", "clear task", "erase task" } },
                { "reminder", new List<string> { "remind", "reminder", "notify", "alert", "set reminder", "schedule reminder" } },
                
                // Help and navigation
                { "help", new List<string> { "help", "what can you do", "assist", "guide", "help me", "options", "menu", "capabilities" } },
                
                // Greetings - CHECK LATER
                { "greeting", new List<string> { "hello", "hi", "hey", "howdy", "good morning", "good afternoon", "good evening", "greetings", "yo", "sup", "whats up", "how are you" } },
                { "goodbye", new List<string> { "bye", "goodbye", "see you", "exit", "quit", "close", "later", "cya", "take care" } },
                { "thanks", new List<string> { "thank", "thanks", "appreciate", "thank you", "thx", "much appreciated" } },
                
                // Activity log
                { "activity_log", new List<string> { "show log", "activity log", "what have you done", "recent actions", "log", "history", "show activity" } },
                
                // Cybersecurity topics
                { "password", new List<string> { "password", "passphrase", "login", "password safety", "password security", "secure password", "strong password", "passwords" } },
                { "phishing", new List<string> { "phishing", "scam", "fraud", "suspicious", "email scam", "fake email", "cyber scam", "phishing email", "phishing attack" } },
                { "2fa", new List<string> { "2fa", "two-factor", "multi-factor", "mfa", "two factor", "authentication", "two step", "2 factor" } },
                { "safe_browsing", new List<string> { "safe browsing", "secure browsing", "browser", "browsing", "internet safety", "web safety" } },
                { "social_engineering", new List<string> { "social engineering", "manipulation", "human hacking", "social", "psychology", "trust attack" } },
                { "malware", new List<string> { "malware", "virus", "ransomware", "trojan", "spyware", "worm", "adware", "keylogger" } },
                { "vpn", new List<string> { "vpn", "virtual private network", "private network", "encryption", "secure connection" } },
                { "data_privacy", new List<string> { "privacy", "data privacy", "personal data", "information security", "data protection" } },
                { "software_updates", new List<string> { "update", "software update", "patch", "security patch", "update software", "system update" } },
                { "email_security", new List<string> { "email security", "secure email", "email safety", "email protection" } },
                { "online_banking", new List<string> { "banking", "online banking", "financial security", "bank", "finance" } },
                { "public_wifi", new List<string> { "public wifi", "wifi", "wireless", "hotspot", "free wifi" } },
                { "quiz", new List<string> { "quiz", "game", "play quiz", "start quiz", "take quiz", "test", "knowledge test", "cyber quiz" } }
            };
        }

        private void InitializeSynonyms()
        {
            intentSynonyms = new Dictionary<string, List<string>>
            {
                { "password", new List<string> { "pass", "passcode", "secret", "credentials", "login info", "account key" } },
                { "phishing", new List<string> { "scam", "con", "trick", "deceive", "fake" } },
                { "malware", new List<string> { "virus", "trojan", "worm", "spyware", "ransomware" } },
                { "2fa", new List<string> { "2 step", "two step", "multi factor", "authenticator" } },
                { "privacy", new List<string> { "private", "confidential", "sensitive", "data" } }
            };
        }

        private void InitializeResponses()
        {
            responseTemplates = new Dictionary<string, string>
            {
                { "bot_identity", "🛡️ I'm your Cybersecurity Awareness Assistant! I'm here to help you stay safe online. I can:\n\n• Provide cybersecurity tips (password, phishing, safe browsing, etc.)\n• Help you manage your tasks\n• Give you a cybersecurity quiz\n• Show you your activity log\n\nWhat would you like to learn about today?" },
                { "add_task", "I can help you add a task! 🗂️ Go to the 'Tasks' tab to add your cybersecurity tasks. You can add a title, description, and even a reminder date. Just type 'help' if you need guidance!" },
                { "view_tasks", "You can view all your tasks in the 'Tasks' tab. 📋 I can help you add, complete, or delete tasks to stay organised. You currently have tasks pending - check the Tasks tab!" },
                { "complete_task", "To complete a task, go to the 'Tasks' tab, select a task, and click 'Mark Complete'. Great job staying organised! ✅" },
                { "delete_task", "To delete a task, go to the 'Tasks' tab, select a task, and click 'Delete Task'. Be careful - this action cannot be undone! 🗑️" },
                { "reminder", "I can set reminders for your tasks! 🔔 Go to the 'Tasks' tab, add a task and check 'Set Reminder' to schedule a notification. Would you like me to help you set one up now?" },
                { "help", "I'm your Cybersecurity Awareness Assistant! 🛡️ I can help you with:\n\n• 🔐 Cybersecurity tips (password, phishing, safe browsing)\n• 📋 Managing your tasks (add, complete, delete)\n• 🎯 Taking a cybersecurity quiz\n• 📜 Viewing your activity log\n\nJust type a topic like 'password' or 'phishing' to learn more! What would you like to explore?" },
                { "greeting", "Hello! 👋 I'm your Cybersecurity Awareness Assistant. I'm here to help you stay safe online! You can ask me about cybersecurity topics, manage tasks, or take a quiz. How can I help you today?" },
                { "goodbye", "Goodbye! 🚀 Remember to stay safe online with these tips:\n✅ Use strong, unique passwords\n✅ Enable 2FA on important accounts\n✅ Be cautious of suspicious emails\n✅ Update your software regularly\n\nI'm always here if you need help again! 👋" },
                { "thanks", "You're welcome! 😊 I'm always here to help you stay safe online. Feel free to ask me anything about cybersecurity, tasks, or take the quiz anytime! 🔒" },
                { "activity_log", "You can view your full activity log in the 'Activity Log' tab. 📜 I track all your tasks, quiz attempts, and conversations to help you see your progress!" },
                { "password", "🔐 Password Safety Tips:\n\n• Use strong passwords with uppercase, lowercase, numbers, and symbols\n• Never reuse passwords across different accounts\n• Use a password manager to store them securely\n• Change passwords regularly\n• Enable 2FA for extra security\n\nWould you like more details on any of these tips?" },
                { "phishing", "🎣 Phishing Prevention Tips:\n\n• Don't click suspicious links in emails or messages\n• Always verify sender email addresses\n• Look for spelling and grammar errors\n• Never share personal information via email\n• Report phishing emails to your IT department\n• When in doubt, delete it!\n\nHave you ever received a suspicious email before?" },
                { "2fa", "🔑 Two-Factor Authentication (2FA):\n\n• Adds an extra layer of security to your accounts\n• Requires a code from your phone or authenticator app\n• Even if someone has your password, they can't access your account\n• Enable 2FA on all important accounts\n• Use an authenticator app instead of SMS when possible\n\nIt's one of the best ways to protect your accounts!" },
                { "safe_browsing", "🌐 Safe Browsing Tips:\n\n• Always use HTTPS websites (look for the padlock icon)\n• Avoid public Wi-Fi for sensitive transactions\n• Use a VPN for secure browsing\n• Keep your browser updated\n• Don't download from untrusted sources\n• Use ad-blockers and privacy extensions\n\nThese habits will keep you much safer online!" },
                { "social_engineering", "🧠 Social Engineering Awareness:\n\n• Be cautious of unsolicited requests for information\n• Verify identities before sharing sensitive data\n• Don't trust unexpected emails or phone calls\n• Hackers manipulate human psychology, not just computers\n• Always verify through a trusted channel\n• If it seems too good to be true, it probably is!\n\nWould you like to know how to spot social engineering attempts?" },
                { "malware", "🦠 Malware Protection Tips:\n\n• Install reputable antivirus software\n• Keep your operating system updated\n• Don't download files from untrusted sources\n• Be careful with email attachments\n• Regular system scans are important\n• Use a firewall\n\nProtecting against malware is essential for keeping your data safe!" },
                { "vpn", "🔒 VPN (Virtual Private Network):\n\n• Encrypts your internet traffic\n• Hides your IP address and location\n• Protects you on public Wi-Fi\n• Bypasses geographical restrictions\n• Choose a reputable VPN provider\n• Essential for privacy and security\n\nDo you use a VPN when connecting to public networks?" },
                { "data_privacy", "🔏 Data Privacy Tips:\n\n• Review privacy settings on all accounts\n• Limit what personal information you share online\n• Use privacy-focused browsers and search engines\n• Be careful what you post on social media\n• Regularly review app permissions\n\nYour data is valuable - protect it!" },
                { "software_updates", "📦 Software Update Tips:\n\n• Install updates as soon as they're available\n• Enable automatic updates where possible\n• Security patches fix known vulnerabilities\n• Don't ignore update notifications\n\nUpdates are one of the easiest ways to stay secure!" },
                { "email_security", "📧 Email Security Tips:\n\n• Use a reputable email provider\n• Enable two-factor authentication\n• Be cautious of unexpected attachments\n• Use spam filters\n• Never share your password\n\nEmail is a common target - stay vigilant!" },
                { "online_banking", "💰 Online Banking Safety:\n\n• Use strong, unique passwords\n• Enable 2FA for banking\n• Monitor your accounts regularly\n• Use secure networks only\n• Never share banking details\n\nYour finances are worth protecting!" },
                { "public_wifi", "📶 Public Wi-Fi Safety:\n\n• Avoid accessing sensitive accounts on public Wi-Fi\n• Use a VPN when connecting to public networks\n• Turn off file sharing when on public Wi-Fi\n• Use your mobile data for sensitive transactions\n• Disable auto-connect to open networks\n\nPublic Wi-Fi is convenient but risky!" },
                { "quiz", "🎯 Ready to test your cybersecurity knowledge? Go to the 'Quiz' tab and click 'Start Quiz'! You'll get immediate feedback on each of the 15 questions. Good luck! 🍀" }
            };
        }

        private void InitializeFollowUpQuestions()
        {
            followUpQuestions = new Dictionary<string, List<string>>
            {
                { "password", new List<string> { "Would you like to know more about creating strong passwords?", "Do you use a password manager?" } },
                { "phishing", new List<string> { "Have you ever received a suspicious email?", "Would you like to see an example of a phishing attempt?" } },
                { "2fa", new List<string> { "Do you have 2FA enabled on your accounts?", "Would you like help enabling 2FA?" } },
                { "malware", new List<string> { "Do you have antivirus software installed?", "Would you like tips on removing malware?" } },
                { "vpn", new List<string> { "Do you use a VPN?", "Would you like VPN recommendations?" } },
                { "social_engineering", new List<string> { "Have you ever been targeted by a social engineering attack?", "Would you like to learn how to identify social engineering?" } }
            };
        }

        public string ProcessInput(string input, string userName, TaskManager taskManager)
        {
            string lowerInput = input.ToLower().Trim();

            // =====================================================
            // STEP 1: Check for bot identity questions (BEFORE anything else)
            // =====================================================

            foreach (string keyword in intentKeywords["bot_identity"])
            {
                if (lowerInput.Contains(keyword))
                {
                    return GetResponse("bot_identity");
                }
            }

            // =====================================================
            // STEP 2: Check for specific task commands
            // =====================================================

            if (lowerInput.StartsWith("add task") || lowerInput.Contains("add a task") || lowerInput.Contains("create task") || lowerInput.Contains("add to do"))
            {
                return "I see you want to add a task! 🗂️ Please go to the 'Tasks' tab and fill in the task details. You can add a title, description, and even set a reminder date. Would you like me to guide you through it?";
            }

            if (lowerInput.Contains("complete task") || lowerInput.Contains("mark complete") || lowerInput.Contains("finish task"))
            {
                return "To complete a task, go to the 'Tasks' tab, select a task, and click 'Mark Complete'. Great job staying organised! ✅ Is there a specific task you want to complete?";
            }

            if (lowerInput.Contains("delete task") || lowerInput.Contains("remove task") || lowerInput.Contains("clear task"))
            {
                return "To delete a task, go to the 'Tasks' tab, select a task, and click 'Delete Task'. Be careful - this action cannot be undone! 🗑️ Would you like to review your tasks before deleting?";
            }

            if (lowerInput.Contains("show tasks") || lowerInput.Contains("list tasks") || lowerInput.Contains("view tasks") || lowerInput.Contains("my tasks"))
            {
                return "You can view all your tasks in the 'Tasks' tab. 📋 You can add, complete, or delete tasks there. Would you like me to explain any of these actions?";
            }

            // =====================================================
            // STEP 3: Check for activity log request
            // =====================================================

            if (lowerInput.Contains("activity log") || lowerInput.Contains("what have you done") || lowerInput.Contains("show log") || lowerInput.Contains("recent actions") || lowerInput.Contains("history"))
            {
                return "📜 You can view your full activity log in the 'Activity Log' tab. I track all your tasks, quiz attempts, and conversations! Would you like to see your recent activity?";
            }

            // =====================================================
            // STEP 4: Check for quiz request
            // =====================================================

            if (lowerInput.Contains("quiz") || lowerInput.Contains("game") || lowerInput.Contains("test my knowledge") || lowerInput.Contains("play quiz") || lowerInput.Contains("take quiz"))
            {
                return "🎯 Ready to test your cybersecurity knowledge? Go to the 'Quiz' tab and click 'Start Quiz'! You'll get immediate feedback on each of the 15 questions. Are you ready to start?";
            }

            // =====================================================
            // STEP 5: Check for help
            // =====================================================

            if (lowerInput.Contains("help") || lowerInput.Contains("what can you do") || lowerInput.Contains("assist") || lowerInput.Contains("options") || lowerInput.Contains("menu"))
            {
                return GetResponse("help");
            }

            // =====================================================
            // STEP 6: Check for cybersecurity topics (BEFORE greetings)
            // =====================================================

            string detectedIntent = DetectIntent(lowerInput);
            if (detectedIntent != null && detectedIntent != "greeting")
            {
                string response = GetResponse(detectedIntent);

                // Add follow-up question for engaging interaction
                string followUp = GetFollowUpQuestion(detectedIntent);
                if (!string.IsNullOrEmpty(followUp))
                {
                    response += "\n\n" + followUp;
                }

                return response;
            }

            // =====================================================
            // STEP 7: Check for greeting (with exact word matching)
            // =====================================================

            if (IsGreeting(lowerInput))
            {
                string[] engagingGreetings = {
                    $"Hello, {userName}! 👋 Great to see you! I'm your Cybersecurity Awareness Assistant. How can I help you stay safe online today?",
                    $"Hi {userName}! 👋 I'm here to help with cybersecurity, tasks, and quizzes. What would you like to explore today?",
                    $"Hey {userName}! 👋 Ready to boost your cybersecurity knowledge? I can help with tips, tasks, and quizzes!",
                    $"Welcome back, {userName}! 👋 How can I assist you with your cybersecurity journey today?"
                };
                return engagingGreetings[random.Next(engagingGreetings.Length)];
            }

            // =====================================================
            // STEP 8: Check for goodbye
            // =====================================================

            if (lowerInput.Contains("bye") || lowerInput.Contains("goodbye") || lowerInput.Contains("see you") || lowerInput.Contains("exit") || lowerInput.Contains("quit") || lowerInput.Contains("later") || lowerInput.Contains("cya") || lowerInput.Contains("take care"))
            {
                return GetResponse("goodbye");
            }

            // =====================================================
            // STEP 9: Check for thanks
            // =====================================================

            if (lowerInput.Contains("thank") || lowerInput.Contains("thanks") || lowerInput.Contains("appreciate") || lowerInput.Contains("thx"))
            {
                return GetResponse("thanks");
            }

            // =====================================================
            // STEP 10: Default responses
            // =====================================================

            return GetDefaultResponse();
        }

        private string DetectIntent(string input)
        {
            // First check for exact keyword matches
            foreach (var intent in intentKeywords)
            {
                if (intent.Key == "greeting" || intent.Key == "bot_identity") continue; // Skip these

                foreach (string keyword in intent.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return intent.Key;
                    }
                }
            }

            // Then check for synonyms
            foreach (var synonymGroup in intentSynonyms)
            {
                foreach (string synonym in synonymGroup.Value)
                {
                    if (input.Contains(synonym))
                    {
                        foreach (var intent in intentKeywords)
                        {
                            if (intent.Key == synonymGroup.Key)
                            {
                                return intent.Key;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private bool IsGreeting(string input)
        {
            // Use word boundary detection to avoid "phishing" matching "hi"
            string[] greetings = { "hello", "hi", "hey", "howdy", "good morning", "good afternoon", "good evening", "greetings", "yo", "sup", "whats up", "how are you" };
            foreach (string g in greetings)
            {
                if (input.Contains(g))
                {
                    // Make sure it's a whole word match for short greetings like "hi"
                    if (g.Length <= 3)
                    {
                        // Check if it's a separate word
                        string[] words = input.Split(' ');
                        foreach (string word in words)
                        {
                            if (word == g)
                                return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private string GetResponse(string intent)
        {
            if (responseTemplates.ContainsKey(intent))
                return responseTemplates[intent];
            return null;
        }

        private string GetFollowUpQuestion(string intent)
        {
            if (followUpQuestions.ContainsKey(intent))
            {
                var questions = followUpQuestions[intent];
                return questions[random.Next(questions.Count)];
            }
            return null;
        }

        private string GetDefaultResponse()
        {
            string[] defaultResponses = {
                "That's an interesting question! 🤔 I'm not entirely sure I understand. Could you rephrase that? You can ask about:\n• Cybersecurity tips (password, phishing, safe browsing)\n• Adding or managing tasks\n• Taking the cybersecurity quiz\n• Viewing your activity log\n\nType 'help' for more options!",
                "Hmm, I didn't quite catch that. 😕 Try asking me about password safety, phishing, or tasks! I'm here to help you stay secure online. What would you like to know?",
                "I'm still learning, but I'm here to help! 🧠 Could you try asking that differently? You can also use the tabs above for specific features like Tasks, Quiz, and Activity Log.",
                "Interesting! 🤖 I can help with cybersecurity questions, tasks, and quizzes. What would you like to do? Try typing 'help' for suggestions, or ask me about a specific security topic!",
                "I want to help, but I'm not sure what you mean. 💬 Here are some things you can ask me about:\n• 'password' for password safety tips\n• 'phishing' to learn about phishing scams\n• '2fa' for two-factor authentication\n• 'malware' for protection tips\n• 'quiz' to start the cybersecurity quiz"
            };
            return defaultResponses[random.Next(defaultResponses.Length)];
        }
    }
}