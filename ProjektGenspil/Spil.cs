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
        private string navn { get; set; } = "Skriv navn";
        private double nyPris { get; set; }
        private string alderGruppe;
        private string antalSpillere;
        private string[] genre;
        private List<SpilKopi> kopiPåLager = new();
        private List<Kunde> forespørgsler = new();

        //public properties
        public string Navn { get { return navn; } }
        public double NyPris { get { return nyPris; } }
        public string AlderGruppe { get { return alderGruppe; } }
        public string AntalSpillere { get { return antalSpillere; } }
        public string[] Genre { get { return genre; } }
        public List<Kunde> Forespørgsler { get { return forespørgsler; } }
        

        //overloaded constructor simpel version
        public Spil()
        {
            forespørgsler = new List<Kunde>(); //erklæres at det nye objekt også har en forespørgsel list
        }

        //constructor bliver kun kaldt af metoder i selve Spil klassen
        public Spil(string navn, double pris, string alder, string antalSpillere, string[] genre)
        {
            this.navn = navn;
            this.nyPris = pris;
            this.alderGruppe = alder;
            this.antalSpillere = antalSpillere;
            this.genre = genre;
            forespørgsler = new List<Kunde>(); //erklæres at det nye objekt også har en forespørgsel list
        }

        //en formular til at udfylde info om spillet, kan bliver kaldet med Spil.OpretSpil();
        public static void OpretSpil()
        {
            //make an instance of Spil klasse
            Spil tempSpil = new Spil();
            tempSpil.OpdaterSpil();
            Lagersystem.Lager.Add(tempSpil);
            
            //MyInterface.spilList.Add(tempSpil);
        }
                
        //starter en formular for at udfylde spil information, gemmer med F5
        public void OpdaterSpil()
        {
            MyInterface.printHeader();
            //make a jagged array where each position is assigned a number, use the arrow keys to move the cursor
            string[][] jarray = new string[5][];
            jarray[0] = new string[] { "Navn:", navn };
            jarray[1] = new string[] { "Ny Pris:", nyPris.ToString() };
            jarray[2] = ["Alder Grupper:",..StamData.alleAlderGrupper];
            jarray[3] = ["Antal Spillere:",..StamData.alleAntalSpillere];
            jarray[4] = ["Genrer:", ..StamData.alleGenrer]; 

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
                        if (tempSpilStringList.Contains(s)) { a = MyInterface.Selected + s + MyInterface.NonColor; } else { a = s; }
                        Console.Write((jarray[ver][hor] == s ? ($"{MyInterface.BGYellow}{(a + MyInterface.NonColor).PadRight(20)}") : a.PadRight(20)));
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
                            }
                        Console.Clear();
                        MyInterface.printHeader();                        
                        break;
                    case ConsoleKey.F5:
                        {
                            navn = jarray[0][1];

                            //nyPris = jarray[1][1] - Konvertering af string til double 
                            if (double.TryParse(jarray[1][1], out double nyPris))
                            {
                                Console.WriteLine($"Ny Pris: {nyPris}");
                            }
                            else
                            {
                                Console.WriteLine("Kunne ikke konvertere nyPris til double.");
                            }
                            genre = tempGenrer.ToArray();
                        }
                        stillNotDone = false;
                        break;
                }
            }
            Console.Clear();
        }

        //udskriver Spil information på en linje
        public void PrintSpilOneLine()
        {
            Console.WriteLine(navn.PadRight(20) + nyPris.ToString().PadRight(20) + alderGruppe.PadRight(20) + antalSpillere.PadRight(20) + Convert.ToString(kopiPåLager.Count).PadRight(20) + forespørgsler.Count);
        }

        //tilføje en Spilkopi til listen
        public void TilføjeSpilKopi (SpilKopi kopi)
        {
            kopiPåLager.Add(kopi);
        }

        //tilføje en kunde til førespørgsel listen
        public void TilføjeKunderForespørgsel(Kunde kunde)
        {
            forespørgsler.Add(kunde);
        }

        //converterer spil information til en format
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