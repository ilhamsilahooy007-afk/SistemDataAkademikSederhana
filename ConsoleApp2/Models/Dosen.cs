using System;
using System.Collections.Generic;
using System.Text;

namespace SistemDataAkademik.Models
{
    public class Dosen
    {
        public string Nidn { get; set; } = "";
        public string NamaLengkap { get; set; } = "";
        public bool JenisKelamin { get; set; } 
        public DateTime TanggalLahir { get; set; } 
        public string KodeProdi { get; set; } = "";
    }
}
