using System;
using ProjektGenspil;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Linq;

namespace ProjektGenspil;

internal class Lagersystem
{

	public static List<Spil> Lager = new List<Spil>();
	
	public static List<SpilKopi> LagerKopi = new List<SpilKopi>();



    public static void LagerMenu()
    {
        Menu.currentPage = menuPages.spilLagerMenu;
        string[][] jarray = new string[Lager.Count + 1][];
        jarray[0] = ["Navn".PadRight(20), "Ny Pris".PadRight(20), "Alder Gruppe".PadRight(20), "Antal Spillere".PadRight(20), "På Lager".PadRight(20), "Forespørgsler"];
        for (int i = 0; i < Lager.Count; i++) { jarray[i + 1] = [Lager[i].PrintSpilToMenu()]; }
        List<string> empty = new List<string>();

        Menu lagerMenu = new Menu(jarray, empty, 0, 0, 0, 1);
        lagerMenu.PrintMenu(0, 3, false);
    }


    public static void PåLager()
    {
        Menu.currentPage = menuPages.spilKopiMenu;
        string[][] jarray = new string[LagerKopi.Count + 1][];
        jarray[0] = ["Navn".PadRight(20), "Pris".PadRight(20), "Sprog".PadRight(20), "Stand".PadRight(20), "Reserveret af"];
        for (int i = 0; i < LagerKopi.Count; i++) { jarray[i + 1] = [LagerKopi[i].PrintSpilToMenu()]; }
        List<string> empty = new List<string>();

        Menu kopiMenu = new Menu(jarray, empty, 0, 0, 0, 1);
        kopiMenu.PrintMenu(0, 3, false);
    }

    public static void TilføjeNytSpil()
    {
        Spil tempSpil = new Spil();
        tempSpil.Formular();
    }


    public static void TilføjeEnSpilKopi(Spil spil)
    {
        SpilKopi tempKopi = new SpilKopi(spil);
        tempKopi.Formular();
    }


    public static void TilføjSpil(Spil spil) //Metode: tilføjer spil til lagerlisten
	{
		Lager.Add(spil);
		//evt udskrift af bekræftelse 
		//Console.WriteLine($"\n{spil} er nu tilføjet til lagerlisten");
	}

    public static void FjernSpil(List<Spil> Lager) // Metode: fjerner spil fra lagerlisten
    {
        Console.WriteLine("Skriv navnet på det spil, du vil fjerne fra lagerlisten:");
        string brugerInputSpilNavn = Console.ReadLine();

        // Find spillet i lagerlisten
        Spil spilAtFjerne = Lager.FirstOrDefault(spil => spil.Navn.Equals(brugerInputSpilNavn, StringComparison.OrdinalIgnoreCase));

        if (spilAtFjerne != null)
        {
            Lager.Remove(spilAtFjerne);
            // Print bekræftelse
            Console.WriteLine($"{spilAtFjerne.Navn} er nu fjernet fra lagerlisten.");
        }
        else
        {
            // Print besked hvis spillet ikke findes
            Console.WriteLine($"Spillet med navnet {brugerInputSpilNavn} blev ikke fundet i lagerlisten.");
        }
    }


    public static List<Spil> SøgSpil(List<Spil> Lager, string navn, string valgtGenre, double MinPris, double MaxPris, string valgtAlder, string valgtAntalSpillere)
    {
        var resultater = Lager.Where(spil =>
            (string.IsNullOrEmpty(navn) || spil.Navn.Contains(navn)) &&
            (string.IsNullOrEmpty(valgtGenre) || (spil.Genre != null && spil.Genre.Contains(valgtGenre))) &&
            (spil.NyPris >= MinPris && spil.NyPris <= MaxPris) &&
            (string.IsNullOrEmpty(valgtAlder) || spil.AlderGruppe == valgtAlder) &&
            (string.IsNullOrEmpty(valgtAntalSpillere) || spil.AntalSpillere == valgtAntalSpillere)
        ).ToList();

        return resultater;

    }


