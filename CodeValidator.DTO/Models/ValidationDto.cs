using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValidator.DTO.Models
{
    public class ValidationDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } 
        public JsonModelDto jsonModelDto { get; set; }

    }
}
