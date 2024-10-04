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
using CodeNest.DAL.Repository;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeNest.BLL.Service
{
    public class JsonService : IJsonService
    {
        private readonly IJsonRepository _jsonRepository;
        public JsonService(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }
        public async Task<ValidationDto> Validate(JsonDto jsonDto)
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
        /// 
        /// </summary>
        /// <param name="jsonDto"></param>
        /// <param name="workspaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ValidationDto> Save(JsonDto jsonDto, ObjectId workspaceId, ObjectId userId)
        {
            bool result = await _jsonRepository.SaveAsync(jsonDto, workspaceId, userId);
            if (result) 
            {
                return new ValidationDto
                {
                    IsValid = true,
                    Message = ""
                };
            }
            return new ValidationDto
            {
                IsValid = false,
            };
        }
    }
}
