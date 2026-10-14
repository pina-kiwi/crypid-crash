namespace ItsAdventureTime;

public static class Debugger
{
    private static bool isActive = false;
    
    //write out debugging info
    public static void Write(string message)
    {
        if (isActive)
        { 
            Console.WriteLine(message);  
        }
    }

    public static void Tron()
    {
        isActive = true;
        Console.WriteLine("I'm tron! Debugging on!");
    }

    public static void Troff()
    {
        isActive = false;
        Console.WriteLine("I'm troff! Debugging off!");
    }
}