    public static void BrugerInputSøgSpil() 
    {
        // Titel
        Console.WriteLine("||Søg efter spil||");

        // Bruger-Input
        Console.WriteLine("\nIndtast navn (eller tryk Enter for at springe over):");
        string navn = Console.ReadLine();

        Console.WriteLine("Indtast minimum pris (eller tryk Enter for at springe over):");
        string minPrisInput = Console.ReadLine();
        double minPris = string.IsNullOrEmpty(minPrisInput) ? 0 : double.Parse(minPrisInput);

        Console.WriteLine("Indtast maksimum pris (eller tryk Enter for at springe over):");
        string maxPrisInput = Console.ReadLine();
        double maxPris = string.IsNullOrEmpty(maxPrisInput) ? double.MaxValue : double.Parse(maxPrisInput);

        Console.WriteLine("Vælg en genre ved at indtaste nummeret (eller tryk Enter for at springe over):");
        for (int i = 0; i < StamData.alleGenrer.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {StamData.alleGenrer[i]}");
        }
        string genreInput = Console.ReadLine();
        string valgtGenre = string.IsNullOrEmpty(genreInput) ? null : StamData.alleGenrer[int.Parse(genreInput) - 1];

        Console.WriteLine("Vælg en aldersgruppe ved at indtaste nummeret (eller tryk Enter for at springe over):");
        for (int i = 0; i < StamData.alleAlderGrupper.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {StamData.alleAlderGrupper[i]}");
        }
        string alderInput = Console.ReadLine();
        string valgtAlder = string.IsNullOrEmpty(alderInput) ? null : StamData.alleAlderGrupper[int.Parse(alderInput) - 1];

        Console.WriteLine("Vælg antal spillere ved at indtaste nummeret (eller tryk Enter for at springe over):");
        for (int i = 0; i < StamData.alleAntalSpillere.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {StamData.alleAntalSpillere[i]}");
        }
        string antalSpillereInput = Console.ReadLine();
        string valgtAntalSpillere = string.IsNullOrEmpty(antalSpillereInput) ? null : StamData.alleAntalSpillere[int.Parse(antalSpillereInput) - 1];

        var resultater = Lagersystem.SøgSpil(Lagersystem.Lager, navn, valgtGenre, minPris, maxPris, valgtAlder, valgtAntalSpillere);

        // Udprint af søgeresultater
        Console.WriteLine("Søgeresultater:");
        foreach (Spil spil in resultater)
        {
            Console.WriteLine($"Navn: {spil.Navn}, Genre: {spil.Genre}, Pris: {spil.NyPris}, Alder: {spil.AlderGruppe}, Antal Spillere: {spil.AntalSpillere}");
        }

        Console.ReadLine();
        Lagersystem.LagerMenu();

    }



    public static void PrintLagerstatus() //Metode: udskriver en sorteret lagerlisten/ spil-listen
	{
        var sorteredeLager = Lager.OrderBy(spil => spil.Navn).ToList(); // Sorter efter navn

        foreach (Spil spil in sorteredeLager)
        {
            Console.WriteLine($"Navn: {spil.Navn}, Genre: {spil.Genre[0]}, Pris: {spil.NyPris} kr, Alder: {spil.AlderGruppe}, Antal Spillere: {spil.AntalSpillere}");
        }
        Console.ReadLine();
        Lagersystem.LagerMenu();
    }

	public static void PrintForespørgsler(Spil spil) //Metode: Udskriver liste med forespørgsler sorteret efter Spil-navn 
	{
        var sorteredeForespørgsler = spil.Forespørgsler.OrderBy(kunde => kunde.SpilForespørgsel).ToList();

        Console.WriteLine("\n||Forespørgsler||");
        Console.WriteLine("");

        foreach (Kunder kunde in sorteredeForespørgsler) 
		{

            Console.WriteLine($"\nSpil: {kunde.SpilForespørgsel}"); 
            Console.WriteLine($"Forespurgt af: {kunde.Navn}, Tlf: {kunde.MobilNummer}, Email: {kunde.EmailAdresse}");
		}
        Console.ReadLine();
        Lagersystem.LagerMenu();
    }
}
