using CodeNest.DTO.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeNest.BLL.Service
{
    public class JsonService : IJsonService
    {
        public async Task<ValidationDto> Validate(string jsonObject)
        {
            if (string.IsNullOrWhiteSpace(jsonObject))
            {
                return new ValidationDto { IsValid = false, Message = "Not Valid Json" };
            }
            jsonObject = jsonObject.Trim();
            if (jsonObject.StartsWith("{") && jsonObject.EndsWith("}") ||
                jsonObject.StartsWith("[") && jsonObject.EndsWith("]"))
            {

                try
                {
                    var parsedJson = JToken.Parse(jsonObject);

                    string beautifiedJson = parsedJson.ToString(Formatting.Indented);

                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        jsonDto = new JsonDto
                        {
                            JsonInput = jsonObject,
                            JsonOutput = beautifiedJson
                        }
                    };
                }
                catch (JsonReaderException ex)
                {
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = ex.ToString(),
                        jsonDto = new JsonDto
                        {
                            JsonInput = jsonObject
                        }
                    };
                }

            }
            else
            {
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Not a Valid Json",
                    jsonDto = new JsonDto
                    {
                        JsonInput = jsonObject
                    }

                };
            }

        }
    }
}
