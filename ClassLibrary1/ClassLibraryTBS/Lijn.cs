using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class Lijn
    {
        public int Id { get; set; }
        public Remise Remise { get; set; }
        public int Nummer { get; set; }
        public bool ConducteurRijdtMee { get; set; }

        public Lijn(int id, Remise remise, int nummer, bool conducteurRijdtMee)
        {
            Id = id;
            Remise = remise;
            Nummer = nummer;
            ConducteurRijdtMee = conducteurRijdtMee;
        }
    }
}
