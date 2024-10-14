using AutoMapper;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;

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
            CreateMap<Script, ScriptDTO>().ReverseMap();
        }
    }
}
