using System.ComponentModel.Design;
using System.Reflection.Metadata;
﻿using System.Xml.Schema;

namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Spil spil = new Spil();
            spil.OpdaterSpil();
            Lagersystem.Lager.Add(spil);

            //søg pris, antal spillere, genre

            //Program-menu - IN PROGRESS - for at kunne afprøve metoder

            Console.WriteLine("||Velkommen til Genspil lagerstyring. Vælg en mulighed i menuen||");
            Console.WriteLine("\n1. Opret/opdater spil");
            Console.WriteLine("2. Slet spil");
            Console.WriteLine("3. Tilføj forespørgsel");
            Console.WriteLine("4. Tilføj kunde");
            Console.WriteLine("5. Se spil på lager");
            Console.WriteLine("6. Se forespørgsler");
            Console.WriteLine("7. Søg");

            int valg = int.Parse(Console.ReadLine());
            switch (valg)
            {
                case 1:
                    MyInterface.printHeader();
                    Console.CursorVisible = false;

                    InputOutput.Initialize();
                    MyInterface.Menu();
                    break;
                case 2:
                    break;                   
            }
        }
    }
}
