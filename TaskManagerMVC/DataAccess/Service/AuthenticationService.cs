namespace DataAccess.Service
{
    using Entities;
    using Repositories;

    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public void AuthenticateUser(string username, string password)
        {
            UserRepo userRepo = RepositoryFactory.GetUsersRepository();
            LoggedUser = userRepo.GetAll().Find(u => u.Username == username && u.Password == password);
        }
    }
}