namespace cat.itb.M6UF3EA1.Models
{
    public class Friend
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return $"id: {id}{Environment.NewLine}" +
                $"name: {name}{Environment.NewLine}";
        }
    }
}
