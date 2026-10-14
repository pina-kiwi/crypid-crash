namespace ItsAdventureTime;

public class Item
{
    public string Name { get; private set; } //private names should be lowercase, public is uppercase, there is a getter and setter for this name
    public string Description { get; private set; }
    public bool IsTakeable { get; private set; }

    //constructor
    public Item(string name, string initialDescription, string description, bool isTakeable = true)
    {
        Name = name;
        Description = description;
        InitialDescription = initialDescription;
        IsTakeable = isTakeable;
        ExplorationCommandValidator.AddNoun(name);
    }

    public string InitialDescription {get; private set;}
    
    public bool HasBeenTaken { get;private set; }

    public string ToString()
    {
        return Name + " - " + Description + " - " + IsTakeable;
    }

    public void Pickup()
    {
        HasBeenTaken = true;
    }
    
    public string GetLocationDescription()
    {
        if (HasBeenTaken)
        {
            string article = SemanticTools.CreateArticle(Name);
            return "There is " + article + " " + Name + " here.";
        }
        return InitialDescription;
    }

    public string GetName(Item item)
    {
        return item.Name;
    }
}
