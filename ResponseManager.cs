using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class ResponseManager
    {
        private Dictionary<string, List<string>> keywordResponses;
        private Random random;
        private List<string> defaultResponses;

        public ResponseManager()
        {
            random = new Random();
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            // Password tips
            keywordResponses["password"] = new List<string>
            {
                "🔐 Create passwords that are at least 12 characters long with a mix of uppercase, lowercase, numbers, and symbols!",
                "🔐 Never reuse passwords across different accounts. Each account needs its own unique password!",
                "🔐 Consider using a password manager like Bitwarden or LastPass to generate and store strong passwords!",
                "🔐 Enable Two-Factor Authentication (2FA) for an extra layer of security beyond just your password!",
                "🔐 Avoid using common words like 'password', 'admin', or '123456' - these are the first ones hackers try!",
                "🔐 Change your passwords every 3-6 months, especially for important accounts like banking and email!"
            };

            // Phishing tips
            keywordResponses["phish"] = new List<string>
            {
                "🎣 Always verify the sender's email address - scammers use addresses that look similar to real ones (e.g., @amaz0n.com instead of @amazon.com)!",
                "🎣 Never click on links in suspicious emails. Hover to see the actual URL first!",
                "🎣 Legitimate companies never ask for passwords or OTPs via email or phone!",
                "🎣 Look for urgent language like 'Your account will be closed' - that's a common scam tactic!",
                "🎣 In South Africa, be especially wary of SMS phishing ('smishing') claiming to be from banks like Capitec, FNB, or Standard Bank!",
                "🎣 If an email creates a sense of panic or urgency, stop and verify through official channels first!"
            };

            // Scam tips
            keywordResponses["scam"] = new List<string>
            {
                "⚠️ If an offer sounds too good to be true, it definitely is! Scammers promise huge rewards to trap victims.",
                "⚠️ Never share your OTP (One-Time Pin) with anyone, even if they claim to be from your bank!",
                "⚠️ Be cautious of calls claiming you've won a prize you never entered. Hang up and verify directly!",
                "⚠️ Don't click on pop-up ads warning about computer viruses - these are often scams to install malware!",
                "⚠️ The 'Mum and Dad' scam is common in SA - scammers pretend to be family members in trouble. Always verify through another channel!",
                "⚠️ Never pay 'fees' to claim a prize - legitimate lotteries don't ask for money upfront!"
            };

            // Privacy tips
            keywordResponses["privacy"] = new List<string>
            {
                "🛡️ Review your social media privacy settings regularly. Limit what personal information is public!",
                "🛡️ Be careful what you share online - cybercriminals use personal info for targeted attacks!",
                "🛡️ Use different email addresses for different purposes (shopping, banking, social media)!",
                "🛡️ Regularly check which apps have access to your accounts and remove unused ones!",
                "🛡️ In South Africa, POPIA (Protection of Personal Information Act) gives you rights over your data - know them!",
                "🛡️ Think before posting: birthdays, addresses, and travel plans can be used against you!"
            };

            // Safe browsing tips
            keywordResponses["browsing"] = new List<string>
            {
                "🌐 Look for 'https://' and the padlock icon before entering sensitive information on any website!",
                "🌐 Keep your browser and antivirus software updated to protect against known vulnerabilities!",
                "🌐 Consider using browser extensions like HTTPS Everywhere and uBlock Origin for extra protection!",
                "🌐 Use a VPN when connecting to public Wi-Fi at malls, airports, or coffee shops to encrypt your traffic!",
                "🌐 Clear your browser cache and cookies regularly to remove tracking data!",
                "🌐 Be careful with browser extensions - only install from official stores and check permissions!"
            };

            // How are you responses
            keywordResponses["how are you"] = new List<string>
            {
                "I'm functioning perfectly! Ready to help you learn about cybersecurity in South Africa!",
                "All systems operational! How can I assist you with online safety today?",
                "Doing great! Cybersecurity is my passion, so I'm always happy to chat about it!",
                "I'm excellent! Another day of helping South Africans stay safe online!"
            };

            // Purpose responses
            keywordResponses["purpose"] = new List<string>
            {
                "My purpose is to educate South African citizens about cybersecurity threats and help them stay safe online. I was created as part of a national cybersecurity awareness campaign!"
            };

            // Greeting responses
            keywordResponses["hello"] = new List<string>
            {
                "Hello there! Ready to learn about cybersecurity today?",
                "Hi! How can I help you stay safe online?",
                "Greetings! What cybersecurity topic would you like to explore?",
                "Hey there! I'm your cybersecurity assistant. What can I teach you today?"
            };

            // Default responses for when no keyword matches
            defaultResponses = new List<string>
            {
                "I didn't quite understand that. Could you rephrase? Try asking about password safety, phishing, scams, or safe browsing!",
                "Hmm, I'm not sure about that. Would you like to learn about cybersecurity topics like password safety or phishing prevention?",
                "I'm still learning! Could you ask me about cybersecurity topics such as safe browsing, privacy, or scam prevention?",
                "That's outside my knowledge area. Let me help you with cybersecurity instead! Ask me about passwords, phishing, or online safety tips."
            };
        }

        public string GetResponse(string userInput)
        {
            string lowerInput = userInput.ToLower();

            // Check each keyword
            foreach (string keyword in keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword.ToLower()))
                {
                    List<string> responses = keywordResponses[keyword];
                    return responses[random.Next(responses.Count)];
                }
            }

            // Return a random default response
            return defaultResponses[random.Next(defaultResponses.Count)];
        }
    }
}