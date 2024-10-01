using CodeNest.BLL.Repositories;
using CodeNest.DTO.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeNest.BLL.Service
{
     public class FormatterServices : IFormatterServices
    {

        #region JsonMethods
        /// <summary>
        /// This use to validate the json data
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public async Task<ValidationDto> JsonValidate(string jsonObject)
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
        #endregion

    }
}
