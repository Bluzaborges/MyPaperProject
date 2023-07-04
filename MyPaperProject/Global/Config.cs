namespace MyPaperProject.Global
{
    public static class Config
    {
        //Database
        public static string dbHost = null;
        public static string dbPort = null;
        public static string dbName = null;
        public static string dbUser = null;
        public static string dbPassword = null;

        //Diretório base da aplicação
        public static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

		//Diretório de upload de arquivos temporários
		public static string uploadFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\temp-files");

		public static bool LoadAppSettings()
        {
            bool result = false;

            try
            {
                IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                dbHost = config.GetValue<string>("Database:Host");
                dbPort = config.GetValue<string>("Database:Port");
                dbName = config.GetValue<string>("Database:Name");
                dbUser = config.GetValue<string>("Database:User");
                dbPassword = config.GetValue<string>("Database:Password");

                result = true;

            } catch
            { }

            return result;
        }
    }

    public enum LogType
    {
        success,
        info,
        error
    }
}
