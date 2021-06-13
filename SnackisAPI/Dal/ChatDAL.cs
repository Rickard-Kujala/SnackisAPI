using Microsoft.Extensions.Configuration;
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
            var client = _dAL.GetClient("CosmosMongoSnackisChat");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackisChat:DbName"]);
            var collection = database.GetCollection<Models.Chat>(_configuration["CosmosMongoSnackisChat:CollectionName"]);

            return collection.Find(new BsonDocument()).ToList();
        }
        public async Task CreateChat(Chat model)
        {
            var client =_dAL.GetClient("CosmosMongoSnackisChat");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackisChat:DbName"]);
            var postCollection = database.GetCollection<Chat>(_configuration["CosmosMongoSnackisChat:CollectionName"]);
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
        public async Task DeletechatById(Guid id)
        {
            var client = _dAL.GetClient("CosmosMongoSnackisChat");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackisChat:DbName"]);
            var postCollection = database.GetCollection<Post>(_configuration["CosmosMongoSnackisChat:CollectionName"]);
            postCollection.Find(new BsonDocument());

            var filter = Builders<Post>.Filter.Eq(x => x.Id, id);

            await postCollection.DeleteOneAsync(filter);
        }
        public async Task UpdateChat(Guid id, Chat updatedChat)
        {
            var client = _dAL.GetClient("CosmosMongoSnackisChat");
            var database = client.GetDatabase(_configuration["CosmosMongoSnackisChat:DbName"]);
            var ChatCollection = database.GetCollection<Chat>(_configuration["CosmosMongoSnackisChat:CollectionName"]);
            ChatCollection.Find(new BsonDocument());

            var filter = Builders<Chat>.Filter.Eq(x => x.Id, id);
            var update = Builders<Chat>.Update
                .Set(c => c.Id, updatedChat.Id)
                .Set(c => c.ReceiverId, updatedChat.ReceiverId)
                .Set(c => c.SenderId, updatedChat.SenderId)
                .Set(c => c.Text, updatedChat.Text)
                .Set(c => c.IsRead, updatedChat.IsRead)
                .Set(c => c.GroupMembers, updatedChat.GroupMembers)
                .Set(c => c.GroupAdminId, updatedChat.GroupAdminId);

            await ChatCollection.UpdateOneAsync(filter, update);
        }
    }
}
