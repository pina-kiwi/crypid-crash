namespace ItsAdventureTime;

public class ConversationCommandValidator
{
    public static bool IsValid(Command command)
    {
        if (command.Verb == "y" || command.Verb == "n" ||
            command.Verb == "leave")
        {
            return true;
        }
        Console.WriteLine("Valid commands are: y, n, leave");
        return false;
    }
}