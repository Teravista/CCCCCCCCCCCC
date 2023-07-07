using Project3.Models;

namespace Project3.Repositories
{
    public interface ILoginRepository
    {
        int Login(LoginModel login);
        //populate database in case of empty table
        void Populate();
    }
}
