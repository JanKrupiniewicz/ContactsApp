using AutoMapper;

namespace ContactsApp.Server.Mappings
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Models.Users, Dtos.Users.RegisterUserDto>().ReverseMap();
            CreateMap<Models.Users, Dtos.Users.LoginUserDto>().ReverseMap();
        }
    }
}
