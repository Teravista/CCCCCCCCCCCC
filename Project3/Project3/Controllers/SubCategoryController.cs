using Microsoft.AspNetCore.Mvc;
using Project3.Dtos;
using Project3.Repositories;

namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository subCategoryRep;

        public SubCategoryController(ISubCategoryRepository subCategoryRep)
        {
            this.subCategoryRep = subCategoryRep;
        }
        // GET /subCategory
        [HttpGet]
        public IEnumerable<SubCategoryDto> GetItems()
        {
            var items = subCategoryRep.GetItems().Select(item => item.AsSubCategoryDto());
            if (items.Count() == 0)
            {
                this.subCategoryRep.Populate();
                items = subCategoryRep.GetItems().Select(item => item.AsSubCategoryDto());
            }
            return items;
        }
    }
}
