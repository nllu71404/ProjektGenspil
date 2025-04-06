using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class InputOutput
    {        
        private string StreamFilePath { get; set; }
        public static string savesFolder = Directory.GetCurrentDirectory() + @"\saves";
        public static string filePath = savesFolder + @"\GenspilLager.txt";

        public InputOutput (string streamFilePath)
        {
            this.StreamFilePath = StreamFilePath+streamFilePath;
        }

        public static void Initialize()
        {
            Spil.LoadGames();
            Kunder.LoadKunder();
        }

        public void SaveFile (List <string> listStrings)
        {
            using (StreamWriter sw = new StreamWriter(StreamFilePath))
            {
                foreach (var line in listStrings)
                {
                    sw.WriteLine(line);
                }
            }
        }

        public List<string> LoadFile()
        {
            FileStream fileStream = new FileStream (StreamFilePath,FileMode.OpenOrCreate);
            List <string> listStrings = new List<string>();
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string line;
                while ((line = sr.ReadLine())!=null)
                {
                    if(!string.IsNullOrEmpty(line))
                    {
                        listStrings.Add(line);
                    }
                }
            }
            return listStrings;
        }
    }
}
