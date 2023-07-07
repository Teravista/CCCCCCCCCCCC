using Project3.Models;

namespace Project3.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetItems();
        void CreateItem(CategoryModel item);
        //populate database in case of empty table
        void Populate();
    }
}
