using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class MyInterface
    {        
        public static List<Spil> spilList = new List<Spil>();
        public static List<SpilKopi> spilKopiList = new List<SpilKopi>();
        public static List<Spil> spilListSorting = spilList;
        

        public static void LagerMenu()
        {
            Menu.currentPage = menuPages.spilLagerMenu;
            string[][] jarray = new string[spilList.Count+1][];
            jarray[0] = ["Navn".PadRight(20),"Ny Pris".PadRight(20),"Alder Gruppe".PadRight(20),"Antal Spillere".PadRight(20),"På Lager".PadRight(20),"Forespørgsler"];
            for (int i = 0; i < spilList.Count; i++) { jarray[i+1] = [spilList[i].PrintSpilToMenu()]; }
            List<string> empty = new List<string>();

            Menu lagerMenu = new Menu(jarray, empty,0,0,0,1);
            lagerMenu.PrintMenu(0,3,false);
        }
                

        public static void PåLager()
        {
            Menu.currentPage = menuPages.spilKopiMenu;
            string[][] jarray = new string[spilKopiList.Count + 1][];
            jarray[0] = ["Navn".PadRight(20), "Pris".PadRight(20), "Sprog".PadRight(20), "Stand".PadRight(20),"Reserveret af"];
            for (int i = 0; i < spilKopiList.Count; i++) { jarray[i + 1] = [spilKopiList[i].PrintSpilToMenu()];}
            List<string> empty = new List<string>();

            Menu kopiMenu = new Menu (jarray, empty,0,0,0,1);
            kopiMenu.PrintMenu(0,3,false);
        }

        public static void TilføjeNytSpil ()
        {
            Spil tempSpil = new Spil();
            tempSpil.Formular();
        }
        
        public static void FjernEtSpil(Spil spil)
        {
            spilList.Remove(spil);
        }

        public static void TilføjeEnSpilKopi (Spil spil)
        {
            SpilKopi tempKopi = new SpilKopi(spil);
            tempKopi.Formular();
        }
    }
}
