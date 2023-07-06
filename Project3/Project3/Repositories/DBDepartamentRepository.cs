using Deparamtes;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Repositories
{
    public class DBDepartamentRepository : IDepartamentRepository
    {
        public void CreateItem(ContactsModel item)
        {
            using (var context = new AppDbContext())
            {
                context.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            ContactsModel customer = new ContactsModel() { Id = id };
            using (var context = new AppDbContext())
            {
                context.Departamentos.Attach(customer);
                context.Departamentos.Remove(customer);
                context.SaveChanges();
            }
            
        }

        public ContactsModel GetItem(int id)
        {
            var department = new ContactsModel();
            using (var context = new AppDbContext())
            {
                department = context.Departamentos.FirstOrDefault(i=>i.Id==id);
            }
            return department;
        }

        public  IEnumerable<ContactsModel> GetItems()
        {
            var departments = new List<ContactsModel>();
            using (var context = new AppDbContext())
            {
                departments =  context.Departamentos.ToList();
            }
            return departments;
        }

        public void UpdateItem(ContactsModel item)
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
