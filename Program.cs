using cat.itb.M6UF3EA2.helpers;

namespace cat.itb.M6UF3EA1;

public static class Driver
{
    public static void Main()
    {
        const string MenuTitle = "Select one of the activities";
        const string ACTTwoAMenuValue = "Get Friend";
        const string ActTwoAIndexValue = "2A";
        const string ActOneInsertFiles = "Insert files";
        const string ActOneIndex = "1";
        const string ActTwoShowBRestaurants = "Show Restaurants trough borough";
        const string ActTwoBIndex = "2B";
        const string ActThreeCRestaurants = "Show restaurants trough zipcode";
        const string ActThreeCIndex = "2C";
        const string SplitText = ". ";
        const string ExitText = "Exit";
        const string ExitOption = "0";
        const string AskValue = "Provide me with the option index: ";
        const string AskZipCode = "Provide me with the zipcode search target: ";
        

        Menu mainMenu = new Menu(MenuTitle,new Dictionary<string, string>()
        {
            {ActOneIndex, ActOneInsertFiles},
            {ActTwoAIndexValue,ACTTwoAMenuValue },
            {ActTwoBIndex, ActTwoShowBRestaurants},
            {ActThreeCIndex, ActThreeCRestaurants },
            {"2D","Get ordered books" },
            {"2E","Get big books"},
            {"3A","Change product stock" },
            {"3B","Add discount property"},
            {"3C","Add caegory smartTV"},
            {"3D","Change zipcode" },
            {"3E","Update and show"},
            {"4A","Delete products between values" },
            {"4B","Delete product Mac mini"},
            {"4C","Delete Delicatessen restaurants"},
            {"4D","Delete first category from MacBook air"},
            {"5A","Drop Collection"},
            {ExitOption.ToString(),ExitText}
        },AskValue);
        string option;

        do
        {
            Console.Write(mainMenu.ToString(SplitText));
            option = Console.ReadLine();

            switch(option.Trim().ToUpper())
            {
                case ActOneIndex:
                    EA2CRUD.ACT1InsertFiles();
                    break;
                case ActTwoAIndexValue:
                    Console.WriteLine(EA2CRUD.ACT2AGetFriends());
                    break;
                case ActTwoBIndex:
                    Console.WriteLine(EA2CRUD.ACT2BGetRestaurant());
                    break;
                case ActThreeCIndex:
                    Console.Write(AskZipCode);
                    string zipcode = Console.ReadLine();
                    try
                    {
                        Console.WriteLine(EA2CRUD.ACT2CShowRestaurantsFromZipCode(zipcode));
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }
                    break;
                case "2D":
                    Console.WriteLine(EA2CRUD.ACT2DShowBook());
                    break;
                case "2E":
                    Console.WriteLine(EA2CRUD.ACT2EShowHighPageBooks());
                    break;
                case "3A":
                    Console.WriteLine(EA2CRUD.ACT3AUpdateProductStock());
                    break;
                case "3B":
                    Console.WriteLine(EA2CRUD.ACT3BUpdateDiscount());
                    break;
                case "3C":
                    Console.WriteLine(EA2CRUD.ACT3CUpdateCategory());
                    break;
                case "3D":
                    Console.WriteLine(EA2CRUD.ACT3DUpdateZipCode());
                    break;
                case "3E":
                    Console.WriteLine(EA2CRUD.ACT3EUpdateAndShow());
                    break;
                case "4A":
                    Console.WriteLine(EA2CRUD.ACT4ADeleteProducts());
                    break;
                case "4B":
                    Console.WriteLine(EA2CRUD.ACT4BDeleteProduct());
                    break;
                case "4C":
                    Console.WriteLine(EA2CRUD.ACT4CDeleteRestaurant());
                    break;
                case "4D":
                    Console.WriteLine(EA2CRUD.ACT4DDeleteElementFromArray());
                    break;
                case "5A":
                    Console.Write("Target database: ");
                    string database = Console.ReadLine();
                    Console.Write("Target collection: ");
                    string colection = Console.ReadLine();

                    Console.WriteLine(EA2CRUD.dropCollection(database,colection));
                    break;
            }
        }while(option != ExitOption);
        
    }
}