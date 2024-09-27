using AutoMapper;
using CodeValidator.DAL.Models;
using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UsersDto, Users>().ReverseMap();
            CreateMap<Json, JsonDto>().ReverseMap();
            CreateMap<BaseToString, BaseToStringDto>().ReverseMap();
            CreateMap<StringToBase, StringToBaseDto>().ReverseMap();
            CreateMap<Workspaces,WorkspacesDto>().ReverseMap(); 
            CreateMap<Html, HtmlDto>().ReverseMap();
            CreateMap<Javascript, JavascriptDto>().ReverseMap();
            CreateMap<Jwt, JwtDto>().ReverseMap();
            CreateMap<Xml, XmlDto>().ReverseMap();
        }
    }
}
