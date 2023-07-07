using Microsoft.AspNetCore.Mvc;
using Project3.Dtos;
using Project3.Repositories;

namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryRepository categoryRep;

        public CategoryController(ICategoryRepository categoryRep)
        {
            this.categoryRep = categoryRep;
        }
        // Get /category
        [HttpGet]
        public IEnumerable<CategoryDto> GetItems()
        {
            var items = categoryRep.GetItems().Select(item => item.AsCategoryDto());
            if (items.Count() == 0)
            {
                this.categoryRep.Populate();
                items = categoryRep.GetItems().Select(item => item.AsCategoryDto());
            }
            return items;
        }
        
    }
}
