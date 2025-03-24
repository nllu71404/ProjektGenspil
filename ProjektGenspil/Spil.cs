using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjektGenspil;

namespace ProjektGenspil
{
    internal class Spil
    {
        //class Spil Attributter, gælder for hver objekt
        public string navn { get; private set; } = "Skriv navn";
        private string nyPris { get; set; } = "Skriv pris";
        private string alderGruppe;
        private string antalSpillere;
        public string[] genre;
        public List<SpilKopi> kopiPåLager = new();
        public List<Kunder> forespørgsler = new();  

        //genre og aldergruppe stamdata
        private static string[] alleAlderGrupper = { "Alder Grupper:", "4-5 år", "6-7 år", "8-9 år", "10-12 år", "13-16 år", "16 år+", "18 år+", "8 år", "10 år" };
        private static string[] alleAntalSpillere = { "Antal Spillere:", "2-4 spillere", "3-6 spillere" };
        private static string[] alleGenrer = { "Genre:", "klassiske spil", "selskabsspil", "familiespil", "voksenspil", "strategispil", "børnespil", "quiz", "Små spil", "Escape Room", "Rollespile", "Bingo/banko", "Udvidelse" };


        //colors interface
        private static string greenCursor = "\u001b[32m";
        private static string nonColor = "\u001b[0m";
        private static string BGYellow = "\u001b[43m";
        private static string selected = "\u001b[36;1m";


        //overloaded constructor simpel version
        public Spil()
        {
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }

        //constructor bliver kun kaldt af metoder i selve Spil klassen
        public Spil(string navn, string pris, string alder, string antalSpillere, string[] genre)
        {
            this.navn = navn;
            this.nyPris = pris;
            this.alderGruppe = alder;
            this.antalSpillere = antalSpillere;
            this.genre = genre;
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }


        //en formular til at udfylde info om spillet, kan bliver kaldet med Spil.OpretSpil();
        public static void OpretSpil()
        {
            //make an instance of Spil klasse
            Spil tempSpil = new Spil();
            tempSpil.OpdaterSpil();
            MyInterface.spilList.Add(tempSpil);
        }

        public static void SletSpil()
        {
            
        }

        public void OpdaterSpil()
        {
            MyInterface.printHeader();
            //make a jagged array where each position is assigned a number, use the arrow keys to move the cursor
            string[][] jarray = new string[5][];
            jarray[0] = new string[] { "Navn:", navn };
            jarray[1] = new string[] { "Ny Pris:", nyPris };
            jarray[2] = alleAlderGrupper;
            jarray[3] = alleAntalSpillere;
            jarray[4] = alleGenrer;

            //temporary variables            
            List<string> tempGenrer = new();
            if (genre != null) { tempGenrer = genre.ToList(); }
            List<string> tempSpilStringList = new List<string> { alderGruppe, antalSpillere };
            tempSpilStringList.AddRange(tempGenrer);

            int ver = 0;
            int hor = 1;
            bool stillNotDone = true;
            ConsoleKeyInfo key;


            while (stillNotDone)
            {
                Console.SetCursorPosition(0, 3);

                foreach (string[] array in jarray)
                {
                    foreach (string s in array)
                    {
                        string a = "";
                        if (tempSpilStringList.Contains(s)) { a = selected + s + nonColor; } else { a = s; }
                        Console.Write((jarray[ver][hor] == s ? ($"{BGYellow}{(a + nonColor).PadRight(20)}") : a.PadRight(20)));
                    }
                    Console.WriteLine();
                }

                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        hor = 1;
                        ver = (ver == jarray.Length - 1 ? 0 : ver + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        hor = 1;
                        ver = (ver == 0 ? jarray.Length - 1 : ver - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        hor = (hor == jarray[ver].Length - 1 ? 1 : hor + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        hor = (hor == 1 ? jarray[ver].Length - 1 : hor - 1);
                        break;
                    case ConsoleKey.Spacebar:
                        if (hor == 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (ver < 2)
                            {
                                Console.CursorVisible = true;
                                jarray[ver][hor] = Console.ReadLine();
                                Console.CursorVisible = false;
                            }
                            else if (ver == 2)
                            {
                                tempSpilStringList.Remove(alderGruppe);
                                alderGruppe = jarray[ver][hor];
                                tempSpilStringList.Add(alderGruppe);
                                //jarray[ver][hor] = selected + jarray[ver][hor] + nonColor;
                            }
                            else if (ver == 3)
                            {
                                tempSpilStringList.Remove(antalSpillere);
                                antalSpillere = jarray[ver][hor];
                                tempSpilStringList.Add(antalSpillere);
                            }
                            else
                            {
                                if (tempSpilStringList.Contains(jarray[ver][hor]))
                                {
                                    tempGenrer.Remove(jarray[ver][hor]);
                                    tempSpilStringList.Remove(jarray[ver][hor]);
                                }
                                else
                                {
                                    tempGenrer.Add(jarray[ver][hor]);
                                    tempSpilStringList.Add(jarray[ver][hor]);
                                }
                                //jarray[ver][hor] = selected + jarray[ver][hor] + nonColor;
                            }
                            Console.Clear();
                            MyInterface.printHeader();
                        }
                        break;
                    case ConsoleKey.F5:
                        {
                            navn = jarray[0][1];
                            nyPris = jarray[1][1];
                            genre = tempGenrer.ToArray();
                        }
                        stillNotDone = false;
                        break;
                }
            }
            Console.Clear();
        }


        //test print
        public void PrintSpilOneLine()
        {
            Console.WriteLine(navn.PadRight(20) + nyPris.PadRight(20) + alderGruppe.PadRight(20) + antalSpillere.PadRight(20) + Convert.ToString(kopiPåLager.Count).PadRight(20) + forespørgsler.Count);
        }

        public void TilføjeKunderForespørgsel(Kunder a)
        {
            forespørgsler.Add(a);
        }
        public string ConvertSpilInfoToSave()
        {
            string s = $"{navn},{nyPris},{alderGruppe},{antalSpillere}";
            foreach (string a in genre)
            {
                s = s + "," + a;
            }
            return s;
        }
    }
}
internal class MyInterface
{
    public static List<Spil> spilList = new List<Spil>();
    //List<Kopi> påLager = new List<Kopi>();


    public static void printHeader()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write("F1 Lager".PadRight(16) + "F2 Opret Spil".PadRight(16) + "F5 Gem".PadRight(16) + "F9 Udskriv");
    }


    public static void Menu()
    {
        ConsoleKeyInfo key;
        int choice = 0;
        while (true)
        {
            printHeader();
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("Navn".PadRight(20) + "Ny Pris".PadRight(20) + "Alder Grupper".PadRight(20) + "Antal Spillere".PadRight(20) + "Antal kopier".PadRight(20) + "Efterspørgsel".PadRight(20));
            Console.SetCursorPosition(0, 4);
            foreach (Spil spil in spilList)
            {
                if (choice == spilList.IndexOf(spil)) Console.Write(MyColor.green);
                spil.PrintSpilOneLine();
                Console.Write(MyColor.nonColor);
            }
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    choice = (choice == spilList.Count - 1 ? choice = 0 : choice + 1);
                    break;
                case ConsoleKey.UpArrow:
                    choice = (choice == 0 ? choice = spilList.Count - 1 : choice - 1);
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    spilList[choice].OpdaterSpil();
                    break;
                case ConsoleKey.F2:
                    Console.Clear();
                    Spil.OpretSpil();
                    break;
            }
            InputOutput.SaveSpil();
        }
    }

    public static void Search()
    {
        spilList.AsEnumerable();
    }
}