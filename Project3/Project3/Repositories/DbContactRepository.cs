namespace Project3.Repositories
{
    public class DbContactRepository : IContactRepository
    {
        public bool CreateItem(ContactModel item)
        {
            using (var context = new AppDbContext())
            {
                var contact = new ContactModel();
                contact = context.Contacts.FirstOrDefault(i => i.Email == item.Email);
                if(contact != null)
                {
                    return false;
                }

                context.Add(item);
                context.SaveChanges();
                return true;
            }
        }

        public void DeleteItem(int id)
        {
            ContactModel customer = new ContactModel() { Id = id };
            using (var context = new AppDbContext())
            {
                context.Contacts.Attach(customer);
                context.Contacts.Remove(customer);
                context.SaveChanges();
            }
            
        }

        public ContactModel GetItem(int id)
        {
            var contacts = new ContactModel();
            using (var context = new AppDbContext())
            {
                contacts = context.Contacts.FirstOrDefault(i=>i.Id == id);
            }
            return contacts;
        }

        public  IEnumerable<ContactModel> GetItems()
        {
            var contacts = new List<ContactModel>();
            using (var context = new AppDbContext())
            {
                contacts =  context.Contacts.ToList();
            }
            return contacts;
        }

        public void UpdateItem(ContactModel item)
        {
            using (var context = new AppDbContext())
            {
                var result = context.Contacts.SingleOrDefault(b => b.Email == item.Email);
                if (result != null)
                {
                    context.Entry(result).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
