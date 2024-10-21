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
using ZstdSharp.Unsafe;

namespace CodeNest.BLL.Service
{
    public class FormatterServices : IFormatterServices
    {
        private readonly ILogger<FormatterServices> _logger;
        private readonly IJsonRepository _jsonRepository;
        private readonly IFormatterRepository _formatterRepository;

        public FormatterServices(ILogger<FormatterServices> logger, IJsonRepository jsonRepository, IFormatterRepository formatterRepository)
        {
            _logger = logger;
            _jsonRepository = jsonRepository;
            _formatterRepository = formatterRepository;
        }

        /// <summary>
        /// Validates the provided JSON data.
        /// </summary>
        /// <param name="jsonDto">The JSON data to validate.</param>
        /// <returns>A ValidationDto indicating whether the JSON is valid and any relevant messages.</returns>
        public async Task<ValidationDto> JsonValidate(BlobDto jsonDto)
        {
            _logger.LogInformation("JsonValidate: Starting JSON validation.");

            if (string.IsNullOrWhiteSpace(jsonDto.Input))
            {
                _logger.LogWarning("JsonValidate: Input is null or whitespace.");
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

                    _logger.LogInformation("JsonValidate: JSON is valid.");
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
                    _logger.LogError(ex, "JsonValidate: JSON parsing failed.");
                    return new ValidationDto
                    {
                        IsValid = false,
                        Message = "Invalid JSON format.",
                        Blobs = new BlobDto
                        {
                            Input = jsonDto.Input
                        }
                    };
                }
            }
            else
            {
                _logger.LogWarning("JsonValidate: Input is not a valid JSON.");
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Not a Valid JSON",
                    Blobs = new BlobDto
                    {
                        Input = jsonDto.Input
                    }
                };
            }
        }

        /// <summary>
        /// Saves the provided JSON data.
        /// </summary>
        /// <param name="jsonDto">The JSON data to save.</param>
        /// <param name="workSpace">The workspace identifier.</param>
        /// <param name="user">The user identifier.</param>
        /// <returns>A ValidationDto indicating whether the save operation was successful and any relevant messages.</returns>
        public async Task<bool> Save(BlobDto jsonDto, ObjectId workSpace, ObjectId user)
        {
            _logger.LogInformation("Save: Starting save operation.");

            try
            {
                if (jsonDto == null)
                {
                    _logger.LogWarning("Save: Received null JsonDto.");
                    return true;
                }

                bool saveResult = await _jsonRepository.SaveAsync(jsonDto, workSpace, user);

                if (saveResult)
                {
                    _logger.LogInformation("Save: Successfully saved JSON data.");
                    return true;
                }
                else
                {
                    _logger.LogError("Save: Failed to save JSON data.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Save: An unexpected error occurred.");
                return false;
            }
        }
        /// <summary>
        /// gets blob data
        /// </summary>
        /// <param name="blobId"></param>
        /// <returns></returns>
        public async Task<BlobDto> GetBlob(ObjectId blobId)
        {
            BlobDto blobDto = await _formatterRepository.GetBlob(blobId);
            return blobDto;
        }
    }
}
