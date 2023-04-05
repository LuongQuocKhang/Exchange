using Exchange.Data.Entities;
using MongoDB.Driver;

namespace Exchange.Data.Data
{
    public class SignalContext : ISignalContext
    {
        public SignalContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Signals = database.GetCollection<Signals>(configuration.GetValue<string>("MongoCollection:Signal"));
        }

        public IMongoCollection<Signals> Signals { get; }
    }
}
