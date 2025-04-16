using AutoMapper;

namespace ContactsApp.Server.Mappings
{
    /// <summary>
    /// Klasa mapująca dla użytkowników.
    /// </summary>
    public class UsersProfile : Profile
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="UsersProfile"/> i definiuje mapowania między modelami a DTO.
        /// </summary>
        public UsersProfile()
        {
            CreateMap<Models.Users, Dtos.Users.RegisterUserDto>().ReverseMap();
            CreateMap<Models.Users, Dtos.Users.LoginUserDto>().ReverseMap();
        }
    }
}
