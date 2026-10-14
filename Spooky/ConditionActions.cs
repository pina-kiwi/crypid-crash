namespace ItsAdventureTime;

public class ConditionActions
{
    public static Action WriteOutput(string message)
    {
        return () => { Console.WriteLine(message); };
    }
    
    // add to inventory
    public static Action AddItemToInventory(string itemName)
    {
        return () => { Player.AddItemToInventory(itemName); };
    }
    
    // remove from inventory
    public static Action RemoveItemFromInventory(string itemName)
    {
        return () => Player.RemoveItemFromInventory(itemName); 
    }
    
    // add object to location
    public static Action AddItemToLocation(string itemName, string locationName)
    {
        return () => Map.AddItem(itemName, locationName);
    }
    
    // remove object from location
    public static Action RemoveItemFromLocation(string itemName, string locationName)
    {
        return () => Map.RemoveItem(itemName, locationName);
    }
    //move player to a location
    public static Action MovePlayerToLocation(string locationName)
    {
        return () => Player.MoveToLocation(locationName);
    }
    
    //create a map connection
    public static Action AddMapConnection(string startLocaitionName, string direction, string endLocationName)
    {
        return  () => Map.AddConnection( startLocaitionName, direction, endLocationName);
    }

    public static Action RemoveMapConnection(string startLocationName, string direction)
    {
        return () => Map.RemoveConnection(startLocationName, direction);
    }
    
    // record things in a journal
    
    // spawning an npc/mob
    
    // despawn npc/mob
    
    // change our game state
    
    // teleport us or item
    
    // win game
    
    // game losing
}