using System;
using System.Data;

namespace CyberSecurityChatbot
{
    public class QuizManager
    {
        private DatabaseHelper db;
        private ActivityLogger logger;

        public QuizManager(DatabaseHelper databaseHelper, ActivityLogger activityLogger)
        {
            db = databaseHelper;
            logger = activityLogger;
        }

        public bool SaveQuizResult(string userName, int score, int totalQuestions)
        {
            bool result = db.SaveQuizResult(userName, score, totalQuestions);
            if (result)
            {
                logger.LogActivity(userName, "Quiz_Saved", $"Quiz result saved: {score}/{totalQuestions}");
            }
            return result;
        }

        public DataTable GetQuizHistory(string userName)
        {
            return db.GetQuizHistory(userName);
        }
    }
}