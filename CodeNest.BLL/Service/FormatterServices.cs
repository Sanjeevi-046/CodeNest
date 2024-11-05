// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Esprima;
using Jsbeautifier;
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
        public async Task<BlobDto> JsonValidate(BlobDto jsonDto)
        {
            if (string.IsNullOrWhiteSpace(jsonDto.Input))
            {
                _logger.LogWarning("JsonValidate: Input is null or whitespace.");
                return new BlobDto();
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
                    return new BlobDto
                    {
                        Input = jsonDto.Input,
                        Output = beautifiedJson
                    };
                }
                catch (JsonReaderException ex)
                {
                    _logger.LogError(ex, "JsonValidate: JSON parsing failed.");
                    return new BlobDto();
                }
            }
            else
            {
                _logger.LogWarning("JsonValidate: Input is not a valid JSON.");
                return new BlobDto();
            }
        }

        /// <summary>
        /// Saves the provided JSON data.
        /// </summary>
        /// <param name="jsonDto">The JSON data to save.</param>
        /// <param name="workSpace">The workspace identifier.</param>
        /// <param name="user">The user identifier.</param>
        /// <returns>A ValidationDto indicating whether the save operation was successful and any relevant messages.</returns>
        public async Task<bool> Save(BlobDto jsonDto, ObjectId workSpace, ObjectId user, string filename)
        {
            _logger.LogInformation("Save: Starting save operation.");

            try
            {
                if (jsonDto == null)
                {
                    _logger.LogWarning("Save: Received null JsonDto.");
                    return true;
                }

                bool saveResult = await _jsonRepository.SaveAsync(jsonDto, workSpace, user, filename);

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

        public async Task<BlobDto> Update(BlobDto blobDto, ObjectId blobID, ObjectId userId)
        {
            BlobDto result = await _formatterRepository.Update(blobDto, blobID, userId);
            return result;
        }
        /// <summary>
        /// Validates the javascript code
        /// </summary>
        /// <param name="blobDto"></param>
        /// <returns>if code is valide returns input with ouput and success message,else returns input only with error message</returns>
        public async Task<BlobDto> JavascriptValidate(BlobDto blobDto)
        {
            if (string.IsNullOrWhiteSpace(blobDto.Input))
            {
                _logger.LogWarning("JavascriptValidate: Input is null or whitespace.");
                return new BlobDto();
            }

            blobDto.Input = blobDto.Input.Trim();

            try
            {
                JavaScriptParser js = new();
                js.ParseScript(blobDto.Input);
                Beautifier beautifier = new();
                string beautifiedCode = beautifier.Beautify(blobDto.Input);
                _logger.LogInformation("JavascriptValidate: JavaScript is valid.");
                return new BlobDto
                    {
                        Input = blobDto.Input,
                        Output = beautifiedCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JavascriptValidate: JavaScript parsing failed.");
                return new BlobDto();
            }
        }
    }
}
