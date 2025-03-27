using System;
using ProjektGenspil;
using System.Collections.Generic;
using System.Linq;

namespace ProjektGenspil;

internal class Lagersystem
{
	public static List<Spil> Lager;

	public Lagersystem()
	{
		Lager = new List<Spil>();
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
			Console.WriteLine($"{spil.navn} er nu fjernet fra lagerlisten"); //Der skal laves en properties/egenskaber af Spil-klassens attributter, så de andre klasser kan tilgå dem 

		} 
	else
		{
			Console.WriteLine($"{spil.navn} findes ikke i lagerlisten");
		}
	}


	public static List<Spil> SøgSpil(List<Spil> Lager, string navn, string[] genre, string pris, string alder, string antalSpillere)//Metode: Søgefunktion til at finde spil på lagerlisten ved hjælp af forskellige søgekriterier (navn, genre, aldersgruppe, sprog, stand, antal spillere)
    {

        var resultater = Lager.Where(spil =>
            (string.IsNullOrEmpty(navn) || spil.navn.Contains(navn)) &&
            (genre == null || genre.Length == 0 || genre.Contains(spil.Genre)) &&
            (string.IsNullOrEmpty(pris) || spil.pris == pris) &&
            (string.IsNullOrEmpty(alder) || spil.alder == alder) &&
            (string.IsNullOrEmpty(antalSpillere) || spil.AntalSpillere == antalSpillere)
        ).ToList();

        return resultater;

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
		//Mangler info fra spil-klassen

		foreach (Kunder kunde in spil.forespørgsler) 
		{
			Console.WriteLine($"{kunde}");
		}
	}
}
