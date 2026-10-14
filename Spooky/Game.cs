namespace ItsAdventureTime;

public static class Game //each class does one thing
{
    public static void PlayGame()
    {
        Initialize();

        Console.WriteLine(Player.GetLocationDescription(true));

        //[type] [name]
        // if bool, start with is/has etc
        bool isPlaying; //declaring the variable
        isPlaying = true; //assigning
        Conditions.ChangeCondition(ConditionTypes.IsPlaying, true);
        //Console.WriteLine("Welcome to the ItsAdventureTime Game!"); //write line will print the next line
        while (Conditions.IsTrue(ConditionTypes.IsPlaying))
        {
            Command command = CommandProcessor.Process();

            if (command.IsValid)
            {
                if (command.Verb == "exit")
                {
                    Console.WriteLine("\nall done :)\nbaiii baiii");
                    Conditions.ChangeCondition(ConditionTypes.IsPlaying, false);
                }
                else
                {
                    CommandHandler.Handle(command);

                    // Check immediately after handling command
                    if (!Conditions.IsTrue(ConditionTypes.IsPlaying))
                    {
                        break; // Stop the loop now
                    }
                }
            }
        }
        Console.WriteLine("\nGame Over! Thanks for playing.");

        /*
        while (isPlaying)
        {
            Command command = CommandProcessor.Process();

            if (command.IsValid)
            {
                if (command.Verb == "exit")
                {
                    Console.WriteLine("\nall done :)\nbaiii baiii");
                    isPlaying = false;
                }
                else
                {
                    CommandHandler.Handle(command);

                    // Check if game over condition was triggered inside command handling
                    if (Conditions.IsTrue(ConditionTypes.IsPlaying))
                    {
                        Console.WriteLine("\nGame Over! Thanks for playing.");
                        isPlaying = false;
                    }
                }
            }
        }*/
    }

    private static void Initialize()
    {
        Conditions.Initialize();
        Map.Initialize();
        Items.Initialize();
        Player.Initialize();
        States.Initialize();
    }
}