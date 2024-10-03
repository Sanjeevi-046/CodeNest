using CodeNest.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CodeNest.DAL.Context
{
    public class MangoDbService
    {
        private readonly IMongoDatabase _database;
        public MangoDbService(IConfiguration _configuration)
        {
            _configuration = _configuration;
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mangoClient = new (mongoUrl);
            _database = mangoClient.GetDatabase("CodeValidator");
        }

        public IMongoCollection<Users> UserModel => _database.GetCollection<Users>("Users");
        public IMongoCollection<BaseToString> BasetoString => _database.GetCollection<BaseToString>("base_to_string");
        public IMongoCollection<StringToBase> StringtoBase => _database.GetCollection<StringToBase>("string_to_base");
        public IMongoCollection<CustomHtml> Html => _database.GetCollection<CustomHtml>("html");
        public IMongoCollection<CustomJavascript> JavaScript => _database.GetCollection<CustomJavascript>("javascript");
        public IMongoCollection<Json> Json => _database.GetCollection<Json>("json");
        public IMongoCollection<Jwt> Jwt => _database.GetCollection<Jwt>("jwt");
        public IMongoCollection<Xml> Xml => _database.GetCollection<Xml>("xml");
        public IMongoCollection<Workspaces> WorkSpaces => _database.GetCollection<Workspaces>("workspaces");
    }
}
