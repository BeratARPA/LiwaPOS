using AutoMapper;
using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.Entities.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppAction, AppActionDTO>().ReverseMap();
            CreateMap<AppRule, AppRuleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
        }
    }
}
