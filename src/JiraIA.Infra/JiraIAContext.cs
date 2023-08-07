using JiraIA.Infra.Providers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JiraIA.Infra
{
    public class JiraIAContext
    {
        private readonly IMongoClient _mongoClient;

        public IMongoDatabase MongoDatabase { get; private set; }

        public JiraIAContext(IOptions<DbSettingsProvider> dbSettingsProvider, IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            MongoDatabase = _mongoClient.GetDatabase(dbSettingsProvider.Value.DatabaseName);
        }
    }
}
