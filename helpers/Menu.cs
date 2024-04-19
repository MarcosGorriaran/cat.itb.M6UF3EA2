namespace cat.itb.M6UF3EA2.helpers
{
    public class Menu
    {
        private string title;
        private string askValueMsg;
        private List<string> elements;
        public Menu(string title, IEnumerable<String> elements, string askValueMsg) 
        {
            this.title = Title;
            elements = elements.ToList();

        }
        public Menu (IEnumerable<string> elements, string askValueMsg):this(string.Empty,elements,askValueMsg) { }
        public Menu (IEnumerable<string> elements):this(elements, string.Empty) {}
        public Menu ():this(new List<string>()) { }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string AskValueMsg
        {
            get { return askValueMsg; }
            set { askValueMsg = value; }
        }
        public List<string> Elements
        {
            get { return Elements; }
            set { Elements = value; }
        }
        public string ToConsoleMenu(string spliterIndexElement)
        {
            string result = string.Empty;
            if(Title == string.Empty)
            {
                result += Title+Environment.NewLine;
            }
            for(int i = 0; i<Elements.Count(); i++)
            {
                result += $"{i + 1}{spliterIndexElement}{Elements[i]}{Environment.NewLine}";
            }

            return result+AskValueMsg;
        }
        
    }
}
