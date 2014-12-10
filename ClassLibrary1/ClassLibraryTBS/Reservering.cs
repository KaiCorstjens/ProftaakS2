using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Reservering
    {
        public int Id { get; private set; }
        public Tram Tram { get; private set; }
        public Spoor Spoor { get; private set; }

        public Reservering(int id, Tram tram, Spoor spoor)
        {
            Id = id;
            Tram = tram;
            Spoor = spoor;
        }
    }
}
