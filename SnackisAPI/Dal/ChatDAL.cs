﻿using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public class ChatDAL : IChatDAL
    {
        private readonly IDAL _dAL;
        private readonly IConfiguration _configuration;

        public ChatDAL(IDAL dAL, IConfiguration configuration)
        {
            _dAL = dAL;
            _configuration = configuration;
        }
        public IEnumerable<Models.Chat> GetAllChats()
        {
            var client = _dAL.GetClient("CosmosMongoChat");
            var database = client.GetDatabase(_configuration["CosmosMongoChat:DbName"]);
            var collection = database.GetCollection<Models.Chat>(_configuration["CosmosMongoChat:CollectionName"]);

            return collection.Find(new BsonDocument()).ToList();
        }
        public async Task CreateChat(Chat model)
        {
            var client =_dAL.GetClient("CosmosMongoChat");
            var database = client.GetDatabase(_configuration["CosmosMongoChat:DbName"]);
            var postCollection = database.GetCollection<Chat>(_configuration["CosmosMongoChat:CollectionName"]);
            postCollection.Find(new BsonDocument());

            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                ReceiverId=model.ReceiverId,
                SenderId=model.SenderId,
                Date=DateTime.Now,
                Text=model.Text,
                GroupMembers=model.GroupMembers,
                GroupAdminId=model.GroupAdminId
               

            };

            await postCollection.InsertOneAsync(chat);
        }
    }
}
