using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class TramIndeling
    {
        private List<Spoor> alleSporen = RemiseManager.Sporen;
        private bool sporenOp = false;
        int spoorTeller = 0;
        private bool increaseTeller = false;

        /// <summary>
        /// Functie met algoritme waarmee de tram ingedeeld wordt op een spoor(/op sectoren)
        /// </summary>
        /// <param name="tram">tram die ingedeeld moet worden</param>
        /// <returns>Lijst met sectoren waarop de tram is ingedeeld</returns>
        public List<Sector> DeelTramIn(Tram tram)
        {
           /* if (tram.nummer == 834)
            {
                //breakpoint
            } */
            VerwijderSchoonmaakReparatieSporen();
            List<Sector> ingedeeldeSectors = null;
            bool sectorFound = false;
            while (!sectorFound)
            {
                if (sporenOp) return null; // anders ingedeeldesectors = null en sectorFound = true;
                Spoor ingedeeldSpoor = krijgEerstVolgendeSpoor();
                if (ingedeeldSpoor != null)
                {
                    if (isSpoorBeschikbaar(ingedeeldSpoor))
                    {
                        if (isSpoorLangGenoeg(ingedeeldSpoor, tram.lengte))
                        {
                            ingedeeldeSectors = vrijeSectoren(ingedeeldSpoor, tram,1,true);
                            if (ingedeeldeSectors.Count() < tram.lengte)
                            {
                                ingedeeldeSectors = null;
                            }
                            if (ingedeeldeSectors != null && ingedeeldeSectors.Any())
                            {
                                sectorFound = true;
                                voegTramAanSectorsToe(ingedeeldeSectors,tram,false);
                            }
                        
                        }
                        else
                        {
                            spoorTeller++;
                            increaseTeller = true;
                        }
                    }
                }
            }
            if (increaseTeller) spoorTeller = 0;
            return ingedeeldeSectors;
        }

        private void VerwijderSchoonmaakReparatieSporen()
        {
            List<Spoor> verwijderlijst = new List<Spoor>();
            foreach(Spoor s in RemiseManager.Sporen)
            {
                if (s.Nummer < 30)
                {
                    verwijderlijst.Add(s);
                }
            }
            foreach (Spoor s in verwijderlijst)
            {
                alleSporen.Remove(s);
            }
        }
        public string DeelTramInOpSector(Tram tram,Sector sector)
        {
            List<Sector> vrijeSpoorSectors = null;
                List<Sector> ingedeeldeSectors = new List<Sector>();

            Sector beginSector = sector;
            Spoor spoorvanSector = RemiseManager.spoorViaId(sector.SpoorNummer);
            if (beginSector != null)
            {
                if (!beginSector.Blokkade)
                {
                    if (beginSector.Beschikbaar)
                    {
                        if (spoorvanSector.SectorList.Count() - beginSector.Nummer >= tram.lengte)
                        {
                            //Ok up to here
                            vrijeSpoorSectors = vrijeSectoren(spoorvanSector, tram,beginSector.Nummer,true);
                            foreach (Sector s in vrijeSpoorSectors)
                            {
                                if (s.Nummer >= sector.Nummer)
                                {
                                    ingedeeldeSectors.Add(s);
                                }
                            }
                            foreach (Sector s in ingedeeldeSectors)
                            {
                                if (s.Nummer < 30)
                                {
                                    ingedeeldeSectors = null;
                                    return "Sector is schoonmaak of reparatie-sector.";
                                }
                            }
                            if (ingedeeldeSectors.Count() < tram.lengte)
                            {
                                ingedeeldeSectors = null;
                                return "niet genoeg vrije sectoren";
                            }
                            if (ingedeeldeSectors != null && ingedeeldeSectors.Any())
                            {
                                voegTramAanSectorsToe(ingedeeldeSectors, tram, true);
                                return "Tram toegevoegd.";
                            }
                        }
                        else
                        {
                            return "niet genoeg sectoren op spoor.";
                        }
                    }
                }
                else
                {
                    return "Sector geblokkeerd";
                }
            }
            return "Sector niet gevonden";
        }
        /// <summary>
        /// Functie om het (eerst)volgende spoor te krijgen.
        /// </summary>
        /// <returns>(eerst)volgende spoor</returns>
        private Spoor krijgEerstVolgendeSpoor()
        {
            Spoor[] sporenArray = alleSporen.ToArray();
            bool sectorsleft = false;
            if (spoorTeller >= sporenArray.Count())
            {
                Console.WriteLine("Sporen vol");
                sporenOp = true;
                return null;
            }

            foreach (Sector s in sporenArray[spoorTeller].SectorList)
            {
                if (s.Tram == null&&!s.Blokkade)
                {
                    sectorsleft = true;
                }
            }
            if (!sectorsleft)// && !increaseTeller)
            {
                spoorTeller++;
            }
            if (spoorTeller < sporenArray.Count())
            {
                return sporenArray[spoorTeller];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Controleert of het spoor beschikbaar is
        /// </summary>
        /// <param name="spoor">spoor dat gecontroleerd moet worden</param>
        /// <returns>true als het spoor beschikbaar is</returns>
        private bool isSpoorBeschikbaar(Spoor spoor)
        {
            return (spoor.Beschikbaar);
        }
        /// <summary>
        /// Kijkt of het spoor lang genoeg is voor de tram
        /// </summary>
        /// <param name="spoor">spoor dat gecontroleerd moet worden</param>
        /// <param name="lengte">lengte van de tram</param>
        /// <returns>true als het spoor lang genoeg is</returns>
        private bool isSpoorLangGenoeg(Spoor spoor,int lengte)
        {

            Console.WriteLine("lengte: " + lengte + " SpoorLengte: " + spoor.SectorList.Count());
            return (lengte <= spoor.SectorList.Count);
        }
        /// <summary>
        /// Zoekt vrije sectors waar de tram (qua lengte) op kan staan
        /// </summary>
        /// <param name="spoor">spoor waarin sectoren gezocht moeten worden</param>
        /// <param name="tram">tram die geplaatst moet worden</param>
        /// <returns></returns>
        private List<Sector> vrijeSectoren(Spoor spoor, Tram tram, int beginsectornummer, bool reverse)
        {
            List<Sector> spoorSectors = RemiseManager.sectorenVanSpoor(spoor.Id);
            List<Sector> sectors = new List<Sector>();
            if (reverse) spoorSectors.Reverse(); // Reverse list, zodat de tram eerst op de achterste sectoren v/h spoor komt te staan
            foreach (Sector s in spoorSectors)
            {
                if (s.Blokkade)
                {
                    sectors.Clear();
                }
                if (sectors.Count < tram.lengte)
                {
                    if (s.Beschikbaar && !s.Blokkade && s.Tram == null && s.Nummer >=beginsectornummer)
                    {
                        sectors.Add(s);
                    }
                    else
                    {
                        sectors.Clear();
                    }
                }
            }
            
            return sectors;
        }
        /// <summary>
        /// Functie om een tram toe te voegen aan sectoren.
        /// </summary>
        /// <param name="sectorlist">lijst met sectoren waarop de tram komt te staan</param>
        /// <param name="tram">tram die aan de sectoren wordt toegevoegd</param>
        private void voegTramAanSectorsToe(List<Sector> sectorlist, Tram tram, bool opslaan)
        {
            foreach (Sector s in RemiseManager.Sectors)
            {
                foreach (Sector se in sectorlist)
                {
                    if (s.Id == se.Id)
                    {
                        s.VoegTramToe(tram);
                        if (opslaan)
                        {
                                DatabaseManager.registreerSectorStatus(se);
                        }
                    }
                }
            }
        }
    }
}
