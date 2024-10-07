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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeNest.DAL.Repository
{
    public class FormatterServices : IFormatterServices
    {
        private readonly ILogger<FormatterServices> _logger;

        public FormatterServices(ILogger<FormatterServices> logger)
        {
            _logger = logger;
        }

        #region JsonMethods
        /// <summary>
        /// This use to validate the json data
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public Task<ValidationDto> JsonValidate(string jsonObject)
        {
            if (string.IsNullOrWhiteSpace(jsonObject))
            {
                _logger.LogWarning("JsonValidate: Received empty or whitespace JSON string.");
                return Task.FromResult(new ValidationDto { IsValid = false, Message = "Not Valid Json" });
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

                    _logger.LogInformation("JsonValidate: Successfully validated JSON.");
                    return Task.FromResult(new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        JsonDto = new JsonDto
                        {
                            JsonInput = jsonObject,
                            JsonOutput = beautifiedJson
                        }
                    });
                }
                catch (JsonReaderException ex)
                {
                    _logger.LogError(ex, "JsonValidate: JSON validation failed.");
                    return Task.FromResult(new ValidationDto
                    {
                        IsValid = false,
                        Message = "Invalid JSON format.",
                        JsonDto = new JsonDto
                        {
                            JsonInput = jsonObject
                        }
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "JsonValidate: An unexpected error occurred.");
                    return Task.FromResult(new ValidationDto
                    {
                        IsValid = false,
                        Message = "An unexpected error occurred during JSON validation.",
                        JsonDto = new JsonDto
                        {
                            JsonInput = jsonObject
                        }
                    });
                }
            }
            else
            {
                _logger.LogWarning("JsonValidate: JSON string does not start and end with valid characters.");
                return Task.FromResult(new ValidationDto
                {
                    IsValid = false,
                    Message = "Not a Valid Json",
                    JsonDto = new JsonDto
                    {
                        JsonInput = jsonObject
                    }
                });
            }
        }
        #endregion

        //#region XmlMethods
        ///// <summary>
        ///// This use to validate the xml data
        ///// </summary>
        ///// <param name="xmlObject"></param>
        ///// <returns></returns>
        //public Task<ValidationDto> XmlValidate(string xmlObject)
        //{
        //    if (string.IsNullOrWhiteSpace(xmlObject))
        //    {
        //        _logger.LogWarning("XmlValidate: Received empty or whitespace XML string.");
        //        return Task.FromResult(new ValidationDto { IsValid = false, Message = "Not Valid XML" });
        //    }

        //    try
        //    {
        //        XDocument.Parse(xmlObject);
        //        _logger.LogInformation("XmlValidate: Successfully validated XML.");
        //        return Task.FromResult(new ValidationDto
        //        {
        //            IsValid = true,
        //            Message = "Valid XML",
        //            JsonDto = new JsonDto
        //            {
        //                JsonInput = xmlObject,
        //                JsonOutput = XDocument.Parse(xmlObject).ToString()
        //            }
        //        });
        //    }
        //    catch (XmlException ex)
        //    {
        //        _logger.LogError(ex, "XmlValidate: XML validation failed.");
        //        return Task.FromResult(new ValidationDto
        //        {
        //            IsValid = false,
        //            Message = "Invalid XML format.",
        //            JsonDto = new JsonDto
        //            {
        //                JsonInput = xmlObject
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "XmlValidate: An unexpected error occurred.");
        //        return Task.FromResult(new ValidationDto
        //        {
        //            IsValid = false,
        //            Message = "An unexpected error occurred during XML validation.",
        //            JsonDto = new JsonDto
        //            {
        //                JsonInput = xmlObject
        //            }
        //        });
        //    }
        //}
        //#endregion
    }
}
