using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models.VM
{
    public class ProizvodVM
    {
        public int IDProizvod { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(25)]
        public string BrojProizvoda { get; set; }

        [StringLength(15)]
        public string Boja { get; set; }

        [Display(Name = "Minimalno")]
        public short MinimalnaKolicinaNaSkladistu { get; set; }

        [Column(TypeName = "money")]
        public decimal CijenaBezPDV { get; set; }

        [Display(Name = "Potkategorija")]
        public int? PotkategorijaID { get; set; }

        public virtual Potkategorija Potkategorija { get; set; }

        public IEnumerable<Potkategorija> SvePotkategorije { get; set; }

    }
}