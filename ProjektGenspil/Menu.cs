using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ProjektGenspil
{
    public enum menuPages
    {
        spilLagerMenu,
        opdaterSpilForm,
        nytSpilForm,
        spilKopiMenu,
        opdaterSpilKopiForm,
        nytSpilKopiForm,
        kunderMenu,
        nyKunderForm,
        opdaterKunderForm,
        chooseKunde,
        reserver
    }
    internal class Menu
    {
        public static menuPages currentPage = menuPages.spilLagerMenu;
                
        private string[][] jarray;
        private int hor = 0, ver = 0;
        private int horOffset=0;
        private int verOffset=0;
        private ConsoleKeyInfo key;
        private bool stillNotDone = true;
        private List<string> allInfoToList;
        private int verToBeFilled, verOneChoice, verMultipleChoices;
        private static int index;


        //private readonly static string greenCursor = "=> \u001b[32m";
        private readonly static string selected = "\u001b[32m";
        private readonly static string nonColor = "\u001b[0m";
        private readonly static string bGYellow = "\u001b[43m";
        //private readonly static string selected = "\u001b[36;1m";

        
        public Menu(string[][] jarray, List<string> allInfoToList, int verToBeFilled, int verOneChoice, int verMultipleChoices, int verOffset)
        {
            this.jarray = jarray;
            this.verToBeFilled = verToBeFilled;
            this.verOneChoice = verOneChoice;
            this.verMultipleChoices = verMultipleChoices;
            this.allInfoToList = allInfoToList;
            this.verOffset = verOffset;
        }
        public void PrintMenu (int left, int top, bool højreMenuExist)
        {
            if (højreMenuExist) { horOffset = 1; hor = 1; }
            ver = verOffset;
            Console.Clear();
            while (stillNotDone)
            {
                PrintHeader();
                Console.SetCursorPosition(left, top);
                PrintContent();
                PrintAdditionalContent();
                ChoiceWithKeys();
            }
        }
        private static void PrintHeader()
        {
            Console.SetCursorPosition(0, 0);
            string header = "";
            switch (currentPage)
            {
                case menuPages.spilLagerMenu:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16) + "F5 Søg".PadRight(16) + "F6 Opret Spil".PadRight(16) + "F7 Opret Kopi".PadRight(16)+"F9 Print Lagerstatus";
                    break;
                case menuPages.spilKopiMenu:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16) + "F5 Søg".PadRight(16);
                    break;
                case menuPages.kunderMenu:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16) + "F5 Søg".PadRight(16) + "F6 Opret Kunde".PadRight(16);
                    break;
                case menuPages.opdaterSpilForm:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16) + "F6 Forespørgsel".PadRight(16)+ "F8 Gem".PadRight(16);
                    break;
                case menuPages.opdaterSpilKopiForm:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16)  + "F8 Gem".PadRight(16);
                    break;
                case menuPages.nytSpilForm:
                case menuPages.nytSpilKopiForm:
                case menuPages.nyKunderForm:
                case menuPages.opdaterKunderForm:
                    header = "F1 Spil katalog".PadRight(16) + "F2 På lager".PadRight(16) + "F3 Kunder".PadRight(16) + "F8 Gem".PadRight(16);
                    break;
            }
            Console.Write(header);
        }
        private void PrintContent ()
        {
            if(jarray.Length < 2) { jarray = [jarray[0],["empty"]]; }
            foreach (string[] row in jarray)
            {
                foreach (string s in row)
                {
                    string a = s;
                    if (allInfoToList.Contains(s)) { a = selected + s + nonColor; }
                    Console.Write((jarray[ver][hor] == s ? ($"{bGYellow}{(a + nonColor).PadRight(20)}") : a.PadRight(20)));
                }
                Console.WriteLine();
            }
        }
        private void PrintAdditionalContent()
        {
            if (currentPage==menuPages.opdaterSpilForm)
            {
                Console.Write("Forespørgsler:");
                foreach (Kunder kunde in Lagersystem.Lager[index].Forespørgsler)
                {
                    Console.Write(kunde.Navn.PadRight(8));
                }
            }
            else if (currentPage==menuPages.opdaterSpilKopiForm)
            {
                Console.Write("Reserveret af:");
                if (Lagersystem.LagerKopi[index].reservation != null)
                {
                    Console.Write(Lagersystem.LagerKopi[index].reservation.Navn.PadRight(8));
                }
            }
            else if(currentPage==menuPages.opdaterKunderForm)
            {
                Console.Write("Forespørgsler:");
                foreach (Spil spil in Kunder.kundeList[index].forespørgsel)
                {
                    Console.Write(spil.Navn.PadRight(8));
                }
            }
        }
        private void ChoiceWithKeys ()
        {
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    hor = horOffset;
                    ver = (ver==jarray.Length-1 ? verOffset : ver+1);
                    break;
                case ConsoleKey.UpArrow:
                    hor = horOffset;
                    ver = (ver==verOffset ? jarray.Length-1 : ver-1);
                    break;
                case ConsoleKey.RightArrow:
                    hor = (hor == jarray[ver].Length-1? horOffset:hor+1);
                    break;
                case ConsoleKey.LeftArrow:
                    hor= (hor == horOffset? jarray[ver].Length-1 : hor-1);
                    break;
                case ConsoleKey.Spacebar:
                    SpaceBarButton();
                    break;
                case ConsoleKey.Enter:
                    EnterButton();
                    break;
                case ConsoleKey.F1:
                    stillNotDone= false;
                    Lagersystem.LagerMenu();
                    break;
                case ConsoleKey.F2:
                    stillNotDone= false;
                    Lagersystem.PåLager();
                    break;
                case ConsoleKey.F3:
                    stillNotDone = false;
                    Kunder.KundeMenu();
                    break;
                case ConsoleKey.F5:
                    stillNotDone = false;
                    Lagersystem.BrugerInputSøgSpil();
                    break;
                case ConsoleKey.F6:
                    if (currentPage==menuPages.spilLagerMenu)
                    {
                        currentPage=menuPages.nytSpilForm;
                        stillNotDone = false;
                        Lagersystem.TilføjeNytSpil();
                    }
                    else if (currentPage==menuPages.kunderMenu)
                    {
                        currentPage = menuPages.nyKunderForm;
                        stillNotDone = false;
                        Kunder kunde = new Kunder();
                        kunde.KundeFormular();
                    }
                    else if (currentPage==menuPages.opdaterSpilForm)
                    {
                        stillNotDone = false;
                        Kunder.ChooseKunder(Lagersystem.Lager[index]);
                    }
                    break;
                case ConsoleKey.F7:
                    if (currentPage == menuPages.spilLagerMenu)
                    {
                        currentPage=menuPages.nytSpilKopiForm;
                        stillNotDone = false;
                        index = ver - verOffset;
                        Lagersystem.TilføjeEnSpilKopi(Lagersystem.Lager[ver-verOffset]);
                    }
                    break;
                case ConsoleKey.F8:
                    F8ButtonSave();
                    break;
                case ConsoleKey.F9:
                    Lagersystem.PrintLagerstatus();
                    break;
            }
        }
        private void SpaceBarButton ()
        {
            if (ver<verToBeFilled+verOffset)
            {
                Console.CursorVisible = true;
                jarray[ver][hor] = Console.ReadLine();
                allInfoToList[ver-verOffset] = jarray[ver][hor];
                Console.CursorVisible = false;
            }
            else if (ver<verToBeFilled+verOneChoice+verOffset)
            {
                allInfoToList[ver-verOffset] = jarray[ver][hor];
            }
            else if (currentPage==menuPages.nytSpilForm||currentPage==menuPages.opdaterSpilForm||currentPage==menuPages.chooseKunde)
            {
                if (allInfoToList.Contains(jarray[ver][hor]))
                {                    
                    allInfoToList.Remove(jarray[ver][hor]);
                }
                else
                {
                    allInfoToList.Add(jarray[ver][hor]);
                }
            }
            Console.Clear();
        }
        private void EnterButton ()
        {
            stillNotDone=false;
            if (currentPage== menuPages.spilLagerMenu)
            {
                currentPage = menuPages.opdaterSpilForm;
                index = ver - verOffset;
                Lagersystem.Lager[ver-verOffset].Formular();
            }
            else if (currentPage==menuPages.spilKopiMenu)
            {
                currentPage = menuPages.opdaterSpilKopiForm;
                index = ver - verOffset;
                Lagersystem.LagerKopi[ver-verOffset].Formular();
            }
            else if (currentPage==menuPages.kunderMenu)
            {
                currentPage=menuPages.opdaterKunderForm;
                index= ver - verOffset;
                Kunder.kundeList[index].KundeFormular();
            }
        }
        private void F8ButtonSave ()
        {
            if (currentPage==menuPages.nytSpilForm)
            {
                Spil temp = new Spil();
                temp.ConvertFromList(allInfoToList);
                Lagersystem.Lager.Add(temp);
                stillNotDone = false;
                Spil.SaveGames();
                Lagersystem.LagerMenu();
            }
            else if (currentPage==menuPages.opdaterSpilForm)
            {
                Lagersystem.Lager[index].ConvertFromList(allInfoToList);
                stillNotDone = false;
                Spil.SaveGames();
                Lagersystem.LagerMenu();
            }
            else if (currentPage==menuPages.nytSpilKopiForm)
            {
                SpilKopi temp = new SpilKopi(Lagersystem.Lager[index]);
                temp.ConvertFromList(allInfoToList);
                Lagersystem.Lager[index].TilføjeSpilKopi(temp);
                Lagersystem.LagerKopi.Add(temp);
                stillNotDone=false;
                Spil.SaveGames();
                Lagersystem.PåLager();
            }
            else if (currentPage==menuPages.opdaterSpilKopiForm)
            {
                Lagersystem.LagerKopi[index].ConvertFromList(allInfoToList);
                stillNotDone=false;
                Spil.SaveGames();
                Lagersystem.PåLager();
            }
            else if (currentPage==menuPages.nyKunderForm)
            {
                Kunder temp = new Kunder();
                temp.ConvertFromList(allInfoToList);
                Kunder.kundeList.Add(temp);
                stillNotDone=false;
                Kunder.SaveKunder();
                Kunder.KundeMenu();
            }
            else if (currentPage==menuPages.opdaterKunderForm)
            {
                Kunder.kundeList[index].ConvertFromList(allInfoToList);
                stillNotDone = false;
                Kunder.SaveKunder();
                Kunder.KundeMenu();
            }
            else if (currentPage==menuPages.chooseKunde)
            {
                Kunder.ForespørgselConvertFromList(allInfoToList, Lagersystem.Lager[index]);
                stillNotDone = false;
                Kunder.SaveKunder();
                currentPage=menuPages.opdaterSpilForm;
                Lagersystem.Lager[index].Formular();
            }
            else if (currentPage==menuPages.reserver)
            {
                Kunder.ForespørgselConvertFromList(allInfoToList, Lagersystem.LagerKopi[index]);
                stillNotDone= false;
                currentPage=menuPages.opdaterSpilKopiForm;
                Lagersystem.LagerKopi[index].Formular();
            }
        }
    }
}
