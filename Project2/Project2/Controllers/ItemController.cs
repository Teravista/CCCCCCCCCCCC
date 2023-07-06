using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using System.Linq;

namespace Project1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private static readonly IEnumerable<ItemModel> Items = new[]
        {
            new ItemModel { Id = 1,Title="the godfather",ImageId=1,Ranking=0,ItemType=0},
            new ItemModel { Id = 2,Title="the madfather",ImageId=2,Ranking=0,ItemType=0},
            new ItemModel { Id = 3,Title="the mamasita",ImageId=3,Ranking=0,ItemType=1},
            new ItemModel { Id = 4,Title=" sonsita",ImageId=4,Ranking=0,ItemType=1},
        };
        [HttpGet("{itemType:int}")]
        public ItemModel[] Get(int itemType)
        {
            ItemModel[] items = Items.Where(i => i.ItemType == itemType).ToArray();
            System.Threading.Thread.Sleep(2000);
            return items;
        }
    }
}
