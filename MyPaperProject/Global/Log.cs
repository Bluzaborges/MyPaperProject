using MyPaperProject.Database;
using Npgsql;
using System.Security.Cryptography;

namespace MyPaperProject.Global
{
    public static class Log
    {
        public static void Add(LogType type, string message)
        {
            try
            {
                DbAccess db = new DbAccess();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = @"INSERT INTO logs (creation_date, type, message) " +
                                      @"VALUES (@creationDate, @type, @message);";

                    cmd.Parameters.AddWithValue("@creationDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@type", type.ToString());
                    cmd.Parameters.AddWithValue("@message", message);

                    using (cmd.Connection = db.OpenConnection())
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            } catch (Exception ex)
            {
                SaveErrorLog(ex.Message);
            }
        }

        private static void SaveErrorLog(string message)
        {
            string errorLogFileName = "errorLog.txt";

            if (!File.Exists(Path.Combine(Config.baseDirectory, errorLogFileName)))
                File.Create(Path.Combine(Config.baseDirectory, errorLogFileName)).Dispose();

            File.AppendAllText(Path.Combine(Config.baseDirectory, errorLogFileName), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - error : " + message + Environment.NewLine);
        }
    }
}
