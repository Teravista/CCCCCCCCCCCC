using Deparamtes;
using Project1.Models;

namespace Project1.Repositories
{
    public interface IDepartamentRepository
    {
        ContactsModel GetItem(int id);
        IEnumerable<ContactsModel> GetItems();
        void CreateItem(ContactsModel item);
        void UpdateItem(ContactsModel item);
        void DeleteItem(int id);
    }
}
