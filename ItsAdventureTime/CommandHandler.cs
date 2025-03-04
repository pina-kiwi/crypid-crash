namespace ItsAdventureTime;

public static class CommandHandler
{
    public static void Handle(Command command)
    {
        if (command.Verb == "eat")
        {
            Eat(command);
        }
        else if (command.Verb == "go")
        {
            Move(command);
        }
    }

    public static void Eat(Command command)
    {
        Console.WriteLine("Eating " + command.Noun);
    }

    public static void  Move(Command command)
    {
        Console.WriteLine("Moving " + command.Noun);
    }
}