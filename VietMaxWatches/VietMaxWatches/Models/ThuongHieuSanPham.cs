namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuongHieuSanPham")]
    public partial class ThuongHieuSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuongHieuSanPham()
        {
            SanPham = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(10)]
        public string MaThuongHieu { get; set; }

        [StringLength(50)]
        public string TenThuongHieu { get; set; }

        public string MoTa { get; set; }

        public string Logo { get; set; }

        public string Image { get; set; }

        public string Theme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
