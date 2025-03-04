namespace ItsAdventureTime;

public class Location
{
    private string name;
    public Dictionary<string, Location> Connections;

    public Location(string nameInput) //constructor, same name as class no return type
    {
        name = nameInput;
    }
}