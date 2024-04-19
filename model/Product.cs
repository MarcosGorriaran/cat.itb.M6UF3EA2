

namespace cat.itb.M6UF3EA1.Models
{
    public class Product : Model<Product>
    {
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string picture { get; set; }
        public string[] categories { get; set; }
    }
}
