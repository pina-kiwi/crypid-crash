namespace ItsAdventureTime;

class Program //c# starts with program class and the main function, always capilize class
{
    static void Main(string[] args) //method/function, public is assumed
    //public [return type] name() {}
    // [return type] float, int, string, char, bool, void, list
    {
        
        Console.WriteLine("Hello, World! Goodbye, stinky o/");
        Game.PlayGame();
    }

    /*
    public int GetWeight()
    {
        return 2;
    }
    */
}