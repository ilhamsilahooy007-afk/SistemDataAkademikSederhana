using System;
using System.Collections.Generic;
using System.Text;

namespace SistemDataAkademik.Models
{
    public class Mahasiswa
    {
        public string Nim { get; set; } = "";
        public string NamaLengkap { get; set; } = "";
        public bool JenisKelamin { get; set; }
        public string KodeProdi { get; set; } = "";
        public string NidnPembimbingAkademik { get; set; } = "";
    }

}