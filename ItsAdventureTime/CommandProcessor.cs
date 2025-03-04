using System.ComponentModel.Design;

namespace ItsAdventureTime;

public static class CommandProcessor
{
    
    public static Command Process()
    {
        //get input
        string rawInput;
        rawInput = GetInput();
        //Console.WriteLine(rawInput);
        
        //make sure it is two words, parse
        Command command = Parser.Parse(rawInput);
        Console.WriteLine("Verb: {" + command.Verb + "}, Noun: {" + command.Noun + "}");

        //make sure we have the words in our vocabulary, validate
        
        bool isValid = CommandValidator.IsValid(command);
        
        //do stuff with command
        
        Console.WriteLine("isValid = " + isValid);
        
        return command;
    }
    
    public static string GetInput()
    {
        Console.Write("> ");
        string input;
        
        input = Console.ReadLine();
        
        return input;
    }
}