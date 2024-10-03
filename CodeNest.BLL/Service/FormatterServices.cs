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
        public async Task<ValidationDto> JsonValidate(string jsonInput)
        {
            // Check for null or whitespace input
            if (string.IsNullOrWhiteSpace(jsonInput))
            {
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Input is either null, empty, or contains only whitespace."
                };
            }

            // Trim the input to remove any leading or trailing spaces
            jsonInput = jsonInput.Trim();

            // Validate if input starts and ends with the appropriate JSON characters
            if ((jsonInput.StartsWith('{') && jsonInput.EndsWith('}')) ||
                (jsonInput.StartsWith('[') && jsonInput.EndsWith(']')))
            {
                try
                {
                    // Try parsing the JSON input
                    JToken parsedJson = JToken.Parse(jsonInput);
                    string formattedJson = parsedJson.ToString(Formatting.Indented);

                    // Return valid JSON response
                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON.",
                       jsonDto = new JsonDto
                        {
                            JsonInput = jsonInput,
                            JsonOutput = formattedJson
                        }
                    };
                }
                catch (JsonReaderException ex)
                {
                    // Handle invalid JSON input
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = $"Invalid JSON: {ex.Message}",
                        jsonDto = new JsonDto
                        {
                            JsonInput = jsonInput
                        }
                    };
                }
            }
            else
            {
                // Return when the structure does not match JSON object or array
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Input is not in valid JSON format (missing appropriate starting or ending brackets).",
                    jsonDto = new JsonDto
                    {
                        JsonInput = jsonInput
                    }
                };
            }
        }
        #endregion

    }
}
