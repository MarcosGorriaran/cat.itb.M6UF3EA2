using cat.itb.M6UF3EA2.helpers;

namespace cat.itb.M6UF3EA1;

public static class Driver
{
    public static void Main()
    {
        const string MenuTitle = "Select one of the activities";
        const string ACTTwoAMenuValue = "Get Friend";
        const string ActTwoAIndexValue = "2A. ";
        const string AskValue = "Provide me with the option index: ";

        Menu mainMenu = new Menu(MenuTitle,new Dictionary<string, string>()
        {
            {ActTwoAIndexValue,ACTTwoAMenuValue }
        },AskValue);
    }
}