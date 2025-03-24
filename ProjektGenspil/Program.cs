namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            MyInterface.printHeader();
            Console.CursorVisible = false;

            InputOutput.Initialize();
            MyInterface.Menu();
        }
    }
}
