using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValidator.DTO.Models
{
    public class JsonModelDto
    {
        public string? Id { get; set; }
        public string? JsonInput { get; set; }
        public string? BeautifiedJson { get; set; }
    }
}
