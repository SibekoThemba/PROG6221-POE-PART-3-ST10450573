using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class SentimentAnalyzer
    {
        private Dictionary<string, string> sentimentKeywords;
        private Dictionary<string, List<string>> sentimentResponses;

        public SentimentAnalyzer()
        {
            InitializeKeywords();
            InitializeSentimentResponses();
        }

        private void InitializeKeywords()
        {
            sentimentKeywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Worried sentiments (fear, anxiety)
                ["worried"] = "worried",
                ["scared"] = "worried",
                ["afraid"] = "worried",
                ["nervous"] = "worried",
                ["anxious"] = "worried",
                ["concerned"] = "worried",
                ["fear"] = "worried",
                ["unsafe"] = "worried",
                ["vulnerable"] = "worried",
                ["panic"] = "worried",
                ["terrified"] = "worried",
                ["stressed"] = "worried",

                // Frustrated sentiments (anger, confusion)
                ["frustrated"] = "frustrated",
                ["confused"] = "frustrated",
                ["difficult"] = "frustrated",
                ["hard"] = "frustrated",
                ["complicated"] = "frustrated",
                ["annoying"] = "frustrated",
                ["too much"] = "frustrated",
                ["overwhelmed"] = "frustrated",
                ["angry"] = "frustrated",
                ["hate"] = "frustrated",
                ["useless"] = "frustrated",

                // Curious sentiments (interest, learning)
                ["curious"] = "curious",
                ["interested"] = "curious",
                ["tell me"] = "curious",
                ["learn"] = "curious",
                ["explain"] = "curious",
                ["how does"] = "curious",
                ["what is"] = "curious",
                ["why"] = "curious",
                ["teach"] = "curious",
                ["educate"] = "curious",

                // Positive sentiments
                ["thank"] = "grateful",
                ["thanks"] = "grateful",
                ["helpful"] = "grateful",
                ["great"] = "positive",
                ["awesome"] = "positive",
                ["good"] = "positive"
            };
        }

        private void InitializeSentimentResponses()
        {
            sentimentResponses = new Dictionary<string, List<string>>
            {
                ["worried"] = new List<string>
                {
                    "😟 It's completely normal to feel worried about online security. Many South Africans share your concern. Let me share some simple protection steps with you.",
                    "😟 I understand your concern. Cybersecurity can feel intimidating, but I'm here to help make it simple for you. Here's what you need to know:",
                    "😟 Don't worry! Being concerned shows you're taking this seriously. Let me give you practical tips that will make you feel more secure."
                },
                ["frustrated"] = new List<string>
                {
                    "😤 I hear your frustration. Cybersecurity can seem overwhelming at first. Let's take it step by step - here's an easy starting point:",
                    "😤 I understand it feels complicated. Let me simplify this for you with one practical tip:",
                    "😤 You're not alone in feeling this way. Let me break this down into simple, actionable steps:"
                },
                ["curious"] = new List<string>
                {
                    "😊 That's wonderful! Curiosity is the first step to becoming cyber-safe. Here's an important tip to feed your curiosity:",
                    "😊 I love your enthusiasm for learning! Here's something valuable about cybersecurity:",
                    "😊 Great question! Your curiosity will help you stay safe online. Here's what you should know:"
                },
                ["grateful"] = new List<string>
                {
                    "🙏 You're very welcome! I'm glad I could help. Stay safe online! Would you like to learn about another topic?",
                    "🙏 My pleasure! Remember, staying informed is the best protection. What else would you like to know?",
                    "🙏 Thank you for using the Cybersecurity Awareness Bot. Together we can make South Africa more secure online!"
                },
                ["positive"] = new List<string>
                {
                    "😊 That's great to hear! Keeping a positive attitude about cybersecurity makes learning easier. What would you like to explore next?",
                    "😊 I'm glad you're enjoying this! Cybersecurity awareness is empowering. Shall we continue with another topic?",
                    "😊 Your positive approach is fantastic! Let's build on that with more cybersecurity knowledge."
                }
            };
        }

        public string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            foreach (var keyword in sentimentKeywords.Keys)
            {
                if (lowerInput.Contains(keyword.ToLower()))
                {
                    return sentimentKeywords[keyword];
                }
            }

            return "neutral";
        }

        public string GetSentimentResponse(string sentiment)
        {
            Random random = new Random();

            if (sentimentResponses.ContainsKey(sentiment))
            {
                var responses = sentimentResponses[sentiment];
                return responses[random.Next(responses.Count)];
            }

            return null;
        }

        public string AnalyzeAndRespond(string userInput)
        {
            string sentiment = DetectSentiment(userInput);
            if (sentiment != "neutral")
            {
                return GetSentimentResponse(sentiment);
            }
            return null;
        }
    }
}