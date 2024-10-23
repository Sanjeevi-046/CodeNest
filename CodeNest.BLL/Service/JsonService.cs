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

        public async Task<List<BlobDto>> GetJson(ObjectId workspaceId)
        {
            return await _jsonRepository
                .GetJsonList(workspaceId);
        }

        public async Task<ValidationDto> Validate(BlobDto jsonDto)
        {
            if (string.IsNullOrWhiteSpace(jsonDto.Input))
            {
                return new ValidationDto
                {
                    IsValid = false,
                    Message = "Not Valid Json"
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
        /// 
        /// </summary>
        /// <param name="jsonDto"></param>
        /// <param name="workspaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ValidationDto> Save(BlobDto jsonDto, ObjectId workspaceId, ObjectId userId, string filename)
        {
            bool result = await _jsonRepository.SaveAsync(jsonDto, workspaceId, userId, filename); 
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
