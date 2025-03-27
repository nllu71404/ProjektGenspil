using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class MyInterface
    {
        //colors interface
        private readonly static string greenCursor = "=> \u001b[32m";
        private readonly static string green = "\u001b[32m";
        private readonly static string nonColor = "\u001b[0m";
        private readonly static string bGYellow = "\u001b[43m";
        private readonly static string selected = "\u001b[36;1m";

        public static string GreenCursor {  get { return greenCursor; } }
        public static string Green {  get { return greenCursor; } }
        public static string NonColor { get { return nonColor; } }
        public static string BGYellow { get { return bGYellow; } }
        public static string Selected { get { return selected; } }



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
                    if (choice == spilList.IndexOf(spil)) Console.Write(MyInterface.Green);
                    spil.PrintSpilOneLine();
                    Console.Write(MyInterface.NonColor);
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
}
