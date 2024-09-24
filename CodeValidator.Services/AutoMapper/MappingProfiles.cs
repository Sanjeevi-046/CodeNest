using AutoMapper;
using CodeValidator.DAL.Models;
using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.AutoMapper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<JsonModel,JsonModelDto>().ReverseMap();   
        }
    }
}
