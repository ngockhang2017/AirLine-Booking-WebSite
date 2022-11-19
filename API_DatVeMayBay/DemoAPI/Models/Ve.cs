namespace DemoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ve()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int MaVe { get; set; }

        public int? SoGhe { get; set; }

        [StringLength(10)]
        public string KhoangGhe { get; set; }

        public int? MaPhieuDatVe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual PhieuDatVe PhieuDatVe { get; set; }
    }
}
