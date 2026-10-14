using System.Runtime.InteropServices.Marshalling;

namespace ItsAdventureTime;

public class ExplorationCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            { "eat", Eat },
            { "go", Move },
            { "tron", Tron },
            { "troff", Troff },
            { "take", Take },
            { "inventory", ShowInventory },
            { "look", Look },
            { "drop", Drop },
            { "nouns", Nouns },
            { "verbs", Verbs },
            { "fight", ChangeToFightState },
            { "explore", ChangeToExploreState },
            { "talk", ChangeToTalkState },
            { "drink", Drink },
            { "beerme", SpawnBeerInInventory },
            { "unbeerme", DespawnBeerInInventory },
            { "puke", Puke },
            { "tidyup", TidyUp },
            { "teleport", Teleport },
            {"connect", Connect},
            {"disconnect", Disconnect},
            {"use", Use},
        };

    private static void Use(Command command)
    {
        string noun = command.Noun;
        string location = Player.CurrentLocation.Name;
        
        Usable( noun, location);
        
    }

    private static void Usable(string noun, string location)
    {
        //using flower to find code
        if (noun == "flowers")
        {
            bool hasFlower = false;
            bool angelInRoom = false;

            foreach (Item item in Player.Inventory)
            {
                if (item.Name == "flowers")
                {
                    hasFlower = true;
                }
            }

            if (location == "Cemetery")
            {
                angelInRoom = true;
            }
            
            if (hasFlower && angelInRoom)
            {
                Console.WriteLine("You place the flower in the hand of the angel. A scroll with the code 05-22-25 falls, it looks like a code!");

                Player.AddItemToInventory("scroll");
                Player.RemoveItemFromInventory("flowers");
                
            }
            else
            {
                Console.WriteLine("That's not helpful here.");
            }
        }
        else if (noun == "key")
        {
            bool horseKey = false;
            bool InStudy = false;

            foreach (Item item in Player.Inventory)
            {
                if (item.Name == "key")
                {
                    horseKey = true;
                }
            }

            if (location == "Study")
            {
                InStudy = true;
            }
            
            if (horseKey && InStudy)
            {
                Console.WriteLine("You place the key in the painting. The painting swings open and you are presented with a safe to the west. ");
                Player.RemoveItemFromInventory("key");
                Map.AddConnection("Study", "west", "Safe");
            }
            else
            {
                Console.WriteLine("That's not helpful here.");
            }
        }
        //if has code open the safe
        else if (noun == "scroll")
        {
            bool hasScroll = false;
            bool InSafe = false;

            foreach (Item item in Player.Inventory)
            {
                if (item.Name == "scroll")
                {
                    hasScroll = true;
                }
            }

            if (location == "Safe")
            {
                InSafe = true;
            }
            
            if (hasScroll && InSafe)
            {
                Conditions.ChangeCondition(ConditionTypes.IsScrollUsed, true);
                Conditions.ChangeCondition(ConditionTypes.IsSafeUnlocked, true);
                Console.WriteLine("The safe springs open!");
                Map.AddItem("generator", "Safe");
                Map.AddItem("$20", "Safe");
                
                Player.Look();
            }
            else
            {
                Console.WriteLine("That's not helpful here.");
            }
        }
        //if they have all winning conditions
        else if (noun == "generator")
        {
            bool hasGenerator = false;

            foreach (Item item in Player.Inventory)
            {
                if (item.Name == "generator")
                {
                    hasGenerator = true;
                }
            }
            
            if (hasGenerator)
            {
                Console.WriteLine(
                    "You were able to call for help and were rescued the next morning. You were finally able to go visit your family, recount your experience, and talk about the creature. No one believed you, but there's no way you're going back to find out if it was real. Or at least not by yourself.");
                Debugger.Write("changing condition to false");
                Conditions.ChangeCondition(ConditionTypes.IsPlaying, false);
                Debugger.Write($"IsPlaying? {Conditions.IsTrue(ConditionTypes.IsPlaying)}");
            }
        }
        else
        {
            Console.WriteLine("You don't have that.");
        }  
    }

    private static void Disconnect(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsDisconnected,true);
    }

    private static void Connect(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsCreatedConnection, true);
    }

    private static void Teleport(Command command)
    {
        Conditions.ChangeCondition(ConditionTypes.IsTeleported, true);
    }

    private static void TidyUp(Command command)
    {
            Conditions.ChangeCondition(ConditionTypes.IsTidiedUp, true);
    }

    private static void Puke(Command command)
    {
            Conditions.ChangeCondition(ConditionTypes.IsHungOver, true);
    }

    private static void DespawnBeerInInventory(Command command)
    {
            Conditions.ChangeCondition(ConditionTypes.IsBeerMed, false);
    }

    private static void SpawnBeerInInventory(Command command)
    {
            Conditions.ChangeCondition(ConditionTypes.IsBeerMed, true);
    }

    private static void Drink(Command command)
    {
            Player.Drink(command);
    }

    private static void ChangeToTalkState(Command command)
    {
        States.ChangeState(StateTypes.Talking);
    }
    
    private static void ChangeToFightState(Command command)
    {
        States.ChangeState(StateTypes.Fighting);
    }
    
    private static void ChangeToExploreState(Command command)
    {
        States.ChangeState(StateTypes.Exploring);
    }

    private static void Verbs(Command command)
    {
        List<string> verbs = ExplorationCommandValidator.GetVerbs();
        foreach (string verb in verbs)
        {
            Console.WriteLine(verb);
        }
    }

    private static void Nouns(Command command)
    {
        List<string> nouns = ExplorationCommandValidator.GetNouns();
        foreach (string noun in nouns)
        {
            Console.WriteLine(noun);
        }
    }

    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> method = commandMap[command.Verb];
            method.Invoke(command);
        }
        else
        {
            Console.WriteLine("I don't know how to do that.");
        }
    }
    
    private static void Drop(Command command)
    {
        Player.Drop(command);
    }
    
    private static void Look(Command command)
    {
        Player.Look();
    }

    private static void ShowInventory(Command command)
    {
        Player.ShowInventory();
    }
    
    private static void Take(Command command)
    {
        Player.Take(command);
    }

    private static void Troff(Command command)
    {
        Debugger.Troff();
    }

    private static void Tron(Command command)
    {
        Debugger.Tron();
    }

    public static void Eat(Command command)
    {
        Console.WriteLine("Eating..." + command.Noun);
    }

    public static void Move(Command command)
    {
        Player.Move(command);
    }
}