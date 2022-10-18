using DatabaseServer;

namespace DatabaseServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DatabaseConnect db = new DatabaseConnect();
            db.Connect();
        }
    }
}