using System;
using System.Collections.Generic;
using System.Text;

namespace SistemDataAkademik.Models
{
    public class MataKuliah
    {
        public string KodeMataKuliah { get; set; } = "";
        public string NamaMataKuliah { get; set; } = "";
        public double Sks { get; set; }
        public string KodeProdi { get; set; } = "";
    }

}
