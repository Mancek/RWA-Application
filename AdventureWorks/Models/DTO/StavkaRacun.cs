using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models.DTO
{
    public class StavkaRacun
    {
        public int IDStavka { get; set; }
        public Proizvod Proizvod { get; set; }
        public Potkategorija Potkategorija { get; set; }
        public Kategorija Kategorija { get; set; }
        public KreditnaKartica KreditnaKartica { get; set; }
        public Komercijalist Komercijalist { get; set; }
    }
}