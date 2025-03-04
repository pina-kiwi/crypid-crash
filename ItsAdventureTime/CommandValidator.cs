namespace ItsAdventureTime;

public static class CommandValidator
{
    
    public static List<string> Verbs = new List<string>
        { "go", "eat" };

    public static List<string> StandAloneVerbs = new List<string>
        { "inventory", "exit", "look" };
    
    public static List<string> Nouns = new List<string>
        { "bagel", "apple", "beer"};

    public static bool IsValid(Command command)
    {
        bool isValid = false;
        
        if (IsVerb(command.Verb) || IsStandAloneVerb(command.Verb))
        {
            if (IsStandAloneVerb(command.Verb))
            {
                isValid = true;
            }
            else if (IsNoun(command.Noun))
            {
                isValid = true;
            }
        }
        
        return isValid;
    }

    private static bool IsNoun(string commandNoun)
    {
        if (Nouns.Contains(commandNoun))
        {
            return true;
        }
        return false;
    }

    private static bool IsStandAloneVerb(string commandVerb)
    {
        if (StandAloneVerbs.Contains(commandVerb))
        {
            return true;
        }
        return false;
    }

    private static bool IsVerb(string commandVerb)
    {
        if (Verbs.Contains(commandVerb))
        {
            return true;
        }
        return false;
    }
}