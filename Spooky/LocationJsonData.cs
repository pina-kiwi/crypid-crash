namespace ItsAdventureTime;

public class LocationJsonData
{
    public string Name { set; get; }
    public string Description { set; get; }
    public string InitialDescription { set; get; }
    public Dictionary<string, string> Connections { set; get; }
}