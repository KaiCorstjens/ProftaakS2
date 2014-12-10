using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Tram
    {
        public int Id { get; private set; }
        public Tramtype tramtype { get; private set; }
        public int nummer { get; private set; }
        public int lengte { get; private set; }
        public string status { get; private set; }
        public Remise remise { get; private set; }
        public bool vervuild { get; private set; }
        public bool defect { get; private set; }
        public bool conducteurGeschikt { get; private set; }
        public bool beschikbaar { get; private set; }

        public Tram(int id, Tramtype tramtype, int nummer, int lengte, string status, Remise remise, bool vervuild, bool defect, bool conducteurGeschikt, bool beschikbaar)
        {
            this.Id = id;
            this.tramtype = tramtype;
            this.nummer = nummer;
            this.lengte = lengte;
            this.status = status;
            this.remise = remise;
            this.vervuild = vervuild;
            this.defect = defect;
            this.conducteurGeschikt = conducteurGeschikt;
            this.beschikbaar = beschikbaar;
        }

        public void Onderhoud(TypeOnderhoud typeOnderhoud, string opmerking, DateTime beschikbaar, Medewerker medewerker)
        {
            if (typeOnderhoud == TypeOnderhoud.GroteSchoonmaak || typeOnderhoud == TypeOnderhoud.KleineSchoonmaak)
            {
                this.IsNietVervuild();
            }
            else if (typeOnderhoud == TypeOnderhoud.GroteReparatie || typeOnderhoud == TypeOnderhoud.KleineReparatie)
            {
                this.IsNietDefect();
            }
            Tramonderhoud onderhoud = new Tramonderhoud(medewerker, this, beschikbaar, DateTime.Now, typeOnderhoud, opmerking);
            TramManager.voegOnderhoudToe(onderhoud);
            DatabaseManager.registreerOnderhoud(onderhoud);
        }

        public void IsVervuild()
        {
            this.vervuild = true;
        }

        public void IsDefect()
        {
            this.defect = true;
        }

        public void IsNietVervuild()
        {
            this.vervuild = false;
        }

        public void IsNietDefect()
        {
            this.defect = false;
        }

        public void VeranderTramstatus(string statuss)
        {
            this.status = statuss;
        }

        public override string ToString()
        {
            return Convert.ToString(this.nummer) + " "
                   + this.tramtype + " "
                   + this.status + " "
                   + this.remise.ToString() + " ";
        }
    }
}
