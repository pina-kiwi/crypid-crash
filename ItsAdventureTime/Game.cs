namespace ItsAdventureTime;

public static class Game //each class does one thing
{
    public static void PlayGame()
    {
        //[type] [name]
        // if bool, start with is/has etc
        bool isPlaying; //declaring the variable
        isPlaying = true; //assigning
        Console.WriteLine("Welcome to the ItsAdventureTime Game!"); //writeline will print the next line
        
        Map.Initialize();
        
        while (isPlaying == true)
        {
            string input;
            
            Command command = CommandProcessor.Process();
            //do stuff with what the player typed in
            
            /*
            Console.Write("> "); //does not end line
            input = Console.ReadLine();
            Console.WriteLine(input);
            */
            
            if (command.Verb == "exit")
            {
                Console.WriteLine("Game Over!");
                isPlaying = false;
            }
            else
            {
                CommandHandler.Handle(command);
            }
            
        }
    }
}