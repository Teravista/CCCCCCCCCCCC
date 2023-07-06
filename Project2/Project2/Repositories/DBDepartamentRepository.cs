using Deparamtes;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Repositories
{
    public class DBDepartamentRepository : IDepartamentRepository
    {
        public void CreateItem(DepartmentModel item)
        {
            using (var context = new AppDbContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            DepartmentModel customer = new DepartmentModel() { Id = id };
            using (var context = new AppDbContext())
            {
                context.Departamentos.Attach(customer);
                context.Departamentos.Remove(customer);
                context.SaveChanges();
            }
            
        }

        public DepartmentModel GetItem(int id)
        {
            var department = new DepartmentModel();
            using (var context = new AppDbContext())
            {
                department = context.Departamentos.FirstOrDefault(i=>i.Id==id);
            }
            return department;
        }

        public  IEnumerable<DepartmentModel> GetItems()
        {
            var departments = new List<DepartmentModel>();
            using (var context = new AppDbContext())
            {
                departments =  context.Departamentos.ToList();
            }
            return departments;
        }

        public void UpdateItem(DepartmentModel item)
        {
            using (var context = new AppDbContext())
            {
                var result = context.Departamentos.SingleOrDefault(b => b.Id == item.Id);
                if (result != null)
                {
                    context.Entry(result).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
