using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class InputOutput
    {
        public static string savesFolder = Directory.GetCurrentDirectory() + @"\saves";
        public static string filePath = savesFolder + @"\GenspilLager.txt";

        public static void Initialize()
        {
            if (!Directory.Exists(savesFolder))
            {
                Directory.CreateDirectory(savesFolder);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            LoadSpil();
        }
        public static void LoadSpil()
        {
            List<string> load = File.ReadAllLines(filePath).ToList();
            foreach (string line in load)
            {
                string[] entries = line.Split(',');
                string[] tempGenre = entries[4..(entries.Length - 1)]; //genre starts from index 4 and forth
                Spil tempspil = new Spil(entries[0], entries[1], entries[2], entries[3], tempGenre);
                MyInterface.spilList.Add(tempspil);
            }
        }

        public static void SaveSpil()
        {
            List<string> output = new List<string>();
            foreach (Spil spil in MyInterface.spilList)
            {
                string line = spil.ConvertSpilInfoToSave();
                output.Add(line);
            }
            File.WriteAllLines(filePath, output);
        }
    }
}
