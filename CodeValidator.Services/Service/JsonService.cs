using CodeValidator.DTO.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace CodeValidator.BLL.Service
{
    public class JsonService : IJsonService
    {
        public async Task<ValidationDto> Validate(string jsonObject)
        {
            if (string.IsNullOrWhiteSpace(jsonObject)) 
            { 
                return new ValidationDto { IsValid=false , Message="Not Valid Json"}; 
            }
            jsonObject = jsonObject.Trim();
            if ((jsonObject.StartsWith("{") && jsonObject.EndsWith("}")) || //For object
                (jsonObject.StartsWith("[") && jsonObject.EndsWith("]"))) //For array
            {

                try
                {
                    var parsedJson = JToken.Parse(jsonObject);  

                    string beautifiedJson = parsedJson.ToString(Formatting.Indented);

                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        jsonModelDto = new JsonModelDto
                        {
                            JsonInput = jsonObject,
                            BeautifiedJson = beautifiedJson
                        }
                    };
                }
                catch (JsonReaderException) 
                {
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = "Not a valid JSON.",
                        jsonModelDto = new JsonModelDto 
                        { 
                            JsonInput = jsonObject 
                        }
                    };
                }

            }
            else
            {
                return new ValidationDto 
                { IsValid = false, 
                    Message = "Not a Valid Json" , 
                    jsonModelDto=new JsonModelDto {JsonInput=jsonObject } 
                
                };
            }

        }
    }
}
