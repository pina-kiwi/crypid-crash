namespace ItsAdventureTime;

public static class Player
{
    public static Location CurrentLocation;
    public static List<Item> Inventory = new List<Item>();

    public static void Initialize()
    {
        CurrentLocation = Map.StartLocation;
    }

    public static void Move(Command command)
    {
        Console.WriteLine("Moving " + command.Noun);
        
        //if we can move in the direction
        if (CurrentLocation.CanMoveInDirection(command))
        {
            //ask the map for the location in the direction
            CurrentLocation = CurrentLocation.GetLocationIndirection(command);
            Console.WriteLine(CurrentLocation.GetDescription(true));
        }
        else
        {
            Console.WriteLine("Cannot move " + command.Noun);
        }
    }

    public static string GetLocationDescription(bool inital)
    {
        if (CurrentLocation.Name == "safe")
        {
            inital = false;
        }
        
        return CurrentLocation.GetDescription(inital);
    }

    public static void Take(Command command)
    {
        //figure out which item to take: turn the noun into an item
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            Console.WriteLine("No such item, i dont know what " + command.Noun + " is.");
        }
        else if (!CurrentLocation.HasItem(item)) //if the item is not in current location
        {
            Console.WriteLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            Console.WriteLine("The " + command.Noun + " cannot be taked.");
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.Items.Remove(item);
            //CurrentLocation.RemoveItem(item)
            item.Pickup();
            Console.WriteLine("You pick up the " + command.Noun + "!");
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("You are empty-handed.\n;-;");
        }
        else
        {
            Console.WriteLine("You are carrying: ");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Console.WriteLine(" " + article + " "+ item.Name + ": " + item.Description);
            }
        }
    }

    public static void Look()
    {
        Console.WriteLine( CurrentLocation.GetDescription(false));
    }

    public static void Drop(Command command)
    {
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            string article = SemanticTools.CreateArticle(command.Noun);
            Console.WriteLine("i dont know what " + article + " "+ command.Noun + " is");
        }
        else if (!Inventory.Contains(item))
        {
            Console.WriteLine("you are not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            Console.WriteLine("You dropped the " + command.Noun + " in " + "!");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            Console.WriteLine("** drinking beer **");
            Conditions.ChangeCondition(ConditionTypes.IsDrunk, true);
            RemoveItemFromInventory("beer");
            AddItemToInventory("beer-bottle");
        }
    }

    public static void AddItemToInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);

        if (item == null)
        {
            return;
        }
        
        Inventory.Add(item);
    }

    public static void RemoveItemFromInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);
        if (item == null)
        {
            return;
        }
        Inventory.Remove(item);
    }

    public static void MoveToLocation(string locationName)
    {
        //look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        //if theres no location with that name
        if (newLocation == null)
        {
            Console.WriteLine("trying to move to the void\nget lost");
            return;
        }
        
        CurrentLocation = newLocation; 
        Look();
    }
}