using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektGenspil;

namespace ProjektGenspil
{
    internal class SpilKopi : Spil
    {        
        public double pris;
        public string sprog = "";
        public string stand="";
        public Kunder reservation;
        protected int spilListIndex;
        protected Spil main;

        private string Navn { get { return main.Navn; } }
        private double NyPris { get { return main.NyPris; } }
        private string AlderGruppe { get { return main.AlderGruppe; } }
        private string AntalSpillere { get { return main.AntalSpillere; } }
        private string[] Genre { get { return main.Genre; } }

        public SpilKopi(string navn, double nyPris, string alder, string antalSpillere, string[] genre, double pris, string stand)
            : base(navn, nyPris, alder, antalSpillere, genre)
        {
            this.pris = pris;
            this.stand = stand;
        }
        public SpilKopi(Spil spil)
        {
            main = spil;
        }

        public override string PrintSpilToMenu()
        {
            string line = (Navn == null ? "error" : Navn.PadRight(20) + Convert.ToString(pris).PadRight(20) + sprog.PadRight(20) + stand.PadRight(20));
            return line;
        }

        public override void Formular ()
        {
            string[][] jarray = new string[8][];
            jarray[0] = ["Navn:", Navn];
            jarray[1] = ["Ny Pris:", Convert.ToString(NyPris)];
            jarray[2] = ["Alder Grupper:", AlderGruppe];
            jarray[3] = ["Antal Spillere:", AntalSpillere];
            jarray[4] = ["Genrer:", ..Genre];
            jarray[5] = (pris == null ? ["Pris:", "skriv pris"] : ["Pris:", Convert.ToString(pris)]);
            jarray[6] = ["Sprog:", .. StamData.alleSprog];
            jarray[7] = ["Stand:", .. StamData.alleStand];
            //string empty = "";
            List<string> allInfoToList = [Convert.ToString(pris), sprog, stand];

            Menu spilKopiFormular = new Menu(jarray, allInfoToList, 1, 2, 0,5);
            spilKopiFormular.PrintMenu(0, 3, true);
        }

        public override void ConvertFromList(List<string> allInfoToList)
        {
            pris = Convert.ToDouble(allInfoToList[0]);
            sprog = allInfoToList[1];
            stand = allInfoToList[2];
        }
        public void Reserver (Kunder kunde)
        {
            reservation = kunde;
        }

        public string SaveGamesCopyToString ()
        {
            string lines = $"{pris},{sprog},{stand}";
            return lines;
        }
    }
}
