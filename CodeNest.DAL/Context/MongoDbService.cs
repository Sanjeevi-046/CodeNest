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

using CodeNest.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CodeNest.DAL.Context
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public MongoDbService(IConfiguration _configuration)
        {
            _configuration = _configuration;
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mangoClient = new(mongoUrl);
            _database = mangoClient.GetDatabase("CodeValidator");
        }

        public IMongoCollection<Users> UserModel => _database.GetCollection<Users>("Users");
        public IMongoCollection<BaseToString> BasetoString => _database.GetCollection<BaseToString>("base_to_string");
        public IMongoCollection<CustomHtml> Html => _database.GetCollection<CustomHtml>("html");
        public IMongoCollection<CustomJavascript> JavaScript => _database.GetCollection<CustomJavascript>("javascript");
        public IMongoCollection<CustomJson> Json => _database.GetCollection<CustomJson>("json");
        public IMongoCollection<Jwt> Jwt => _database.GetCollection<Jwt>("jwt");
        public IMongoCollection<CustomXml> Xml => _database.GetCollection<CustomXml>("xml");
        public IMongoCollection<Workspaces> WorkSpaces => _database.GetCollection<Workspaces>("workspaces");
    }
}
