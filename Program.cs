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
            {ExitOption.ToString(),ExitText}
        },AskValue);
        string option;

        do
        {
            Console.Write(mainMenu.ToString(SplitText));
            option = Console.ReadLine();

            switch(option)
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
                    Console.WriteLine(AskZipCode);
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
                    
            }
        }while(option != ExitOption);
        
    }
}