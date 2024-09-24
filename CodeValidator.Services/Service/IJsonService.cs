using CodeValidator.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CodeValidator.BLL.Service
{
    public interface IJsonService
    {

        Task<ValidationDto>Validate(string jsonObject);
    }
}
