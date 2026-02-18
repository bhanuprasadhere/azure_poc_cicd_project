using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public MongoService(IConfiguration config)
        {
            var connectionString = config["MongoDbSettings:ConnectionString"];
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(config["MongoDbSettings:DatabaseName"]);

            _itemsCollection = mongoDatabase.GetCollection<Item>("Items");
        }

        public async Task<List<Item>> GetAsync() =>
            await _itemsCollection.Find(_ => true).ToListAsync();

        public async Task<Item?> GetAsync(string id) =>
            await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Item newItem) =>
            await _itemsCollection.InsertOneAsync(newItem);

        public async Task UpdateAsync(string id, Item updatedItem) =>
            await _itemsCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task RemoveAsync(string id) =>
            await _itemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}