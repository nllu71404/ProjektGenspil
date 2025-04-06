using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class Kunder
    {
        public static List<Kunder> kundeList = new List<Kunder>();

        private string navn;
        private string mobilNummer;
        private string emailAdresse;
        public List<Spil> forespørgsel = new();


        private string spilForespørgsel;

        //Properties 
        public string Navn { get { return navn; } }
        public string MobilNummer { get { return mobilNummer; } }
        public string EmailAdresse { get { return emailAdresse; } }
        public string SpilForespørgsel { get { return spilForespørgsel;} }


        public Kunder(string navn, string mobilNummer, string emailAdresse, string spilForespørgsel) 
        {
            this.navn = navn;
            this.mobilNummer = mobilNummer;
            this.emailAdresse = emailAdresse;
            this.spilForespørgsel = spilForespørgsel;
        }

        public Kunder() 
        {
            List <Spil> forespørgsel = new();
        }

        public static void KundeMenu()
        {
            Menu.currentPage = menuPages.kunderMenu;
            string[][] jarray = new string[kundeList.Count + 1][];
            jarray[0] = ["Navn".PadRight(20), "Mobil Nummer".PadRight(20), "Email".PadRight(20), "Forespørgsler"];
            for (int i = 0; i < kundeList.Count; i++) { jarray[i + 1] = [kundeList[i].PrintKunderToMenu()]; }
            List<string> empty = new List<string>();

            Menu kundeMenu = new Menu(jarray, empty, 0, 0, 0, 1);
            kundeMenu.PrintMenu(0, 3, false);
        }
        public string PrintKunderToMenu()
        {
            string line = (navn.PadRight(20) + mobilNummer.PadRight(20) + emailAdresse.PadRight(20));
            return line;
        }

        public void KundeFormular ()
        {            
            string[][] jarray = new string[3][];
            jarray[0] = (navn == null? ["Navn:", "skriv navn"] : ["Navn:", navn]);
            jarray[1] = (mobilNummer == null ? ["Mobil Nummer:", "Indtast mobilnr."] : ["Mobil Nummer:", mobilNummer]);
            jarray[2] = (emailAdresse == null ? ["Email:", "Indtast Email."] : ["Email:", emailAdresse]);
            List<string> allInfoToList= [navn, mobilNummer, emailAdresse];

            Menu kundeMenu = new Menu(jarray, allInfoToList, 3, 0, 0,0);
            kundeMenu.PrintMenu(0,3,true);
        }

        public void ConvertFromList(List<string> allInfoToList)
        {
            navn = allInfoToList[0];
            mobilNummer = allInfoToList[1];
            emailAdresse = allInfoToList[2];
        }

        public static void ChooseKunder (Spil spil)
        {
            Menu.currentPage = menuPages.chooseKunde;
            string[][] jarray = new string[kundeList.Count + 1][];
            jarray[0] = ["Navn".PadRight(20), "Mobil Nummer".PadRight(20), "Email".PadRight(20), "Forespørgsler"];
            for (int i = 0; i < kundeList.Count; i++) { jarray[i + 1] = [kundeList[i].PrintKunderToMenu()]; }
            List<string> allInfoToList= new List<string>();
            foreach (Kunder kunde in spil.Forespørgsler)
            {
                allInfoToList.Add(kunde.PrintKunderToMenu());
            }
            Menu kundeMenu = new Menu(jarray, allInfoToList,0,0,kundeList.Count,1);
            kundeMenu.PrintMenu(0, 3, false);
        }

        public static void Reserver(SpilKopi kopi)  //not functioning
        {
            Menu.currentPage = menuPages.reserver;
            string[][] jarray = new string[kundeList.Count + 1][];
            jarray[0] = ["Navn".PadRight(20), "Mobil Nummer".PadRight(20), "Email".PadRight(20), "Forespørgsler"];
            for (int i = 0; i < kundeList.Count; i++) { jarray[i + 1] = [kundeList[i].PrintKunderToMenu()]; }
            List<string> allInfoToList = new();
            if (kopi.reservation!=null)
            {
                allInfoToList.Add(kopi.reservation.PrintKunderToMenu());
            }            
            Menu kundeMenu = new Menu(jarray, allInfoToList, 0, 1, 0, 1);
            kundeMenu.PrintMenu(0, 3, false);
        }

        public static void ForespørgselConvertFromList (List<string> allInfoToList, Spil spil)
        {
            foreach (var line in  allInfoToList)
            {
                foreach (Kunder kunde in kundeList)
                {
                    if (kunde.mobilNummer == line.Substring(20,8))
                    { 
                        spil.TilføjeKunderForespørgsel(kunde);
                        kunde.forespørgsel.Add(spil);
                    }
                }
            }
        }
        public static void ReservationConvertFromList(List<string> allInfoToList, SpilKopi kopi)
        {
            foreach (var line in allInfoToList)
            {
                foreach (Kunder kunde in kundeList)
                {
                    if (kunde.mobilNummer == line.Substring(20, 8))
                    {
                        kopi.reservation = kunde;
                    }
                }
            }
        }

        public static void SaveKunder()
        {
            InputOutput saveKunder = new InputOutput("Kunder.txt");
            List<string> list = new List<string>();
            foreach (var kunde in kundeList)
            {                
                string line = $"{kunde.navn},{kunde.mobilNummer},{kunde.emailAdresse}";
                if (kunde.forespørgsel != null)
                {
                    foreach (var spil in kunde.forespørgsel)
                    {
                        line = line + "," + spil.Navn;
                    }
                }
                list.Add(line);
            }
            saveKunder.SaveFile(list);
        }
        public static void LoadKunder()
        {
            InputOutput loadKunder = new InputOutput("Kunder.txt");
            foreach (var line in loadKunder.LoadFile())
            {
                kundeList.Add(FromString(line));
            }
        }
        public static Kunder FromString (string line)
        {
            Kunder tempKunde= new Kunder();
            string[] entries = line.Split(',');
            tempKunde.navn = entries[0];
            tempKunde.mobilNummer = entries[1];
            tempKunde.emailAdresse = entries[2];
            for (int i = 3;i<entries.Length;i++)
            {
                foreach (var spil in MyInterface.spilList)
                {
                    if (entries[i]==spil.Navn)
                    {
                        tempKunde.forespørgsel.Add(spil);
                        spil.Forespørgsler.Add(tempKunde);
                    }
                }
            }
            return tempKunde;
        }
    }    

}
