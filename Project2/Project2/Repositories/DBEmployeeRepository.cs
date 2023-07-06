using Deparamtes;
using Project1.Models;

namespace Project1.Repositories
{
    public class DBEmployeeRepository : IEmployeeRepository
    {
        public void CreateItem(EmployeeModel item)
        {
            using (var context = new AppDbContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            EmployeeModel customer = new EmployeeModel() { Id = id };
            using (var context = new AppDbContext())
            {
                context.Employess.Attach(customer);
                context.Employess.Remove(customer);
                context.SaveChanges();
            }

        }

        public EmployeeModel GetItem(int id)
        {
            var department = new EmployeeModel();
            using (var context = new AppDbContext())
            {
                department = context.Employess.FirstOrDefault(i => i.Id == id);
            }
            return department;
        }

        public IEnumerable<EmployeeModel> GetItems()
        {
            var departments = new List<EmployeeModel>();
            using (var context = new AppDbContext())
            {
                departments = context.Employess.ToList();
            }
            return departments;
        }

        public void UpdateItem(EmployeeModel item)
        {
            using (var context = new AppDbContext())
            {
                var result = context.Employess.SingleOrDefault(b => b.Id == item.Id);
                if (result != null)
                {
                    context.Entry(result).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
