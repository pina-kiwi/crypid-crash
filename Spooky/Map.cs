using System.Text.Json;

namespace ItsAdventureTime;

public static class Map
{
    private static Dictionary<string, Location> nameToLocation = new Dictionary<string, Location>();
    public static Location StartLocation;
    public static void Initialize()
    {
        Debugger.Write("Current Directory: " + Environment.CurrentDirectory);
        string path = Path.Combine(Environment.CurrentDirectory, "Map.json");
        string rawText = File.ReadAllText(path);
        
        MapJsonData data = JsonSerializer.Deserialize<MapJsonData>(rawText);
        
        //make all the locations
        Dictionary<string, Location> locations = new Dictionary<string, Location>();
        foreach (LocationJsonData location in data.Locations)
        {
            Location newLocation = AddLocation(location.Name, location.Description, location.InitialDescription);
            locations.Add(location.Name, newLocation);
        }
        //set up all the connections
        foreach (LocationJsonData location in data.Locations)
        {
            Location currentLocation = locations[location.Name];
            foreach (KeyValuePair<string, string> connection in location.Connections)
            {
                string direction = connection.Key.ToLower();
                string destination = connection.Value;
                
                if (nameToLocation.ContainsKey(destination))
                {
                    Location destinationLocation = nameToLocation[destination];
                    currentLocation.AddConnection(direction, destinationLocation);
                }
                else
                {
                    Console.WriteLine("Unknown destination: " + destination);
                }
            }
        }

        if (locations.TryGetValue(data.StartLocation, out Location startLocation))
        {
            StartLocation = startLocation;
        }
        else
        {
            Console.WriteLine("You didn't put a start location in your map.json file, bitch!");
        }
    }
    
    private static Location AddLocation(string locationName, string locationDescription, string locationInitalDescription, bool locationVisited = false)
    {
        Location newLocation = new Location(locationName, locationDescription, locationInitalDescription, locationVisited);
        nameToLocation.Add(locationName, newLocation);
        return newLocation;
    }

    public static void AddItem(string itemName, string locationName)
    {
        //find out which location is named LocationName
        Location location = GetLocationByName(locationName);
        Item item = Items.GetItemByName(itemName);
        
        //add the item to the location
        if (item != null && location != null)
        {
            location.AddItem(item);
        }
    }

    public static void RemoveItem(string itemName, string locationName)
    {
        //find out which location is named locationName
        Location location = GetLocationByName(locationName);
        Item item = Items.GetItemByName(itemName);

        if (item != null && location != null)
        {
            location.RemoveItem(item);
        }
    }

    public static Location GetLocationByName(string locationName)
    {
        if (nameToLocation.ContainsKey(locationName))
        {
            return nameToLocation[locationName];
        }
        else
        {
            return null;
        }
    }

    public static void AddConnection(string startLocaitionName, string direction, string endLocationName)
    {
        //get the location obejcts baded on the names
        Location start = GetLocationByName(startLocaitionName);
        Location end = GetLocationByName(endLocationName);
        
        // if the locations dont exist
        if (start == null || end == null)
        {
            Console.WriteLine("Tried to create a connection between unknown locations: " + startLocaitionName + ", " + endLocationName);
            return;
        }
        start.AddConnection(direction, end);
    }

    public static void RemoveConnection(string startLocationName, string direction)
    {
        Location start = GetLocationByName(startLocationName);

        if (start == null)
        {
            Console.WriteLine("Tried to remove a connection from an unkown location: " + startLocationName);
            return;
        }
        
        start.RemoveConnection(direction);
    }
}