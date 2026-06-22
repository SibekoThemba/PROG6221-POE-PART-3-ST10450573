using System;
using System.Data;

namespace CyberSecurityChatbot
{
    public class TaskManager
    {
        private DatabaseHelper db;
        private ActivityLogger logger;

        public TaskManager(DatabaseHelper databaseHelper, ActivityLogger activityLogger)
        {
            db = databaseHelper;
            logger = activityLogger;
        }

        public bool AddTask(string userName, string title, string description, DateTime? reminderDate)
        {
            bool result = db.AddTask(userName, title, description, reminderDate);
            if (result)
            {
                string logMsg = $"Added task: '{title}'";
                if (reminderDate.HasValue)
                    logMsg += $" with reminder on {reminderDate.Value.ToShortDateString()}";
                logger.LogActivity(userName, "Task_Added", logMsg);
            }
            return result;
        }

        public DataTable GetTasks(string userName, bool showCompleted = false)
        {
            return db.GetTasks(userName, showCompleted);
        }

        public bool CompleteTask(int taskId)
        {
            return db.CompleteTask(taskId);
        }

        public bool DeleteTask(int taskId)
        {
            return db.DeleteTask(taskId);
        }
    }
}