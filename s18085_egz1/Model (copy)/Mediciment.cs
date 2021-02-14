using System;
using System.Collections.Generic;
using s18085_egz1.Controllers;

namespace s18085_egz1.Model
{
    public class Mediciment
    {
        public int IdMediciment { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public string Type { get; set; }
        public List<Prescription> Prescriptions { get; set;  }

        public Mediciment()
        {
            Prescriptions = new List<Prescription>();
        }
    }
}
