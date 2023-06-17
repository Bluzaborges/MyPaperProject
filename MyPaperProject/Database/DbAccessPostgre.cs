using MyPaperProject.Global;
using Npgsql;

namespace MyPaperProject.Database
{
    public class DbAccessPostgre
    {
        public NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection result = new NpgsqlConnection();

            try
            {
                string connectionString = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
                                                  Config.dbHost, Config.dbPort, Config.dbName, Config.dbUser, Config.dbPassword);

                result = new NpgsqlConnection(connectionString);
                result.Open();
            }
            catch (Exception ex)
            {
                Log.Add(LogType.error, "[DbAccess.OpenConnection]: " + ex.Message);
            }

            return result;
        }
    }
}
