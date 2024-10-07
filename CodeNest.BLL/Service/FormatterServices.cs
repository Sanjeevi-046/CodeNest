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

using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeNest.BLL.Service
{
    public class FormatterServices : IFormatterServices
    {
        private readonly ILogger<FormatterServices> _logger;
        private readonly IJsonRepository _jsonRepository;

        public FormatterServices(ILogger<FormatterServices> logger, IJsonRepository jsonRepository)
        {
            _logger = logger;
            _jsonRepository = jsonRepository;
        }

        #region JsonMethods
        public async Task<ValidationDto> JsonValidate(JsonDto jsonDto)
        {
            if (string.IsNullOrWhiteSpace(jsonDto.JsonInput))
            {
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Not Valid Json"
                };
            }
            jsonDto.JsonInput = jsonDto.JsonInput.Trim();
            char firstChar = jsonDto.JsonInput[0];
            char lastChar = jsonDto.JsonInput[^1];

            if ((firstChar == '{' && lastChar == '}') ||
                (firstChar == '[' && lastChar == ']'))
            {

                try
                {
                    JToken parsedJson = JToken.Parse(jsonDto.JsonInput);

                    string beautifiedJson = parsedJson.ToString(Formatting.Indented);

                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        JsonDto = new JsonDto
                        {
                            JsonInput = jsonDto.JsonInput,
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
                            JsonInput = jsonDto.JsonInput
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
                        JsonInput = jsonDto.JsonInput
                    }
                };
            }
        }

        /// <summary>
        /// Save the json data
        /// </summary>
        /// <param name="usersDto"></param>
        /// <param name="workSpace"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ValidationDto> Save(JsonDto jsonDTo, ObjectId workSpace, ObjectId user)
        {
            if (jsonDTo == null)
            {
                _logger.LogWarning("Save: Received null UsersDto.");
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Data cannot be null."
                };
            }

            bool saveResult = await _jsonRepository.SaveAsync(jsonDTo, workSpace, user);

            if (saveResult)
            {
                _logger.LogInformation("Save: Successfully saved JSON data.");
                return new ValidationDto
                {
                    IsValid = true,
                    Message = "Data saved successfully."
                };
            }
            else
            {
                _logger.LogError("Save: Failed to save JSON data.");
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Failed to save data."
                };
            }
        }
        #endregion
    }
}
