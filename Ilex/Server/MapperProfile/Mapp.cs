using AutoMapper;
using Ilex.Shared.ModelDTOs.Account;
using Ilex.Shared.Models;

namespace Ilex.Server.MapperProfile
{
    public class Mapp : Profile
    {
        public Mapp()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
        }
    }
}
