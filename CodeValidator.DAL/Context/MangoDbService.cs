using CodeValidator.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CodeValidator.DAL.Context
{
    public class MangoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        public MangoDbService(IConfiguration _configuration)
        {
            _configuration = _configuration;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mangoClient = new MongoClient(mongoUrl);
            _database = mangoClient.GetDatabase("CodeValidator");
        }

        public IMongoCollection<Users> userModel => _database.GetCollection<Users>("Users");
        public IMongoCollection<BaseToString> basetoString => _database.GetCollection<BaseToString>("base_to_string");
        public IMongoCollection<StringToBase> stringtoBase => _database.GetCollection<StringToBase>("string_to_base");
        public IMongoCollection<Html> html => _database.GetCollection<Html>("html");
        public IMongoCollection<Javascript> javaScript => _database.GetCollection<Javascript>("javascript");
        public IMongoCollection<Json> json => _database.GetCollection<Json>("json");

        public IMongoCollection<Jwt> jwt => _database.GetCollection<Jwt>("jwt");

        public IMongoCollection<Xml> xml => _database.GetCollection<Xml>("xml");
        public IMongoCollection<Workspaces> workSpaces => _database.GetCollection<Workspaces>("workspaces");

    }
}
