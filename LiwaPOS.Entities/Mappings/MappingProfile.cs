using AutoMapper;
using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.Entities.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Otomasyon
            CreateMap<AppAction, AppActionDTO>().ReverseMap();
            CreateMap<AppRule, AppRuleDTO>().ReverseMap();
            CreateMap<RuleActionMap, RuleActionMapDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
        }
    }
}
