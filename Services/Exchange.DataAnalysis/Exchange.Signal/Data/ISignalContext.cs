using Exchange.Data.Entities;
using MongoDB.Driver;

namespace Exchange.Data.Data
{
    public interface ISignalContext
    {
        public IMongoCollection<Signals> Signals { get; }
    }
}
