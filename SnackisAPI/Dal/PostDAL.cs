using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public class PostDAL : IPostDAL
    {
        private readonly IConfiguration _configuration;

        public PostDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private MongoClient GetClient(string mongoSettingsName)
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(_configuration[$"{mongoSettingsName}:Host"], 10255);
            settings.UseTls = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            settings.RetryWrites = false;

            MongoIdentity identity = new MongoInternalIdentity(_configuration[$"{mongoSettingsName}:DbName"], _configuration[$"{mongoSettingsName}:UserName"]);
            MongoIdentityEvidence evidence = new PasswordEvidence(_configuration[$"{mongoSettingsName}:Password"]);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);

            return client;
        }
        public IEnumerable<Models.Post> GetCollectionFromDB()
        {
            var client = GetClient("CosmosMongoSnackis");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackis:DbName"]);
            var collection = database.GetCollection<Models.Post>(_configuration["CosmosMongoSnackis:CollectionName"]);

            return collection.Find(new BsonDocument()).ToList();
        }
        public async Task CreatePostToDB(Post model)
        {
            var client = GetClient("MongoDBSettings");
            var database = client.GetDatabase(_configuration["MongoDBSettings:DbName"]);
            var postCollection = database.GetCollection<Post>(_configuration["MongoDBSettings:CollectionName"]);
            postCollection.Find(new BsonDocument());

            var post = new Post
            {
                Id = Guid.NewGuid(),    
                UserId=model.UserId,
                Category=model.Category,
                Title=model.Title,
                Text=model.Text,
                DateTime=DateTime.Now,
                AbuseReport=model.AbuseReport,
                PostParent=model.PostParent
            };
            
            await postCollection.InsertOneAsync(post);
        }
    }
}
