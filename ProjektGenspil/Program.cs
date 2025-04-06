using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Reflection.Metadata;
﻿using System.Xml.Schema;
using System.Collections.Generic;
using System.Linq;

namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)

        {
            InputOutput.Initialize();
            MyInterface.LagerMenu();
        
            //Instans af Spil-klassen
            Spil spil = new Spil();

            //Test kunde-objekter tilføjet til forespørgsel-listen for at afprøve PrintForespørgsler metoden
            Kunder LarsHansen = new Kunder("Lars Hansen", "20387745", "LarsH@gmail.com", "Ludo");
            Kunder SimoneJensen = new Kunder("Simone Jensen", "44760211", "SiJensen@gmail.com", "Hitster");
            Kunder AstridJohansen = new Kunder("Astrid Johansen", "22897102", "Astridjohansen@msn.com", "Matador");
            Kunder JonasMathiasen = new Kunder("Jonas Mathiasen", "01038436", "Jonasersej@Hotmail.com", "Matador");

            spil.TilføjeKunderForespørgsel(LarsHansen);
            spil.TilføjeKunderForespørgsel(SimoneJensen);
            spil.TilføjeKunderForespørgsel(AstridJohansen);
            spil.TilføjeKunderForespørgsel(JonasMathiasen);
            
            //Test-Spil-objekter tilføjet til lagerlisten, for at afprøve søgefunktionen - Der er fejl i udskrivning af Genre!!
           
            Spil spil1 = new Spil("Ticket to Ride", 299.99, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[0], StamData.alleGenrer[0..1]);
            Spil spil2 = new Spil("Exploding Kittens", 150.00, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[1], [StamData.alleGenrer[0], StamData.alleGenrer[3]]);

            Lagersystem.TilføjSpil(spil1);
            Lagersystem.TilføjSpil(spil2);

            
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
           // int valg = int.Parse(Console.ReadLine());
           // switch (valg)
           // {

           //     case 1:
           //         Spil.OpretSpil();
           //         break;
                
           //     case 2:
           //         Lagersystem.FjernSpil(Lagersystem.Lager);
           //         break;

           //     case 5:
           //         Console.WriteLine("Spil på lager:");
           //        Lagersystem.PrintLagerstatus(); 
           //         break;

           //         case 6:
           //         Lagersystem.PrintForespørgsler(spil);
           //         break;

           //     case 7:

           //         Lagersystem.BrugerInputSøgSpil();

           //         Console.ReadLine();

           //             break;
           //         }
           // Console.ReadLine();
           //}
           ////Instans af Spil-klassen
           // Spil spil = new Spil();

           // //Test kunde-objekter tilføjet til forespørgsel-listen for at afprøve PrintForespørgsler metoden
           // Kunde LarsHansen = new Kunde("Lars Hansen", "20387745", "LarsH@gmail.com", "Ludo");
           // Kunde SimoneJensen = new Kunde("Simone Jensen", "44760211", "SiJensen@gmail.com", "Hitster");
           // Kunde AstridJohansen = new Kunde("Astrid Johansen", "22897102", "Astridjohansen@msn.com", "Matador");
           // Kunde JonasMathiasen = new Kunde("Jonas Mathiasen", "01038436", "Jonasersej@Hotmail.com", "Matador");

           // spil.TilføjeKunderForespørgsel(LarsHansen);
           // spil.TilføjeKunderForespørgsel(SimoneJensen);
           // spil.TilføjeKunderForespørgsel(AstridJohansen);
           // spil.TilføjeKunderForespørgsel(JonasMathiasen);



           // //Test-Spil-objekter tilføjet til lagerlisten, for at afprøve søgefunktionen - Der er fejl i udskrivning af Genre!!
           

           // Spil spil1 = new Spil("Ticket to Ride", 299.99, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[0], StamData.alleGenrer[0..1]);
           // Spil spil2 = new Spil("Exploding Kittens", 150.00, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[1], [StamData.alleGenrer[0], StamData.alleGenrer[3]]);

           // Lagersystem.TilføjSpil(spil1);
           // Lagersystem.TilføjSpil(spil2);

    }        
}

