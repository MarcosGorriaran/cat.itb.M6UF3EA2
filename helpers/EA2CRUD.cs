using cat.itb.M6UF3EA1.CRUD;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

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
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            List<Restaurant> searchResult = crud.Select(Builders<Restaurant>.Filter.Eq(someone => someone.borough, FirstSearchTarget) | Builders<Restaurant>.Filter.Eq(someone => someone.borough, SecondSearchTarget));
            string resultMsg = "";

            foreach (Restaurant someone in searchResult)
            {
                resultMsg += someone + Environment.NewLine;
            }
            return resultMsg;
        }
        public static string ACT2CShowRestaurantsFromZipCode(string code)
        {
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            List<BsonDocument> searchResult = crud.SelectBson(Builders<Restaurant>.Filter.Eq(element => element.address.zipcode, code), Builders<Restaurant>.Projection.Include(element => element.name));
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
            List<Book> books = crud.Select(Builders<Book>.Sort.Ascending(element=>element.pageCount));
            foreach (Book book in books)
            {
                resultMsg += $"TITOL: {book.title}, Author: [{Environment.NewLine}";
                foreach(string author in book.authors)
                {
                    resultMsg+= author + Environment.NewLine;
                }
                resultMsg += "]"+Environment.NewLine;
            }

            return resultMsg;
        }
        public static string ACT2EShowHighPageBooks()
        {
            CRUDMongoDB<Book> crud = new CRUDMongoDB<Book>("book");

            string resultMsg = string.Empty;
            List<Book> books = crud.Select(Builders<Book>.Filter.Gt(element => element.pageCount,250));

            foreach(Book book in books)
            {
                resultMsg += $"Title: {book.title}, isbn: {book.isbn}, pageCount: {book.pageCount}"+Environment.NewLine;
            }

            return resultMsg;
        }
        public static int ACT3AUpdateProductStock()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<Product>.Filter.Gte(element=>element.price,600) & Builders<Product>.Filter.Lte(element=>element.price,1000),
                Builders<Product>.Update.Set(element=>element.stock,150)));
        }
        public static int ACT3BUpdateDiscount()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<Product>.Filter.Gt(element=>element.stock,100), Builders<Product>.Update.Set(element => element.discount, 100)));
        }
        public static int ACT3CUpdateCategory()
        {
            CRUDMongoDB<Product> crud = new CRUDMongoDB<Product>("product");

            return Convert.ToInt32(crud.Update(Builders<Product>.Filter.Eq(element => element.name, "Apple TV"), Builders<Product>.Update.AddToSet(element => element.categories, "smartTV")));
        }
        public static int ACT3DUpdateZipCode()
        {
            CRUDMongoDB<Restaurant> crud = new CRUDMongoDB<Restaurant>("restaurant");

            return Convert.ToInt32(crud.Update(Builders<Restaurant>.Filter.Eq(element => element.), Builders<Restaurant>.Update.Set(element => element., "smartTV")));
        }
    }
        
}
