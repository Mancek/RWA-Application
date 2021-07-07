using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models.VM
{
    public class PotkategorijaVM
    {
        public int IDPotkategorija { get; set; }

        [Display(Name = "Kategorija")]
        public int KategorijaID { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public virtual Kategorija Kategorija { get; set; }

        public IEnumerable<Kategorija> SveKategorije { get; set; }

    }
}