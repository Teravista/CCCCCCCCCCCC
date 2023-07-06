using Webber.Entites;
namespace Webber.Repositories
{


    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item{Id=Guid.NewGuid(),Name = "Bread",Price = 420,CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(),Name = "Butter",Price = 2,CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(),Name = "Cheeeese",Price = 200,CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(),Name = "Void",Price = 13,CreatedDate=DateTimeOffset.UtcNow}
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {

            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}