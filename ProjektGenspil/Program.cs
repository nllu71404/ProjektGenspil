using System.ComponentModel.Design;
using System.Reflection.Metadata;
﻿using System.Xml.Schema;

namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            InputOutput.Initialize();
            MyInterface.LagerMenu();
                        

            



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

              
        }
    }
}
