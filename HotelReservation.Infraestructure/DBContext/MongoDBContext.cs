using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infraestructure.DBContext
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
        static MongoDBContext()
        {
            // Register the GuidSerializer with the correct representation
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        }
        public MongoDBContext(IMongoClient client, MongoDBSettings mongoDBSettings)
        {
            _database = client.GetDatabase(mongoDBSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
