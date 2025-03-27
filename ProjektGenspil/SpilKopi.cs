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
        double pris;
        string stand;
        Kunder reservation;

        public SpilKopi() { }
        public SpilKopi(string navn, double nyPris, string alder, string antalSpillere, string[] genre, double pris, string stand)
            : base(navn, nyPris, alder, antalSpillere, genre)
        {
            this.pris = pris;
            this.stand = stand;
        }
    }
}
