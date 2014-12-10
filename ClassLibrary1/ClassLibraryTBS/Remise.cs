using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
   public class Remise
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public int GroteServicebeurtenPerDag { get; private set; }
        public int KleineServicebeurtenPerDag { get; private set; }
        public int GroteSchoonmaakbeurtenPerDag { get; private set; }
        public int KleineSchoonmaakbeurtenPerDag { get; private set; }

        public Remise(int id, string naam, int groteServicebeurtenPerDag, int kleineServicebeurtenPerDag, int kleineSchoonmaakbeurtenPerDag, int groteSchoonmaakbeurtenPerDag)
        {
            Id = id;
            Naam = naam;
            GroteServicebeurtenPerDag = groteServicebeurtenPerDag;
            KleineServicebeurtenPerDag = kleineServicebeurtenPerDag;
            KleineSchoonmaakbeurtenPerDag = kleineSchoonmaakbeurtenPerDag;
            GroteSchoonmaakbeurtenPerDag = groteSchoonmaakbeurtenPerDag;
        }

       public override string ToString()
       {
           return this.Naam + " ";
       }
    }
}
