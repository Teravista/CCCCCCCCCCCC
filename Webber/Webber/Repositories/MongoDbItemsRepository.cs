using MongoDB.Bson;
using MongoDB.Driver;
using Webber.Entites;

namespace Webber.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "dbName";
        private const string collectionName = "collectionName";
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        private readonly IMongoCollection<Item> itemsCollection;
        public MongoDbItemsRepository(IMongoClient mongoCleint)
        {
            IMongoDatabase database = mongoCleint.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingIem => existingIem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}