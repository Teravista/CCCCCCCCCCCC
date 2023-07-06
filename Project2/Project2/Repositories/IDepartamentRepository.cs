using Deparamtes;
using Project1.Models;

namespace Project1.Repositories
{
    public interface IDepartamentRepository
    {
        DepartmentModel GetItem(int id);
        IEnumerable<DepartmentModel> GetItems();
        void CreateItem(DepartmentModel item);
        void UpdateItem(DepartmentModel item);
        void DeleteItem(int id);
    }
}
