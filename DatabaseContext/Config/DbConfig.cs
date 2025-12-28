namespace DatabaseContext.Config
{
    public static class DbConfig
    {
        const string host = "localhost";
        const string port = "5432";
        const string database = "univdb";
        const string username = "postgres";
        const string passwd = "passwd";

        public static string GetConnectionString() => $"Host={host};Port={port};Database={database};Username={username};Password={passwd}";
    }
}

