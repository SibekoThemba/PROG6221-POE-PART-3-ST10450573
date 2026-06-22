using System.Data;

namespace CyberSecurityChatbot
{
    public class ActivityLogger
    {
        private DatabaseHelper db;

        public ActivityLogger(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }

        public bool LogActivity(string userName, string actionType, string description)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = "UnknownUser";
            }
            return db.LogActivity(userName, actionType, description);
        }

        public DataTable GetActivityLog(string userName, int limit = 10)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = "UnknownUser";
            }
            return db.GetActivityLog(userName, limit);
        }

        public DataTable GetFullActivityLog(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = "UnknownUser";
            }
            return db.GetFullActivityLog(userName);
        }
    }
}