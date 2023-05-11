namespace SpinGameApp
{
    public class MongoDbSettings
    {
        public MongoDbSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DbName = databaseName;
        }

        public string ConnectionString { get; }
        public string DbName { get; }
    }
}