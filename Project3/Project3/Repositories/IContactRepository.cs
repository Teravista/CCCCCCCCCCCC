

namespace Project3.Repositories
{
    public interface IContactRepository
    {
        ContactModel GetItem(int id);
        IEnumerable<ContactModel> GetItems();
        bool CreateItem(ContactModel item);
        void UpdateItem(ContactModel item);
        void DeleteItem(int id);
    }
}
