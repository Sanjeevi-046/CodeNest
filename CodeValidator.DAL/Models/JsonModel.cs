using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValidator.DAL.Models
{
    public class JsonModel
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;
        
        public string JsonInput { get; set; }
        public string BeautifiedJson { get; set; }

    }
}
