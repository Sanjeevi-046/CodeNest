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
        public async Task<ValidationDto> JsonValidate(BlobDto jsonDto)
        {
            if (string.IsNullOrWhiteSpace(jsonDto.Input))
            {
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Please Enter Input"
                };
            }

            jsonDto.Input = jsonDto.Input.Trim();
            char firstChar = jsonDto.Input[0];
            char lastChar = jsonDto.Input[^1];

            if ((firstChar == '{' && lastChar == '}') ||
                (firstChar == '[' && lastChar == ']'))
            {

                try
                {
                    JToken parsedJson = JToken.Parse(jsonDto.Input);

                    string beautifiedJson = parsedJson.ToString(Formatting.Indented);

                    return new ValidationDto
                    {
                        IsValid = true,
                        Message = "Valid JSON",
                        Blobs = new BlobDto
                        {
                            Input = jsonDto.Input,
                            Output = beautifiedJson
                        }
                    };
                }
                catch (JsonReaderException ex)
                {
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = ex.ToString(),
                        Blobs = new BlobDto
                        {
                            Input = jsonDto.Input
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
                    Blobs = new BlobDto
                    {
                        Input = jsonDto.Input
                    }
                };
            }
        }

        /// <summary>
        /// Save the json data
        /// </summary>
        /// <param name="jsonDto"></param>
        /// <param name="workSpace"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ValidationDto> Save(BlobDto jsonDto, ObjectId workSpace, ObjectId user)
        {
            try
            {
                if (jsonDto == null)
                {
                    _logger.LogWarning("Save: Received null JsonDto.");
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = "Data cannot be null."
                    };
                }

                bool saveResult = await _jsonRepository.SaveAsync(jsonDto, workSpace, user);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Save: An unexpected error occurred.");
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "An unexpected error occurred."
                };
            }
        }
    }
}
