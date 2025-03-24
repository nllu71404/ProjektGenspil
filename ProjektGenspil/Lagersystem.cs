using System;
using ProjektGenspil;
using System.Collections.Generic;
using System.Linq;

namespace ProjektGenspil;

public class Lagersystem
{
	public List<Spil> Lager;

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
			Console.WriteLine($"\n{spil.navn} er nu fjernet fra lagerlisten");  
		} 
	else
		{
			Console.WriteLine($"{spil.navn} findes ikke i lagerlisten");
		}
	}


	public List<Spil> SøgSpil(string navn = null, string[] genre = null, string pris = null, string alder = null, string antalSpillere = null)//Metode: Søgefunktion til at finde spil på lagerlisten ved hjælp af forskellige søgekriterier (navn, genre, aldersgruppe, sprog, stand, antal spillere)
    {
		
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
