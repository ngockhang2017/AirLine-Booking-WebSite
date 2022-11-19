namespace DemoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HanhLy")]
    public partial class HanhLy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HanhLy()
        {
            PhieuDatVe_HanhLy = new HashSet<PhieuDatVe_HanhLy>();
        }

        [Key]
        [StringLength(10)]
        public string MaHanhLy { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        public double? KhoiLuong { get; set; }

        public decimal? GiaTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDatVe_HanhLy> PhieuDatVe_HanhLy { get; set; }
    }
}
