using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Reflection.Metadata;
﻿using System.Xml.Schema;
using System.Collections.Generic;

namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test-Spil-objekter tilføjet til lagerlisten, for at afprøve søgefunktionen - Der er fejl i udskrivning af Genre!!
            string genre = StamData.alleGenrer[2];
            string[] genreArray4 = new string[] { genre };


            Spil spil1 = new Spil("Ticket to Ride", 299.99, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[0], genreArray4);
            Spil spil2 = new Spil("Exploding Kittens", 150.00, StamData.alleAlderGrupper[1], StamData.alleAntalSpillere[1], genreArray4);
            Lagersystem.Lager.Add(spil1);
            Lagersystem.Lager.Add(spil2);


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

                //case 5:
                    //Console.WriteLine("Spil på lager:");
                   // Lagersystem.PrintLagerstatus(); //Fejl!! Kan ikke kalde metoden - hvorfor?
                    //break;

                case 7:

                    //SØGEFUNKTION

                    //Titel
                    Console.WriteLine("||Søg efter spil||");

                    //Bruger-Input
                    Console.WriteLine("\nIndtast navn (tryk Enter for at springe over):");
                    string navn = Console.ReadLine();

                    Console.WriteLine("Indtast minimum pris (tryk Enter for at springe over):");
                    string minPrisInput = Console.ReadLine();
                    double minPris = string.IsNullOrEmpty(minPrisInput) ? 0 : double.Parse(minPrisInput);


                    Console.WriteLine("Indtast maksimum pris (tryk Enter for at springe over):");
                    string maxPrisInput = Console.ReadLine();
                    double maxPris = string.IsNullOrEmpty(maxPrisInput) ? double.MaxValue : double.Parse(maxPrisInput);


                    Console.WriteLine("Vælg en genre ved at indtaste nummeret (tryk Enter for at springe over):");
                    for (int i = 0; i < StamData.alleGenrer.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {StamData.alleGenrer[i]}");
                    }
                    string genreValgInput = Console.ReadLine();
                    string valgtGenre = string.IsNullOrEmpty(genreValgInput) ? null : StamData.alleGenrer[int.Parse(genreValgInput) - 1];


                    Console.WriteLine("Vælg en aldersgruppe ved at indtaste nummeret (tryk Enter for at springe over):");
                    for (int i = 0; i < StamData.alleAlderGrupper.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {StamData.alleAlderGrupper[i]}");
                    }
                    string alderValgInput = Console.ReadLine();
                    string valgtAlder = string.IsNullOrEmpty(alderValgInput) ? null : StamData.alleAlderGrupper[int.Parse(alderValgInput) - 1];


                    Console.WriteLine("Vælg antal spillere ved at indtaste nummeret (tryk Enter for at springe over):");
                    for (int i = 0; i < StamData.alleAntalSpillere.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {StamData.alleAntalSpillere[i]}");
                    }
                    string antalSpillereValgInput = Console.ReadLine();
                    string valgtAntalSpillere = string.IsNullOrEmpty(antalSpillereValgInput) ? null : StamData.alleAntalSpillere[int.Parse(antalSpillereValgInput) - 1];

                    //Kalder SøgSpil metoden for at vise søgeresultater
                    var resultater = Lagersystem.SøgSpil(Lagersystem.Lager, navn, valgtGenre, minPris, maxPris, valgtAlder, valgtAntalSpillere);

                    //Udprint af søgeresultater
                    Console.WriteLine("Søgeresultater:");
                    if (resultater.Count == 0)
                    {
                        Console.WriteLine("Ingen spil fundet, der matcher dine søgekriterier.");
                    }
                    else
                    {
                        foreach (Spil spil in resultater)
                        {
                            Console.WriteLine($"Navn: {spil.Navn}, Genre: {spil.Genre}, Pris: {spil.NyPris}, Alder: {spil.AlderGruppe}, Antal Spillere: {spil.AntalSpillere}");
                        }
                    }

                        break;
                    }
            Console.ReadLine();
           }
           
    }
        
    }

