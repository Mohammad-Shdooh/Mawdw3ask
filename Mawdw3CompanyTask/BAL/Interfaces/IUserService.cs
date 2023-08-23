using Entity.DTO_s;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        public UserResponse SignIn(logInRequest logIn);
        public UserResponse SignUp(User user);
        public Task<User> getUserFromExternalPublicSource();


    }
}
