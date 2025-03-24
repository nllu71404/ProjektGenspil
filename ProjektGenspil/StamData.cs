using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class StamData
    {
        //genre og aldergruppe stamdata
        public static string[] alleAlderGrupper = { "Alder Grupper:", "4-5 år", "6-7 år", "8-9 år", "10-12 år", "13-16 år", "16 år+", "18 år+", "8 år", "10 år" };
        public static string[] alleAntalSpillere = { "Antal Spillere:", "2-4 spillere", "3-6 spillere" };
        public static string[] alleGenrer = { "Genre:", "klassiske spil", "selskabsspil", "familiespil", "voksenspil", "strategispil", "børnespil", "quiz", "Små spil", "Escape Room", "Rollespile", "Bingo/banko", "Udvidelse" };
        
        //colors interface
        public static string greenCursor = "=> \u001b[32m";
        public static string green = "\u001b[32m";
        public static string nonColor = "\u001b[0m";
        public static string BGYellow = "\u001b[43m";
        public static string selected = "\u001b[36;1m";
    }
}
