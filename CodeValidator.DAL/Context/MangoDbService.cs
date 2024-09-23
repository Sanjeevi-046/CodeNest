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

        public IMongoCollection<User> userModel => _database.GetCollection<User>("UserModel");
    }
}
