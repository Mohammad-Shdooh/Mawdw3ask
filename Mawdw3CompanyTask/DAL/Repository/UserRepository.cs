using AutoMapper;
using DAL.Interfaces;
using Entity.DTO_s;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace DAL.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly Mawdw3TaskContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        public UserRepository(Mawdw3TaskContext context,
            IMapper mapper,
            IConfiguration config )
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }
        #region ~SignIn~
        public UserResponse SignIn(logInRequest logIn)
        {
            try { 
            User user = context.Users.FirstOrDefault(usr => usr.Email == logIn.Email && usr.password == logIn.password);
            if( user == null)
                {
                    throw new NotFoundException("Uncorrect Email or Password ");
                }
                UserResponse response = mapper.Map<UserResponse>(user);
            return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ~SignUp~
        public UserResponse SignUp(User user)
        {
            try
            {
                // insert obj and take it's own id after saving changes .
                User userEntity = context.Users.Add(user).Entity;
                context.SaveChanges();
                // i mapped the object because the user that return have a password 
                // and i can't show the password .
                UserResponse userObj = mapper.Map<UserResponse>(userEntity);
                return userObj;
            }
            catch (Exception ex)
            {
                throw new Exception("There are some technical issues .");
            }
        }

        public User isUniqeEmail(string email)
        {
            User user = context.Users.SingleOrDefault(x => x.Email == email);
            return user;
        }
        #endregion

        public async Task<User> getUserFromExternalPublicSource()
        {
            try { 
            string apiUrl = config["publicApi"];
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                     var jsonEntity = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string Name = jsonEntity.results[0].name.first+
                        " "+
                        jsonEntity.results[0].name.last;
                    string Email = jsonEntity.results[0].email;
                    string password = jsonEntity.results[0].login.password;
                    User user = new User
                    {
                        Name = Name,
                        Email = Email,
                        password = password,
                    };
                    return user;
                }
                    throw new BadRequestException("Api not responed");              
            }
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

    }
}
