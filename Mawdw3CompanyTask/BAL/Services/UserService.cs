using BAL.Interfaces;
using DAL.Interfaces;
using Entity.DTO_s;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepo;
        public UserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        #region ~SignIn~
        public UserResponse SignIn(logInRequest logIn)
        {
            logIn.password = HashedPassword(logIn.password);
            UserResponse user = userRepo.SignIn(logIn);
            return user;
        }
        #endregion
        #region ~SignUp~ 
        public UserResponse SignUp(User user)
        {
            if (isValidDTO(user) && isUniqeEmail(user.Email))
            {
                user.password = HashedPassword(user.password);
                UserResponse responseUser = userRepo.SignUp(user);
                return responseUser;

            }
            return null;
        }
        public bool isValidDTO(User user)
        {
            string pattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            string input = user.Email;
            Match mEmail = Regex.Match(input, pattern);


            if (mEmail.Success)
                return !string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.password);
            return false;
        }
        public Boolean isUniqeEmail(string email)
        {
            User user = userRepo.isUniqeEmail(email);
            if (user != null)
                return false;
            return true;
        }
        #endregion
        public string HashedPassword(string pass)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(pass);
            var hashedPass = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPass);
        }


        public  Task<User> getUserFromExternalPublicSource()
        {
            return userRepo.getUserFromExternalPublicSource();
        }
       
    }
}
