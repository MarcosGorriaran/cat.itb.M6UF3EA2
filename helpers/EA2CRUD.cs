using cat.itb.M6UF3EA1.CRUD;
using cat.itb.M6UF3EA1.Models;
using MongoDB.Driver;

namespace cat.itb.M6UF3EA2.helpers
{
    public static class EA2CRUD
    {
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
    }
}
