using System.Reflection.Metadata;

namespace ProjektGenspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Spil spil = new Spil();
            spil.OpdaterSpil();
            Lagersystem.Lager.Add(spil);

            //søg pris, antal spillere, genre
        }
    }
}
