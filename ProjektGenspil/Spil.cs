using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProjektGenspil;

namespace ProjektGenspil
{
    internal class Spil
    {
        //class Spil Attributter, gælder for hver objekt
        private string navn { get; set; } = "Skriv navn";
        private string nyPris { get; set; } = "Skriv pris";
        private string alderGruppe;
        private string[] genre;
        private List<Kunder> forespørgsler = new List<Kunder> { };

        //genre og aldergruppe "muligheder" for hver objekt
        private string[] alleAlderGrupper = { "Alder Grupper:", "0-7 år", "fra 8 år" };
        private string[] alleGenrer = { "Genre:", "RPG", "Familiespil", "Børnespil", "Brætspil med kort" };


        //colors interface
        public string greenCursor = "=> \u001b[32m";
        public string nonColor = "\u001b[0m";
        public string BGYellow = "\u001b[43m";
        public void BGC()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
        }
        public void NonBGC()
        {
            Console.ResetColor();
        }

        //overloaded constructor for tempporarity calling
        public Spil()
        {
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }

        //constructor bliver kun kaldt af metoder i selve Spil klassen
        private Spil(string navn, string pris, string alder)
        {
            this.navn = navn;
            this.nyPris = pris;
            this.alderGruppe = alder;
            genre = new string[] { };
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }


        //en formular til at udfylde info om spillet
        public void OpretSpil()
        {
            //make a jagged array where each position is assigned a number, use the arrow keys to move the cursor
            string[][] jarray = new string[4][];
            jarray[0] = new string[] { "Navn:", (BGYellow + navn + nonColor) };
            jarray[1] = new string[] { "Ny Pris:", (BGYellow + nyPris + nonColor) };
            jarray[2] = alleAlderGrupper;
            jarray[3] = alleGenrer;

            //temporary variables
            string tempAG = "not defined";
            string[] tempGenrer = new string[] { };
            int ver = 0;
            int hor = 0;
            bool stillNotDone = true;
            ConsoleKeyInfo key;


            while (stillNotDone)
            {
                Console.SetCursorPosition(0, 5);

                foreach (string[] array in jarray)
                {
                    foreach (string s in array)
                    {
                        Console.Write((jarray[ver][hor] == s ? ($"{greenCursor}{s.PadRight(20)}{nonColor}") : s.PadRight(20)));
                    }
                    Console.WriteLine();
                }

                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        hor = 0;
                        ver = (ver == jarray.Length - 1 ? 0 : ver + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        hor = 0;
                        ver = (ver == 0 ? jarray.Length - 1 : ver - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        hor = (hor == jarray[ver].Length - 1 ? 0 : hor + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        hor = (hor == 0 ? jarray[ver].Length - 1 : hor - 1);
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
                                tempAG = jarray[ver][hor];
                            }
                            else
                            {
                                string a = jarray[ver][hor];
                                tempGenrer = [.. tempGenrer, a];
                            }
                            Console.Clear();
                            MyInterface.printHeader();
                        }
                        break;
                    case ConsoleKey.F5:
                        Spil saveSpil = new Spil()
                        {
                            navn = jarray[0][1],
                            nyPris = jarray[1][1],
                            alderGruppe = tempAG,
                            genre = tempGenrer
                        };
                        MyInterface.spilList.Add(saveSpil);
                        stillNotDone = false;
                        break;
                }
            }
        }

        //test print
        public void printSpilInfo()
        {
            foreach (Spil a in MyInterface.spilList)
            {
                Console.WriteLine(a.navn);
                Console.WriteLine(a.nyPris);
                Console.WriteLine(a.alderGruppe);
                foreach (string s in a.genre) Console.WriteLine(s);
            }
        }

        public void tilføjeKunder(Kunder a)
        {
            forespørgsler.Add(a);
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
        Console.Write("F1 Lager\tF2 Opret Spil\tF5 Gem\tF9 Udskriv");
    }
}
