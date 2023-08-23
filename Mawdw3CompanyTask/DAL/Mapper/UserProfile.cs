using AutoMapper;
using Entity.DTO_s;
using Entity.Models;

namespace DAL.Mapper
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, User>().ReverseMap();
        }
    }
}
