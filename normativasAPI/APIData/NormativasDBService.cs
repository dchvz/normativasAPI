using MongoDB.Driver;
using normativasAPI.APIData.Configuration;
using normativasAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace normativasAPI.APIData
{
    // esta clase nor permite conectarnos con mongoDB
    public class NormativasDBService
    {
        public MongoClient client { get; set; }
        public IMongoDatabase database { get; set; }

        public NormativasDBService(INormativasDataBaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}