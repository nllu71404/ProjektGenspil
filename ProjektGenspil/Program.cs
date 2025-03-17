namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hej Team 5!");
            MyInterface.printHeader();
            Console.CursorVisible = false;

            Spil tempSpil = new Spil();

            tempSpil.OpretSpil();
            tempSpil.printSpilInfo();
        }
    }
}
