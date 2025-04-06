using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProjektGenspil;

namespace ProjektGenspil
{
    internal class Spil
    {
        //class Spil Attributter, gælder for hver objekt
        protected string navn { get; set; }
        protected double nyPris { get; set; }
        protected string alderGruppe = "";
        protected string antalSpillere = "";
        protected string[] genre = [""];
        protected List<SpilKopi> kopiPåLager = new();
        protected List<Kunder> forespørgsler = new();

        //public properties
        public string Navn { get { return navn; } }
        public double NyPris { get { return nyPris; } }
        public string AlderGruppe { get { return alderGruppe; } }
        public string AntalSpillere { get { return antalSpillere; } }
        public string[] Genre { get { return genre; } }
        public List<SpilKopi> KopiPåLager { get { return kopiPåLager; } }
        public List<Kunder> Forespørgsler { get { return forespørgsler; } }
        
        //overloaded constructor simpel version
        public Spil()
        {
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }

        //constructor bliver kun kaldt af metoder i selve Spil klassen
        public Spil(string navn, double pris, string alder, string antalSpillere, string[] genre)
        {
            this.navn = navn;
            this.nyPris = pris;
            this.alderGruppe = alder;
            this.antalSpillere = antalSpillere;
            this.genre = genre;
            forespørgsler = new List<Kunder>(); //erklæres at det nye objekt også har en forespørgsel list
        }
        

        //udskriver Spil information på en linje
        public virtual string PrintSpilToMenu()
        {            
            string line =(navn==null? "error":navn.PadRight(20)+ Convert.ToString(nyPris).PadRight(20) + alderGruppe.PadRight(20) + antalSpillere.PadRight(20) + Convert.ToString(kopiPåLager.Count).PadRight(20) + forespørgsler.Count);
            return line;
        }

        //tilføje en Spilkopi til listen
        public void TilføjeSpilKopi (SpilKopi kopi)
        {
            kopiPåLager.Add(kopi);
        }

        //tilføje en kunde til førespørgsel listen
        public void TilføjeKunderForespørgsel(Kunder kunde)
        {
            forespørgsler.Add(kunde);
        }

        //converterer spil information til en format
        

        public string SaveGamesToString()
        {
            string line = $"{navn},{nyPris},{alderGruppe},{antalSpillere}";
            foreach (string a in genre)
            {
                line = line + "," + a;
            }            
            foreach (SpilKopi kopi in kopiPåLager)
            {
                line = line + "|" + kopi.SaveGamesCopyToString();
            }
            return line;
        }

        private static List<string> SaveGamesToList()
        {
            List<string> list = new List<string>();
            foreach (Spil spil in MyInterface.spilList)
            {
                list.Add(spil.SaveGamesToString());
            }
            return list;
        }

        public static void SaveGames ()
        {
            InputOutput saveGames = new InputOutput("Games.txt");
            saveGames.SaveFile(SaveGamesToList());
        }

        public static void LoadGames()
        {
            InputOutput loadGames = new InputOutput("Games.txt");
            List<string> list = loadGames.LoadFile();
            foreach (string line in list)
            {
                string[] entries = line.Split("|");
                List <string> allInfoToList = entries[0].Split(",").ToList();
                Spil temp = new Spil();
                temp.ConvertFromList(allInfoToList);
                MyInterface.spilList.Add(temp);
                for (int i = 1; i < entries.Length; i++)
                {
                    List <string> listToKopi = entries[1].Split(",").ToList();
                    SpilKopi kopi = new SpilKopi(temp);
                    kopi.ConvertFromList(listToKopi);
                    temp.kopiPåLager.Add(kopi);
                    MyInterface.spilKopiList.Add(kopi);
                }
            }
        }



        public virtual void Formular()
        {            
            string[][] jarray = new string[5][];
            jarray[0] = (navn == null ? ["Navn:", "skriv navn"] : ["Navn:", navn]);
            jarray[1] = (nyPris == null ? ["Ny Pris:", "skriv pris"] : ["Ny Pris:", Convert.ToString(nyPris)]);
            jarray[2] = ["Alder Grupper:", .. StamData.alleAlderGrupper];
            jarray[3] = ["Antal Spillere:", .. StamData.alleAntalSpillere];
            jarray[4] = ["Genrer:", .. StamData.alleGenrer];

            List<string> allInfoToList = [navn==null?"navn":navn,  Convert.ToString(nyPris), alderGruppe, antalSpillere, ..genre];

            Menu spilMenu = new Menu(jarray, allInfoToList, 2, 2, 1,0);
            spilMenu.PrintMenu(0, 3, true);
        }
        public virtual void ConvertFromList (List<string> allInfoToList)
        {
            navn = allInfoToList[0];
            nyPris=Convert.ToDouble(allInfoToList[1]);
            alderGruppe=allInfoToList[2];
            antalSpillere=allInfoToList[3];
            genre = allInfoToList.ToArray()[4..allInfoToList.Count];
        }
    }
}