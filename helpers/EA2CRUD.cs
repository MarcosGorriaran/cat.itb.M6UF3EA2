using cat.itb.M6UF3EA1.CRUD;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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

            People searchResult = crud.Select(Builders<People>.Filter.Eq(someone=>someone.name,SearchTarget)).First();
            string resultMsg = "";

            foreach(Friend someone in searchResult.friends)
            {
                resultMsg += someone + Environment.NewLine;
            }
            return resultMsg;
        }
        public static string ACT2BGetRestaurant()
        {
            const string FirstSearchTarget = "Manhattan";
            const string SecondSearchTarget = "Seafood";
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>();

            List<Restaurant> searchResult = crud.Select(Builders<Restaurant>.Filter.Eq(someone => someone.borough, FirstSearchTarget) & Builders<Restaurant>.Filter.Eq(someone => someone.borough, SecondSearchTarget));
            string resultMsg = "";

            foreach (Restaurant someone in searchResult)
            {
                resultMsg += someone + Environment.NewLine;
            }
            return resultMsg;
        }
        public static string ACT2CShowRestaurantsFromZipCode(string code)
        {
            const string FirstSearchTarget = "Manhattan";
            const string SecondSearchTarget = "Seafood";
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            List<BsonDocument> searchResult = crud.SelectBson(Builders<Restaurant>.Filter.Eq(element => element.address.zipcode, code), Builders<Restaurant>.Projection.Include(element => element.name));
            string resultMsg = "";

            foreach (BsonDocument element in searchResult)
            {
                resultMsg += "Name: "+element.GetElement("name") + Environment.NewLine;
            }
            return resultMsg;
        }
    }
        
}
