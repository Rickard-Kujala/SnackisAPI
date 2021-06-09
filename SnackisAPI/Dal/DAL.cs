using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public class DAL : IDAL
    {
        private readonly IConfiguration _configuration;

        public DAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MongoClient GetClient(string mongoSettingsName)
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

    }
}
