using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Sector
    {
        public int Id { get; private set; }
        public int SpoorNummer { get; private set; }
        public Tram Tram { get; private set; }
        public int Nummer { get; private set; } // Id als spoornummer?
        public bool Beschikbaar { get; private set; }
        public bool Blokkade { get; private set; }

        public Sector(int id, int spoorNummer, Tram tram, int nummer, bool beschikbaar, bool blokkade)
        {
            this.Id = id;
            this.SpoorNummer = spoorNummer;
            this.Nummer = nummer;
            this.Tram = tram;
            this.Beschikbaar = beschikbaar;
            this.Blokkade = blokkade;
        }

        public void Blokkeer()
        {
            this.Blokkade = true;
        }

        public void Deblokkeer()
        {
            this.Blokkade = false;
        }

        public void ClearSector()
        {
            this.Tram = null;
        }

        public void VoegTramToe(Tram tram)
        {
            this.Tram = tram;
        }
    }
}
