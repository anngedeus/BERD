using System;
namespace CompleteBackend
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World");

            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.RetreiveMathProblems();

            // Add additional code here as needed
            Console.ReadKey();
        }
    }
}