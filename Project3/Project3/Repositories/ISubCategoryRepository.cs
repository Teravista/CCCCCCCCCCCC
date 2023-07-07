using Project3.Models;

namespace Project3.Repositories
{
    public interface ISubCategoryRepository
    {
        IEnumerable<SubCategoryModel> GetItems();
        void CreateItem(SubCategoryModel item);
        //populate database in case of empty table
        void Populate();
    }
}
