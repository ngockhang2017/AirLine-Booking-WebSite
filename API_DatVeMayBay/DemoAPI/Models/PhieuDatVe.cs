namespace DemoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuDatVe")]
    public partial class PhieuDatVe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDatVe()
        {
            KhachHang_DatVe = new HashSet<KhachHang_DatVe>();
            PhieuDatVe_HanhLy = new HashSet<PhieuDatVe_HanhLy>();
            Ves = new HashSet<Ve>();
        }

        [Key]
        public int MaPhieuDatVe { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        public int? MaChuyenBay { get; set; }

        [StringLength(10)]
        public string MaLoaiVe { get; set; }

        public virtual ChuyenBay ChuyenBay { get; set; }

        public virtual LoaiVe LoaiVe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang_DatVe> KhachHang_DatVe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDatVe_HanhLy> PhieuDatVe_HanhLy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
