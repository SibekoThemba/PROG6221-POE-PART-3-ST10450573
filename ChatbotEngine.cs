using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class ChatbotEngine
    {
        private UserMemory memory;
        private ResponseManager responseManager;
        private SentimentAnalyzer sentimentAnalyzer;
        private string currentTopic;
        private List<string> conversationHistory;
        private Dictionary<string, int> topicFollowUpCount;
        private Random random;

        public ChatbotEngine()
        {
            memory = new UserMemory();
            responseManager = new ResponseManager();
            sentimentAnalyzer = new SentimentAnalyzer();
            currentTopic = "";
            conversationHistory = new List<string>();
            topicFollowUpCount = new Dictionary<string, int>();
            random = new Random();
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return GetRandomResponse(new string[] {
                    "Please say something. I'm here to help you learn about cybersecurity!",
                    "I didn't catch that. Could you tell me what you'd like to learn about?",
                    "Type something and I'll help you with cybersecurity tips!"
                });
            }

            conversationHistory.Add(userInput);
            string lowerInput = userInput.ToLower();

            // Check for exit
            if (lowerInput.Contains("exit") || lowerInput.Contains("quit") || lowerInput.Contains("goodbye"))
            {
                string userName = memory.GetUserName();
                if (userName != "friend")
                {
                    return GetRandomResponse(new string[] {
                        $"Goodbye {userName}! Remember to stay safe online! 🔒",
                        $"See you later {userName}! Keep practicing good cybersecurity habits! 🛡️",
                        $"Farewell {userName}! Stay vigilant against online threats! 👋"
                    });
                }
                return GetRandomResponse(new string[] {
                    "Goodbye! Remember to stay safe online! 🔒",
                    "See you next time! Keep learning about cybersecurity! 👋",
                    "Farewell! Stay protected online! 🛡️"
                });
            }

            // Check for memory requests
            if (lowerInput.Contains("what's my name") || lowerInput.Contains("remember me") || lowerInput.Contains("do you remember"))
            {
                return memory.GetUserInfo();
            }

            // Store name if provided
            if (lowerInput.StartsWith("my name is") || lowerInput.Contains("i'm ") || lowerInput.Contains("call me"))
            {
                string name = ExtractName(userInput);
                if (!string.IsNullOrEmpty(name))
                {
                    memory.SetUserName(name);
                    return GetRandomResponse(new string[] {
                        $"Nice to meet you, {name}! I'll remember that. What would you like to learn about cybersecurity?",
                        $"Welcome {name}! I'm excited to help you learn about online safety! What interests you?",
                        $"{name} is a great name! I'll remember you. Shall we talk about cybersecurity?"
                    });
                }
            }

            // Check for topic interest and store in memory
            string[] topics = { "password", "phish", "scam", "privacy", "browsing", "safe browsing" };
            foreach (string topic in topics)
            {
                if (lowerInput.Contains(topic))
                {
                    memory.SetInterest(topic);
                    currentTopic = topic;

                    if (!topicFollowUpCount.ContainsKey(currentTopic))
                    {
                        topicFollowUpCount[currentTopic] = 0;
                    }
                    topicFollowUpCount[currentTopic]++;

                    string response = responseManager.GetResponse(userInput);

                    if (topicFollowUpCount[currentTopic] == 1)
                    {
                        response = response + "\n\n💡 I've noted your interest in " + topic + ". Would you like more tips on this? (Just say 'tell me more' or 'another tip')";
                    }
                    return response;
                }
            }

            // Handle follow-up requests
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") || lowerInput.Contains("more tips"))
            {
                if (!string.IsNullOrEmpty(currentTopic))
                {
                    string response = responseManager.GetResponse(currentTopic);
                    topicFollowUpCount[currentTopic]++;
                    return response + $"\n\n🔐 That was tip #{topicFollowUpCount[currentTopic]}! Would you like another one? Just say 'tell me more' again!";
                }
                return "What topic would you like to learn more about? (password, phishing, scams, privacy, or safe browsing)";
            }

            // Sentiment detection - enhanced
            string sentiment = sentimentAnalyzer.DetectSentiment(userInput);
            if (sentiment != "neutral")
            {
                string sentimentResponse = sentimentAnalyzer.GetSentimentResponse(sentiment);
                if (!string.IsNullOrEmpty(sentimentResponse))
                {
                    string tip = responseManager.GetResponse("password");
                    return sentimentResponse + "\n\n" + tip;
                }
            }

            // Check for gratitude
            if (lowerInput.Contains("thank") || lowerInput.Contains("thanks"))
            {
                return GetRandomResponse(new string[] {
                    "🙏 You're very welcome! I'm glad I could help. Stay safe online!",
                    "😊 My pleasure! Remember, knowledge is the best protection against cyber threats.",
                    "👍 Happy to help! Keep learning and stay vigilant!"
                });
            }

            // Default response
            string defaultResponse = responseManager.GetResponse(userInput);
            if (defaultResponse.Contains("didn't understand"))
            {
                return defaultResponse + "\n\n📚 You can ask me about:\n• Passwords 🔐\n• Phishing 🎣\n• Scams ⚠️\n• Privacy 🛡️\n• Safe browsing 🌐\n\n💬 Or just tell me your name to get started!";
            }

            return defaultResponse;
        }

        private string ExtractName(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            string lowerInput = input.ToLower();
            if (lowerInput.Contains("my name is "))
            {
                int index = lowerInput.IndexOf("my name is ") + 11;
                if (index < input.Length)
                {
                    string name = input.Substring(index).Trim();
                    return name.Length > 0 ? name : null;
                }
            }
            if (lowerInput.Contains("i'm "))
            {
                int index = lowerInput.IndexOf("i'm ") + 4;
                if (index < input.Length)
                {
                    string name = input.Substring(index).Trim();
                    return name.Length > 0 ? name : null;
                }
            }
            if (lowerInput.Contains("call me "))
            {
                int index = lowerInput.IndexOf("call me ") + 8;
                if (index < input.Length)
                {
                    string name = input.Substring(index).Trim();
                    return name.Length > 0 ? name : null;
                }
            }
            return null;
        }

        private string GetRandomResponse(string[] responses)
        {
            return responses[random.Next(responses.Length)];
        }

        // Additional methods for returning user detection
        public bool HasReturningUser()
        {
            return memory.HasUserName() && memory.GetMessageCount() > 0;
        }

        public string GetUserName()
        {
            return memory.GetUserName();
        }

        public string GetLastInterest()
        {
            return memory.GetLastInterest() ?? "cybersecurity";
        }
    }
}