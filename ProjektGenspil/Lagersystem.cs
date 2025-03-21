﻿using System;
using ProjektGenspil;
using System.Collections.Generic;
using System.Linq;

namespace ProjektGenspil;

public class Lagersystem
{
	private List<Spil> Lager;

	public Lagersystem()
	{
		Lager = new List<Spil>();
	}


	public void TilføjSpil(Spil spil) //Metode: tilføjer spil til lagerlisten
	{
		Lager.Add(spil);
	}

	public void FjernSpil(Spil spil) //Metode: fjerner spil fra lagerlisten
	{
	if(Lager.Contains(spil))
			{
			Lager.Remove(spil);
			//Evt print af bekræftelse 
			Console.WriteLine($"{spil.Navn} er fjernet fra lagerlisten"); //Der skal laves en properties/egenskaber af Spil-klassens attributter, så de andre klasser kan tilgå dem 
		} 
	else
		{
			Console.WriteLine($"{spil.Navn} findes ikke i lagerlisten");
		}
	}


    public List<Spil> SøgSpil(string navn = null, Enum genre = null, double pris = 0.00, Enum aldersgruppe = null, int SpillerAntal = 0, Enum sprog = null) //FEJL - ER IKKE FÆRDIG //Metode: Søgefunktion til at finde spil på lagerlisten ved hjælp af forskellige søgekriterier (navn, genre, aldersgruppe, sprog, stand, antal spillere)
    {
		var result = Lager.Where(spil =>
			(navn == null || spil.Navn.Contains(navn, StringComparison.OrdinalIgnoreCase)) ||
			(genre == null || spil.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToString();
        //IKKE FÆRDIG - I PROCESS
       

        return result;
    }

	public void PrintLagerstatus() //Metode: udskriver en sorteret lagerlisten/ spil-listen
	{
		foreach (Spil spil in Lager)
		{
			Lager.Sort();
			Console.WriteLine($"{spil}");
		}
	}

	public void PrintForespørgsler() //Metode: Udskriver liste med forespørgsler  - Mangler info fra Spil-klassen
	{
		//Mangler info fra spil-klassen

		foreach (Spil kunde in Forespørgsler) //Der skal oprettes en liste, der hedder Forespørgsler
		{
			Console.WriteLine($"{kunde}");
		}
	}
}
