using Project3.Models;

namespace Project3.Repositories
{
    public class DbCategoryRepository : ICategoryRepository
    {
        public void CreateItem(CategoryModel item)
        {
            using (var context = new AppDbContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public IEnumerable<CategoryModel> GetItems()
        {
            var categories = new List<CategoryModel>();
            using (var context = new AppDbContext())
            {
                categories = context.Categories.ToList();
            }
            return categories;
        }

        public void Populate()
        {
           var category1 = new CategoryModel() {Category="sluzbowy" };
           var category2 = new CategoryModel() {Category="prywatny" };
           var category3 = new CategoryModel() {Category="inny" };
            using (var context = new AppDbContext())
            {
                context.Add(category1);
                context.Add(category2);
                context.Add(category3);
                context.SaveChanges();
            }
        }
    }
}
