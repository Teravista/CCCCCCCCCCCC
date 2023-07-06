using Deparamtes;
using Project1.Models;

namespace Project1.Repositories
{
    public interface IEmployeeRepository
    {
        EmployeeModel GetItem(int id);
        IEnumerable<EmployeeModel> GetItems();
        void CreateItem(EmployeeModel item);
        void UpdateItem(EmployeeModel item);
        void DeleteItem(int id);
    }
}
