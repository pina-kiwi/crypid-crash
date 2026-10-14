namespace ItsAdventureTime;

public class Location
{
    private string name;
    public string Name => name;
    public string Description;
    public string InitalDescription;
    public bool Visited = false;
    
    public Dictionary<string, Location> Connections; //maps a string to a location type, definition
    public List<Item> Items = new List<Item>();

    public Location(string nameInput, string descriptionInput, string initalDescription, bool visited) //constructor, same name as class no return type
    {
        name = nameInput;
        Description = descriptionInput;
        InitalDescription = initalDescription;
        Visited = visited;
        Connections = new Dictionary<string, Location>(); //assignment
    }

    public void AddConnection(string direction, Location location)
    {
        Connections.Add(direction, location);
    }

    public bool CanMoveInDirection(Command command)
    {
        if (Connections.ContainsKey(command.Noun))
        {
            return true;
        }
        return false;
    }

    public Location GetLocationIndirection(Command command)
    {
        return Connections[command.Noun];
    }

    public string GetDescription(bool inital = false)
    {
        bool safeUsed = Conditions.CheckCondition(ConditionTypes.IsSafeUnlocked); // or IsSafeUnlocked

        string fullDescription;
        if (name == "Safe" && !safeUsed)
        {
            fullDescription = InitalDescription;
        } 
        else if (inital && !Visited)
        {
            Debugger.Write("you are at the " + name);
            fullDescription = InitalDescription;
            Visited = true;
        }
        else
        {
            fullDescription = Description;
        }
        
        foreach (Item item in Items)
        {
            fullDescription = fullDescription + "\n" + item.GetLocationDescription();
        }
        return fullDescription;
    }

    public void AddItem(Item item)
    {
        Debugger.Write("Adding item: " + item.Name +" to " + name);
        Items.Add(item);
    }

    public bool HasItem(Item itemLookingFor)
    {
        foreach (Item item in Items)
        {
            if (item.Name == itemLookingFor.Name)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void RemoveConnection(string direction)
    {
        if (Connections.ContainsKey(direction))
        {
            Connections.Remove(direction);
        }
    }
}