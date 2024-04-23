using System.Collections.Immutable;
using System.Text.Json;
using cat.itb.M6UF3EA1.Helpers;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using UF3_test.connections;

namespace cat.itb.M6UF3EA1.CRUD
{
    public class CRUDMongoDB<T>
    {
        protected IMongoCollection<T> Collection;


        public IMongoDatabase GetDatabase()
        {
            return Collection.Database;
        }
        public CRUDMongoDB(string database, string collection)
        {
            Collection = MongoLocalConnection.GetDatabase(database).GetCollection<T>(collection);
        }
        public CRUDMongoDB():this(ConfigurationHelper.GetDB(),ConfigurationHelper.GetDBUrl()) { }

        public void Insert(T element)
        {
            Collection.InsertOne(element);
        }
        public void Insert(params T[] elements)
        {
            Insert(elements.ToImmutableArray());
        }
        public void Insert(IEnumerable<T> elements)
        {
            List<T> list = elements.ToList();
            for (int i = 0; i<list.Count(); i++) 
            {
                Insert(list[i]);
            }
        }
        public List<T> Select()
        {   
            return Collection.Find(_ => true).ToList();
        }
        public List<T> Select(FilterDefinition<T> condition)
        {
            return Collection.Find(condition).ToList();
        }
        public List<BsonDocument> Select(FilterDefinition<T> filter, ProjectionDefinition<T> projection)
        {
            return Collection.Find(filter).Project(projection).ToList();
        }
        public void ImportJSONElements(params string[] json)
        {
            foreach(string element in json)
            {
                Collection.InsertOne(JsonSerializer.Deserialize<T>(element));
            }
        }
    }
}
