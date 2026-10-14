namespace ItsAdventureTime;

public class Parser
{
    public static Command Parse(string input)
    {
        //remove extra spaces
        input = RemoveExtraSpaces(input);
        
        //convert to lowercase
        input = input.ToLower();

        //break the input into individual words
        List<string> words = input.Split(' ').ToList(); //breaks up by spaces ' ' and makes array and then creates list

        /* foreach (string word in words) //iterates over each 'word' within the 'words' list
        {
            Console.WriteLine("{" + word + "}");
        }

        if (words.Count > 2) //counts how many things are in the list
        {
            Console.WriteLine("too many words silly goose");
        }
        */

        if (input == "")
        {
            words = new List<string>(); //creates new blank list, cause even if user doesnt put anything in it it thinks that the nothing is something
        }
        
        Command command = new Command();
        if (words.Count == 2)
        {
            command.Verb = words[0];
            command.Noun = words[1];
        }
        else if (words.Count == 1)
        {
            command.Verb = words[0];
        }
        else
        {
            Console.WriteLine("i dont understand, silly goose");
        }

        return command;
    }

    public static string RemoveExtraSpaces(string input)
    {
        input = input.Trim(); //removes proceeding or trailing while space

        while (input.Contains("  "))
        {
            input = input.Replace("  ", " ");
        }
        
        return input;
    }

}