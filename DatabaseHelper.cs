using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CyberSecurityChatbot
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ChatbotDB"].ConnectionString;
            }
            catch
            {
                connectionString = "Server=localhost;Database=cyberchatbot_db;Trusted_Connection=True;Encrypt=False;";
            }
        }

        public string GetConnectionString()
        {
            return connectionString;
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Connection Error: " + ex.Message);
                return false;
            }
        }

        // =====================================================
        // TASK CRUD OPERATIONS
        // =====================================================

        public bool AddTask(string userName, string title, string description, DateTime? reminderDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO tasks (user_name, title, description, reminder_date) 
                                     VALUES (@userName, @title, @description, @reminderDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", (object)description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (AddTask): " + ex.Message);
                return false;
            }
        }

        public DataTable GetTasks(string userName, bool showCompleted = false)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT task_id, title, description, reminder_date, is_completed, created_at 
                                     FROM tasks 
                                     WHERE user_name = @userName";
                    if (!showCompleted)
                    {
                        query += " AND is_completed = 0";
                    }
                    query += " ORDER BY created_at DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (GetTasks): " + ex.Message);
                return new DataTable();
            }
        }

        public bool UpdateTask(int taskId, string title, string description, DateTime? reminderDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE tasks 
                                     SET title = @title, 
                                         description = @description, 
                                         reminder_date = @reminderDate
                                     WHERE task_id = @taskId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", (object)description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (UpdateTask): " + ex.Message);
                return false;
            }
        }

        public bool CompleteTask(int taskId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE tasks SET is_completed = 1, completed_at = GETDATE() 
                                     WHERE task_id = @taskId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (CompleteTask): " + ex.Message);
                return false;
            }
        }

        public bool DeleteTask(int taskId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tasks WHERE task_id = @taskId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@taskId", taskId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (DeleteTask): " + ex.Message);
                return false;
            }
        }

        // =====================================================
        // ACTIVITY LOG OPERATIONS
        // =====================================================

        public bool LogActivity(string userName, string actionType, string description)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO activity_log (user_name, action_type, description, timestamp) 
                                     VALUES (@userName, @actionType, @description, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@actionType", actionType);
                    cmd.Parameters.AddWithValue("@description", description);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (LogActivity): " + ex.Message);
                return false;
            }
        }

        public DataTable GetActivityLog(string userName, int limit = 10)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT TOP (@limit) log_id, action_type, description, timestamp 
                                     FROM activity_log 
                                     WHERE user_name = @userName 
                                     ORDER BY timestamp DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (GetActivityLog): " + ex.Message);
                return new DataTable();
            }
        }

        public DataTable GetFullActivityLog(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT log_id, action_type, description, timestamp 
                                     FROM activity_log 
                                     WHERE user_name = @userName 
                                     ORDER BY timestamp DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (GetFullActivityLog): " + ex.Message);
                return new DataTable();
            }
        }

        // =====================================================
        // QUIZ OPERATIONS
        // =====================================================

        public bool SaveQuizResult(string userName, int score, int totalQuestions)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    double percentage = ((double)score / totalQuestions) * 100;
                    string query = @"INSERT INTO quiz_results (user_name, score, total_questions, percentage) 
                                     VALUES (@userName, @score, @totalQuestions, @percentage)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@totalQuestions", totalQuestions);
                    cmd.Parameters.AddWithValue("@percentage", percentage);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (SaveQuizResult): " + ex.Message);
                return false;
            }
        }

        public DataTable GetQuizHistory(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT result_id, score, total_questions, percentage, attempt_date 
                                     FROM quiz_results 
                                     WHERE user_name = @userName 
                                     ORDER BY attempt_date DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database Error (GetQuizHistory): " + ex.Message);
                return new DataTable();
            }
        }
    }
}