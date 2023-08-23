using Entity.DTO_s;
using Entity.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public UserResponse SignIn(logInRequest logIn);
        public UserResponse SignUp(User user);
        public User isUniqeEmail(string email);
        public Task<User> getUserFromExternalPublicSource();


    }
}
