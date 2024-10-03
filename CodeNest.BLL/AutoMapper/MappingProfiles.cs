using AutoMapper;
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;

namespace CodeNest.BLL.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UsersDto, Users>().ReverseMap();
            CreateMap<CustomJson, JsonDto>().ReverseMap();
            CreateMap<BaseToString, BaseToStringDto>().ReverseMap();
            CreateMap<StringToBase, StringToBaseDto>().ReverseMap();
            CreateMap<Workspaces, WorkspacesDto>().ReverseMap();
            CreateMap<CustomHtml, HtmlDto>().ReverseMap();
            CreateMap<CustomJavascript, JavascriptDto>().ReverseMap();
            CreateMap<Jwt, JwtDto>().ReverseMap();
            CreateMap<CustomXml, XmlDto>().ReverseMap();
        }
    }
}
