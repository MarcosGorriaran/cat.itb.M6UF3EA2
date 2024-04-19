
namespace cat.itb.M6UF3EA1.Models
{
    public class Restaurant : Model<Restaurant>
    {
        public Address address { get; set; }
        public string borough {  get; set; }
        public string cuisine { get; set; }
        public RestGrade[] grades { get; set; }
        public string name { get; set; }
        public string restaurant_id { get; set; }
    }
}
