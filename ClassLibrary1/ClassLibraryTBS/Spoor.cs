using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Spoor
    {
        public int Id { get; private set; }
        public Remise Remise { get; private set; }

        public List<Sector> SectorList; 
        public int Nummer { get; private set; }
        public int Lengte { get; private set; }
        public bool Beschikbaar { get; private set; }
        public bool InUitRijSpoor { get; private set; }

        public Spoor(int id, Remise remise, int nummer, int lengte, bool beschikbaar, bool inUitRijSpoor, List<Sector> sectorList )
        {
            this.Id = id;
            this.Remise = remise;
            this.Nummer = nummer;
            this.Lengte = lengte;
            this.Beschikbaar = beschikbaar;
            this.InUitRijSpoor = inUitRijSpoor;
            this.SectorList = sectorList;
        }

        public void Blokkeer()
        {
            foreach (Sector sector in SectorList)
            {
                sector.Blokkeer();
            }
        }

        public void Deblokkeer()
        {
            foreach (Sector sector in SectorList)
            {
                sector.Deblokkeer();
            }
        }
    }
}
