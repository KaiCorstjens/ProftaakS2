using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Tramonderhoud
    {
        public int Id { get; private set; }
        public Medewerker Medewerker { get; private set; }
        public Tram Tram { get; private set; }
        public DateTime BeschikbaarDatum { get; private set; }
        public DateTime DatumTijdstip { get; private set; }
        public TypeOnderhoud TypeOnderhoud { get; private set; }
        public string Opmerking { get; private set; }


        public Tramonderhoud(int id, Medewerker medewerker, Tram tram, DateTime beschikbaarDatum, DateTime datumTijdstip, TypeOnderhoud typeOnderhoud, string opmerking)
        {
            Id = id;
            Medewerker = medewerker;
            Tram = tram;
            BeschikbaarDatum = beschikbaarDatum;
            DatumTijdstip = datumTijdstip;
            TypeOnderhoud = typeOnderhoud;
            Opmerking = opmerking;
        }

        public Tramonderhoud(Medewerker medewerker, Tram tram, DateTime beschikbaarDatum, DateTime datumTijdstip, TypeOnderhoud typeOnderhoud, string opmerking)
        {
            Medewerker = medewerker;
            Tram = tram;
            BeschikbaarDatum = beschikbaarDatum;
            DatumTijdstip = datumTijdstip;
            TypeOnderhoud = typeOnderhoud;
            Opmerking = opmerking;
        }

        public void OnderhoudKlaar()
        {
            //ToDo
        }

        public override string ToString()
        {
            string onderhoud = Convert.ToString(Id) + ":" +" Naam= "+ Medewerker.Naam +", Tram= " + Convert.ToString(Tram.nummer) +", Onderhoud= "+Convert.ToString(TypeOnderhoud);
            return onderhoud;
        }        
    }
}
