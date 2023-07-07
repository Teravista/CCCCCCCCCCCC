using Project3.Models;

namespace Project3.Repositories
{
    public class DbSubCategoryRepository : ISubCategoryRepository
    {
        public void CreateItem(SubCategoryModel item)
        {
            using (var context = new AppDbContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public IEnumerable<SubCategoryModel> GetItems()
        {
            var subCategories = new List<SubCategoryModel>();
            using (var context = new AppDbContext())
            {
                subCategories = context.SubCategories.ToList();
            }
            return subCategories;
        }
        public void Populate()
        {
            var subCategory1 = new SubCategoryModel() { SubCategory = "szef" };
            var subCategory2 = new SubCategoryModel() { SubCategory = "klient" };
            var subCategory3 = new SubCategoryModel() { SubCategory = "stazysta" };
            using (var context = new AppDbContext())
            {
                context.Add(subCategory1);
                context.Add(subCategory2);
                context.Add(subCategory3);
                context.SaveChanges();
            }
        }
    }
}
