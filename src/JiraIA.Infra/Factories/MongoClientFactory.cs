using JiraIA.Infra.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JiraIA.Infra.Factories
{
    public static class MongoClientFactory
    {
        public static MongoClient Create(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<IOptions<ConnectionStringsProvider>>();
            Console.WriteLine($"*************************************************************************{config.Value.MongoDB}");
            MongoClient client = new(config.Value.MongoDB);
            return client;
        }
    }
}
