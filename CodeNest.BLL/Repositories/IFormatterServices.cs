using CodeNest.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeNest.BLL.Repositories
{
     public interface IFormatterServices 
    {
        Task<ValidationDto> JsonValidate(string jsonObject);
    }
}
