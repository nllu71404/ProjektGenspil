using System;
using ProjektGenspil;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Linq;

namespace ProjektGenspil;

internal class Lagersystem
{
	public static List<Spil> Lager = new List<Spil>();


	public Lagersystem()
	{

	}



	public void TilføjSpil(Spil spil) //Metode: tilføjer spil til lagerlisten
	{
		Lager.Add(spil);
		//evt udskrift af bekræftelse 
		Console.WriteLine($"\n{spil} er nu tilføjet til lagerlisten");
	}

	public void FjernSpil(Spil spil) //Metode: fjerner spil fra lagerlisten
	{
	if(Lager.Contains(spil))
			{
			Lager.Remove(spil);


			//Evt print af bekræftelse
			Console.WriteLine($"{spil.Navn} er nu fjernet fra lagerlisten"); 


		} 
	else
		{
			Console.WriteLine($"{spil.Navn} findes ikke i lagerlisten");
		}
	}

    public static List<Spil> SøgSpil(List<Spil> Lager, string navn, string valgtGenre, double MinPris, double MaxPris, string valgtAlder, string valgtAntalSpillere)
    {
        var resultater = Lager.Where(spil =>
            (string.IsNullOrEmpty(navn) || spil.Navn.Contains(navn)) &&
            (string.IsNullOrEmpty(valgtGenre) || spil.Genre.Contains(valgtGenre)) &&
            (spil.NyPris >= MinPris && spil.NyPris <= MaxPris) &&
            (string.IsNullOrEmpty(valgtAlder) || spil.AlderGruppe == valgtAlder) &&
            (string.IsNullOrEmpty(valgtAntalSpillere) || spil.AntalSpillere == valgtAntalSpillere)
        ).ToList();

        return resultater;

    }


    public static void BrugerInputSøgSpil() //Har bare kopieret koden direkte ind i Menuen i program-klassen i case 7
    {
		//Titel
		Console.WriteLine("||Søg efter spil||");

		//Bruger-Input
	 Console.WriteLine("\nIndtast navn:");
            string navn = Console.ReadLine();

    Console.WriteLine("Indtast minimum pris:");
            double minPris = double.Parse(Console.ReadLine());

    Console.WriteLine("Indtast maksimum pris:");
            double maxPris = double.Parse(Console.ReadLine());

    Console.WriteLine("Vælg en genre ved at indtaste nummeret :");
            for (int i = 0; i<StamData.alleGenrer.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {StamData.alleGenrer[i]}");
            }
            int genreValg = int.Parse(Console.ReadLine());
    string valgtGenre = StamData.alleGenrer[genreValg - 1];

            Console.WriteLine("Vælg en aldersgruppe ved at indtaste nummeret:");
            for (int i = 0; i<StamData.alleAlderGrupper.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {StamData.alleAlderGrupper[i]}");
            }
            int alderValg = int.Parse(Console.ReadLine());
string valgtAlder = StamData.alleAlderGrupper[alderValg - 1];

Console.WriteLine("Vælg antal spillere ved at indtaste nummeret :");
for (int i = 0; i < StamData.alleAntalSpillere.Length; i++)
{
    Console.WriteLine($"{i + 1}. {StamData.alleAntalSpillere[i]}");
}
int antalSpillereValg = int.Parse(Console.ReadLine());
string valgtAntalSpillere = StamData.alleAntalSpillere[antalSpillereValg - 1];

var resultater = Lagersystem.SøgSpil(Lagersystem.Lager, navn, valgtGenre, minPris, maxPris, valgtAlder, valgtAntalSpillere);

		//Udprint af søgeresultater
Console.WriteLine("Søgeresultater:");
foreach (Spil spil in resultater)
{
    Console.WriteLine($"Navn: {spil.Navn}, Genre: {spil.Genre}, Pris: {spil.NyPris}, Alder: {spil.AlderGruppe}, Antal Spillere: {spil.AntalSpillere}");
}
        } 
	


    public void PrintLagerstatus() //Metode: udskriver en sorteret lagerlisten/ spil-listen
	{
		foreach (Spil spil in Lager)
		{
			Lager.Sort();
			Console.WriteLine($"{spil}");
		}
	}

	public void PrintForespørgsler(Spil spil) //Metode: Udskriver liste med forespørgsler  - Mangler info fra Spil-klassen
	{

		foreach (Kunder kunde in spil.Forespørgsler) 
		{
			Console.WriteLine($"{kunde}");
		}
	}
}
