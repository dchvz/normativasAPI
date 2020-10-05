using MongoDB.Driver;
using normativasAPI.APIData.Configuration;
using normativasAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace normativasAPI.APIData
{
    public class NormativasDBService
    {
        // esta clase nor permite conectarnos con mongoDB
        private readonly IMongoCollection<Normas> _normativasCollection;

        public NormativasDBService(INormativasDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _normativasCollection = database.GetCollection<Normas>(settings.NormativasCollectionName);
        }

        public List<Normas> Get() =>
            _normativasCollection.Find(x => true).ToList();

        public Normas Get(string id) =>
            _normativasCollection.Find<Normas>(x => x.Id == id).FirstOrDefault();

        public Normas Create(Normas x)
        {
            _normativasCollection.InsertOne(x);
            return x;
        }

        public void Update(string id, Normas xIn) =>
            _normativasCollection.ReplaceOne(x => x.Id == id, xIn);

        public void Remove(Normas xIn) =>
            _normativasCollection.DeleteOne(x => x.Id == xIn.Id);

        public void Remove(string id) =>
            _normativasCollection.DeleteOne(x => x.Id == id);
    }
}