using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace ClassLibrary
{
    public class Medewerker
    {
        public int Id { get; private set; }
        public Functie Functie { get; private set; }
        public string Naam { get; private set; }

        public Medewerker(int id, Functie functie, string naam)
        {
            Id = id;
            Functie = functie; 
            Naam = naam;
        }

        public void DestroySpecimen(string DeathDescription)
        {
            DateTime Now = DateTime.Now;
            //MessageBox.Show("Specimen has been terminated under the following conditions" + DeathDescription + "At" + Now);
        }
    }
}
