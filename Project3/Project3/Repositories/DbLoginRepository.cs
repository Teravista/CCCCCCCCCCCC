using Project3.Models;

namespace Project3.Repositories
{
    public class DbLoginRepository : ILoginRepository
    {
        public int Login(LoginModel login)
        {
            var logins = new List<LoginModel>();
            using (var context = new AppDbContext())
            {
                logins = context.Logins.ToList();
            }
            if (logins.Count() == 0)
            {
                this.Populate();
            }
            var present = new LoginModel();
            using (var context = new AppDbContext())
            {
                present = context.Logins.FirstOrDefault(i => i.Login == login.Login&&i.Password==login.Password);
            }
            if (present is null)
                return 0;
            else
                return 1;
        }

        public void Populate()
        {
            var subCategory1 = new LoginModel() { Login = "admin",Password="admin" };
            var subCategory2 = new LoginModel() { Login = "student",Password="student" };
            var subCategory3 = new LoginModel() { Login = "kocham",Password="ako" };
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
