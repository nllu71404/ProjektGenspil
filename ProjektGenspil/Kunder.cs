using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektGenspil
{
    internal class Kunde
    {
        private string navn;
        private string mobilNummer;
        private string emailAdresse;
        private string spilForespørgsel;

        //Properties 
        public string Navn { get { return navn; } }
        public string MobilNummer { get { return mobilNummer; } }
        public string EmailAdresse { get { return emailAdresse; } }
        public string SpilForespørgsel { get { return spilForespørgsel;} }


        public Kunde(string navn, string mobilNummer, string emailAdresse, string spilForespørgsel) 
        {
            this.navn = navn;
            this.mobilNummer = mobilNummer;
            this.emailAdresse = emailAdresse;
            this.spilForespørgsel = spilForespørgsel;
        }

        public void EmailOmSpil(string overskrift, string tekst, SpilKopi reserveretSpil)
        {

        }
    }    

}
