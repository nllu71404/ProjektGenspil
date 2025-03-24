namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej Team 5!");
            MyInterface.printHeader();
            Console.CursorVisible = false; 

            
            Spil.OpretSpil();
            Spil.printSpilInfo();
        }
    }
}
