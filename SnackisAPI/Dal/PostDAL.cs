﻿using Microsoft.Extensions.Configuration;
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
        public IEnumerable<Models.Post> GetAllPosts()
        {
            var client = GetClient("CosmosMongoSnackis");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackis:DbName"]);
            var collection = database.GetCollection<Models.Post>(_configuration["CosmosMongoSnackis:CollectionName"]);

            return collection.Find(new BsonDocument()).ToList();
        }
        public async Task CreatePost(Post model)
        {
            var client = GetClient("CosmosMongoSnackis");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackis:DbName"]);
            var postCollection = database.GetCollection<Post>(_configuration["CosmosMongoSnackis:CollectionName"]);
            postCollection.Find(new BsonDocument());

            //var postCollection = GetCollectionFromDB();

            var post = new Post
            {
                Id = Guid.NewGuid(),    
                UserId=model.UserId,
                Category=model.Category,
                Title=model.Title,
                Text=model.Text,
                DateTime=DateTime.Now,
                AbuseReport=false,
                PostParent=model.PostParent,
                Nickname=model.Nickname
                
            };
            
            await postCollection.InsertOneAsync(post);
        }
        public async Task DeletePostToDB(string id)
        {
            var client = GetClient("CosmosMongoSnackis");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackis:DbName"]);
            var postCollection = database.GetCollection<Post>(_configuration["CosmosMongoSnackis:CollectionName"]);
            postCollection.Find(new BsonDocument());

            var filter = Builders<Post>.Filter.Eq(x=>x.Id.ToString(), id);

            await postCollection.DeleteOneAsync(filter);
        }
        public IEnumerable<string> GetAllcetegories()
        {
            var client = GetClient("CosmosMongoSnackis");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackis:DbName"]);
            var collection = database.GetCollection<Models.Post>(_configuration["CosmosMongoSnackis:CollectionName"]);

            //var results = collection.Find(FilterDefinition<Post>.Empty).ToList().Select(c=>c.Category);
            return collection.Find(FilterDefinition<Post>.Empty).ToList().Select(c => c.Category);

            //return collection.Find(new BsonDocument()).ToList();
        }
    }
}
