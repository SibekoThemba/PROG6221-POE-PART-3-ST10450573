using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class UserMemory
    {
        private string userName;
        private List<string> userInterests;
        private Dictionary<string, string> userPreferences;
        private DateTime conversationStartTime;
        private int messageCount;
        private List<string> conversationHistory;
        private Dictionary<string, DateTime> topicLastAsked;

        public UserMemory()
        {
            userInterests = new List<string>();
            userPreferences = new Dictionary<string, string>();
            conversationStartTime = DateTime.Now;
            messageCount = 0;
            conversationHistory = new List<string>();
            topicLastAsked = new Dictionary<string, DateTime>();
        }

        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                userName = name.Trim();
            }
        }

        public string GetUserName()
        {
            return userName ?? "friend";
        }

        public bool HasUserName()
        {
            return !string.IsNullOrEmpty(userName);
        }

        public void SetInterest(string topic)
        {
            string lowerTopic = topic.ToLower();
            if (!userInterests.Contains(lowerTopic))
            {
                userInterests.Add(lowerTopic);
            }
            topicLastAsked[lowerTopic] = DateTime.Now;
        }

        public List<string> GetInterests()
        {
            return new List<string>(userInterests);
        }

        public string GetLastInterest()
        {
            if (userInterests.Count > 0)
            {
                return userInterests[userInterests.Count - 1];
            }
            return null;
        }

        public bool HasInterestIn(string topic)
        {
            return userInterests.Contains(topic.ToLower());
        }

        public void SetPreference(string key, string value)
        {
            if (userPreferences.ContainsKey(key))
            {
                userPreferences[key] = value;
            }
            else
            {
                userPreferences.Add(key, value);
            }
        }

        public string GetPreference(string key)
        {
            return userPreferences.ContainsKey(key) ? userPreferences[key] : null;
        }

        public void AddToHistory(string message)
        {
            conversationHistory.Add(message);
            messageCount++;
        }

        public int GetMessageCount()
        {
            return messageCount;
        }

        public TimeSpan GetConversationDuration()
        {
            return DateTime.Now - conversationStartTime;
        }

        public string GetUserInfo()
        {
            string info = "";

            if (!string.IsNullOrEmpty(userName))
            {
                info = $"I remember you, {userName}! ";

                if (userInterests.Count > 0)
                {
                    info += $"You're interested in {string.Join(", ", userInterests)}. ";

                    // Personalized follow-up based on interests
                    string lastInterest = GetLastInterest();
                    if (lastInterest != null)
                    {
                        info += $"\n\nSince you're interested in {lastInterest}, would you like more tips on this topic? Just say 'tell me more'!";
                    }
                    else
                    {
                        info += "\n\nWould you like more tips on any of these topics? Just say 'tell me more'!";
                    }
                }
                else
                {
                    info += "What cybersecurity topic would you like to learn about? I can teach you about passwords, phishing, scams, privacy, or safe browsing!";
                }

                // Personalized greeting based on time of day
                int hour = DateTime.Now.Hour;
                if (hour < 12)
                {
                    info = "Good morning! " + info;
                }
                else if (hour < 18)
                {
                    info = "Good afternoon! " + info;
                }
                else
                {
                    info = "Good evening! " + info;
                }
            }
            else
            {
                info = "I don't think you've told me your name yet. What's your name? (Say 'My name is [Your Name]' or 'Call me [Your Name]')";
            }

            return info;
        }

        public string GetPersonalizedTip()
        {
            if (userInterests.Count > 0)
            {
                string randomInterest = userInterests[new Random().Next(userInterests.Count)];
                return $"As someone interested in {randomInterest}, here's a specialized tip for you! ";
            }
            return "Here's a helpful cybersecurity tip for you! ";
        }

        public void Reset()
        {
            userName = null;
            userInterests.Clear();
            userPreferences.Clear();
            conversationStartTime = DateTime.Now;
            messageCount = 0;
            conversationHistory.Clear();
            topicLastAsked.Clear();
        }
    }
}