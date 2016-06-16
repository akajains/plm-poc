using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace PLM.CmsService.Data
{
    public class MongoConnection : IDbConnection
    {
        IConfiguration config;
        public MongoConnection(IConfiguration config)
        {
            this.config = config;
        }

        public IMongoDatabase Connect
        {
            get
            {
                string connectionString = config["AppSettings:mongoDbConnection"];
                string dbName = config["AppSettings:databaseName"];
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(dbName);
                return database;             

            }
        }
    }
}
