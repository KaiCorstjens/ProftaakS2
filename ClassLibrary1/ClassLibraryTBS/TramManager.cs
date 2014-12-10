using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class TramManager
    {
        public static List<Tram> Trams;
        public static List<Tramonderhoud> onderhoudsBeurten; 

        static TramManager()
        {
            Trams = new List<Tram>();
            onderhoudsBeurten = new List<Tramonderhoud>();
        }

        public static void LaadTrams()
        {
            Trams = DatabaseManager.LaadTrams();
            onderhoudsBeurten = DatabaseManager.LaadTramonderhoud();
        }

        public static Tram tramViaId(int id)
        {
            foreach (Tram tram in Trams)
            {
                if (tram.Id == id) return tram;
            }
            return null;
        }

        public static Tram tramViaNummer(int nummer)
        {
            foreach (Tram tram in Trams)
            {
                if (tram.nummer == nummer) return tram;
            }
            return null;
        }

        public static Tramonderhoud onderhoudViaId(int id)
        {
            foreach (Tramonderhoud onderhoud in onderhoudsBeurten)
            {
                if (onderhoud.Id == id) return onderhoud;
            }
            return null;
        }

        public static void voegOnderhoudToe(Tramonderhoud onderhoud)
        {
            onderhoudsBeurten.Add(onderhoud);
        }

        public static Tramonderhoud OnderhoudFromString(string onderhoudString)
        {
            int index = onderhoudString.IndexOf(":");
            string onderhoudNr = "";

            if (index > 0)
            {
                onderhoudNr = onderhoudString.Substring(0, index);
            }

            return TramManager.onderhoudViaId(Convert.ToInt32(onderhoudNr));
        }
    }
}
