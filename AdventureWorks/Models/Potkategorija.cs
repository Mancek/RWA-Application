namespace AdventureWorks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Potkategorija")]
    public partial class Potkategorija
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Potkategorija()
        {
            Proizvod = new HashSet<Proizvod>();
        }

        [Key]
        public int IDPotkategorija { get; set; }

        [Display(Name = "Kategorija")]
        public int KategorijaID { get; set; }

        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }

        public virtual Kategorija Kategorija { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proizvod> Proizvod { get; set; }
    }
}
