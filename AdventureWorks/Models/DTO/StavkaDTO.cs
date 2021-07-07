using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models.DTO
{
    public class StavkaDTO
    {
        public int Stavka { get; set; }
        public string Proizvod { get; set; }
        public string Potkategorija { get; set; }
        public string Kategorija { get; set; }
        public string KreditnaKartica { get; set; }
        public string Komercijalist { get; set; }
    }
}