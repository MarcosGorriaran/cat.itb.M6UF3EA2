using cat.itb.M6UF3EA1.CRUD;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using UF3_test.connections;

namespace cat.itb.M6UF3EA2.helpers
{
    public static class EA2CRUD
    {
        public static void ACT1InsertFiles()
        {
            const string DB = "itb";
            CRUDMongoDB<Book> bookCRUD = new CRUDMongoDB<Book>(DB, "book");
            CRUDMongoDB<People> peopleCRUD = new CRUDMongoDB<People>(DB, "people");
            CRUDMongoDB<Product> productCRUD = new CRUDMongoDB<Product>(DB, "product");
            CRUDMongoDB<Restaurant> restaurantCRUD = new CRUDMongoDB<Restaurant>(DB, "restaurant");
            CRUDMongoDB<Student> studentCRUD = new CRUDMongoDB<Student>(DB, "student");
            bookCRUD.Insert(Book.ReadJSONArrayFile("../../../FitxersJSON/books.json"));
            peopleCRUD.Insert(People.ReadJSONArrayFile("../../../FitxersJSON/people.json"));
            productCRUD.Insert(Product.ReadJSONArray(File.ReadAllText("../../../FitxersJSON/products.json").Split('\n')));
            restaurantCRUD.Insert(Restaurant.ReadJSONArray(File.ReadAllText("../../../FitxersJSON/restaurants.json").Split('\n').ToList()));
            studentCRUD.Insert(Student.ReadJSONArray(File.ReadAllText("../../../FitxersJSON/students.json").Split('\n')));
        }
        public static string ACT2AGetFriends()
        {
            const string SearchTarget = "Arianna Cramer";
            
            CRUDMongoDB<People> crud = new CRUDMongoDB<People>();
            BsonDocument searchResult = crud.Select(Builders<BsonDocument>.Filter.ElemMatch("friends",Builders<BsonElement>.Filter.Eq("name",SearchTarget))).First();
            string resultMsg = "";

            resultMsg += searchResult.GetElement("friends");
            return resultMsg;
        }
        public static string ACT2BGetRestaurant()
        {
            const string FirstSearchTarget = "Manhattan";
            const string SecondSearchTarget = "Seafood";
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            List<BsonDocument> searchResult = crud.Select(Builders<BsonDocument>.Filter.Eq("borough", FirstSearchTarget) | Builders<BsonDocument>.Filter.Eq("borough", SecondSearchTarget));
            string resultMsg = "";

            foreach (BsonDocument someone in searchResult)
            {
                resultMsg += someone + Environment.NewLine;
            }
            return resultMsg;
        }
        public static string ACT2CShowRestaurantsFromZipCode(string code)
        {
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            List<BsonDocument> searchResult = crud.Select(Builders<BsonDocument>.Filter.Eq("address.zipcode", code), Builders<BsonDocument>.Projection.Include("name").Exclude("_id"));
            string resultMsg = "";

            foreach (BsonDocument element in searchResult)
            {
                resultMsg += element.GetElement("name") + Environment.NewLine;
            }
            return resultMsg;
        }
        public static string ACT2DShowBook()
        {
            CRUDMongoDB<Book> crud = new CRUDMongoDB<Book>("book");

            string resultMsg=string.Empty;
            List<BsonDocument> books = crud.Select(Builders<BsonDocument>.Sort.Ascending("pageCount"));
            foreach (BsonDocument book in books)
            {
                resultMsg += book;
                resultMsg += Environment.NewLine;
            }

            return resultMsg;
        }
        public static string ACT2EShowHighPageBooks()
        {
            CRUDMongoDB<Book> crud = new CRUDMongoDB<Book>("book");

            string resultMsg = string.Empty;
            List<BsonDocument> books = crud.Select(Builders<BsonDocument>.Filter.Gt("pageCount",250),Builders<BsonDocument>.Projection.Include("title").Include("isbn").Include("pageCount").Exclude("_id"));

            foreach(BsonDocument book in books)
            {
                resultMsg += book+Environment.NewLine;
            }

            return resultMsg;
        }
        public static int ACT3AUpdateProductStock()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<BsonDocument>.Filter.Gte("price",600) & Builders<BsonDocument>.Filter.Lte("pageCount", 1000),
                Builders<BsonDocument>.Update.Set("stock",150)));
        }
        public static int ACT3BUpdateDiscount()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<BsonDocument>.Filter.Gt("stock",100), Builders<BsonDocument>.Update.Set("discount", 100)));
        }
        public static int ACT3CUpdateCategory()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<BsonDocument>.Filter.Eq("name", "Apple TV"), Builders<BsonDocument>.Update.AddToSet("categories", "smartTV")));
        }
        public static int ACT3DUpdateZipCode()
        {
            const string SearchTarget = "Charles Street";
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");
            return Convert.ToInt32(crud.Update(Builders<BsonDocument>.Filter.Eq("address.street",SearchTarget), Builders<BsonDocument>.Update.Set("address.zipcode", "30033")));
        }
        public static string ACT3EUpdateAndShow()
        {
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");
            string resultMsg = string.Empty;

            FilterDefinition<BsonDocument> target = Builders<BsonDocument>.Filter.Eq("cuisine", "Caribbean");
            resultMsg+=crud.Update(target, Builders<BsonDocument>.Update.Set("stars","*****"));
            resultMsg += " rows updated" + Environment.NewLine;
            resultMsg += string.Join('\n',crud.Select(target));
            
            return resultMsg;
        }
        public static int ACT4ADeleteProducts()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Delete(Builders<BsonDocument>.Filter.Gte("price",400) & Builders<BsonDocument>.Filter.Lte("price", 600)));
        }
        public static int ACT4BDeleteProduct()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Delete(Builders<BsonDocument>.Filter.Eq("name", "Mac mini")));
        }
        public static int ACT4CDeleteRestaurant()
        {
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            return Convert.ToInt32(crud.Delete(Builders<BsonDocument>.Filter.Eq("cuisine", "Delicatessen")));
        }
        public static int ACT4DDeleteElementFromArray()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<BsonDocument>.Filter.Eq("name","MacBook Air"),Builders<BsonDocument>.Update.PopFirst("categories")));
        }
        public static string dropCollection(string database, string collection)
        {
            IMongoCollection<BsonDocument> conn = MongoConnection.GetDatabase(database).GetCollection<BsonDocument>(collection);
            string resultMsg = conn.CountDocuments(Builders<BsonDocument>.Filter.Empty) +" documents"+Environment.NewLine;
            conn.Database.DropCollection(conn.CollectionNamespace.CollectionName);
            resultMsg += "Remaining Collections: "+Environment.NewLine;
            resultMsg += string.Join(Environment.NewLine,conn.Database.ListCollectionNames().ToList());
            return resultMsg;
        }
    }
        
}
