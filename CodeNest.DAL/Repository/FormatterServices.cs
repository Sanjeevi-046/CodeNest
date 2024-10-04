// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: Sample Description.
//
// ***********************************************************************************************

using CodeNest.DTO.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeNest.DAL.Repository
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
            char firstChar = jsonObject[0];
            char lastChar = jsonObject[^1];

            if ((firstChar == '{' && lastChar == '}') ||
                (firstChar == '[' && lastChar == ']'))
            {

                try
                {
                    JToken parsedJson = JToken.Parse(jsonObject);

                    string beautifiedJson = parsedJson.ToString(Formatting.Indented);

                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        JsonDto = new JsonDto
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
                        JsonDto = new JsonDto
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
                    JsonDto = new JsonDto
                    {
                        JsonInput = jsonObject
                    }
                };
            }
        }
        #endregion
    }
}
