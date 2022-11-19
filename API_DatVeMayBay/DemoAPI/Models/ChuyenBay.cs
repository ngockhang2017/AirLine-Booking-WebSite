namespace DemoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenBay")]
    public partial class ChuyenBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenBay()
        {
            PhieuDatVes = new HashSet<PhieuDatVe>();
        }

        [Key]
        public int MaChuyenBay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCatCanh { get; set; }

        public TimeSpan? GioCatCanh { get; set; }

        public TimeSpan? ThoiGianBay { get; set; }

        [StringLength(10)]
        public string LoaiMayBay { get; set; }

        public int MaChangBay { get; set; }

        public decimal? GiaNguoiLon { get; set; }

        public decimal? GiaTreEm { get; set; }

        public virtual ChangBay ChangBay { get; set; }

        public virtual MayBay MayBay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDatVe> PhieuDatVes { get; set; }
    }
}
