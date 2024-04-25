using System.Collections.Immutable;
using System.Text.Json;
using cat.itb.M6UF3EA1.Helpers;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using UF3_test.connections;

namespace cat.itb.M6UF3EA1.CRUD
{
    public class CRUDMongoDB<T> where T : Model<T>
    {
        protected IMongoCollection<T> Collection;


        public IMongoDatabase GetDatabase()
        {
            return Collection.Database;
        }
        public CRUDMongoDB(string database, string collection)
        {
            Collection = MongoConnection.GetDatabase(database).GetCollection<T>(collection);
        }
        public CRUDMongoDB(string collection) : this(ConfigurationHelper.GetDB(), collection) { }
        public CRUDMongoDB():this(ConfigurationHelper.GetCollection()) { }

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
        public long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            try
            {
                return Collection.UpdateMany(filter, update).MatchedCount;
            }
            catch (Exception)
            {
                return 0;
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
        public List<BsonDocument> SelectBson(FilterDefinition<T> filter, ProjectionDefinition<T> projection)
        {
            List<BsonDocument> bson = Collection.Find(filter).Project(projection).ToList();
            return bson;
        }
        public List<T> Select(SortDefinition<T> sort)
        {
            return Collection.Find(_ => true).Sort(sort).ToList();
        }
        public void ImportJSONElements(params string[] json)
        {
            
            foreach(string element in json)
            {
                MongoConnection.GetDatabase(ConfigurationHelper.GetDB()).GetCollection<BsonDocument>(Collection.CollectionNamespace.CollectionName).InsertOne(BsonDocument.Parse(element));
            }
        }
    }
}
