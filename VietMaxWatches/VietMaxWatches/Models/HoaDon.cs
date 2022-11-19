namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietDatHang = new HashSet<ChiTietDatHang>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public string TenKhachHang { get; set; }

        public string DiaChiGiaoHang { get; set; }

        public DateTime? ThoiGianGiaoHang { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        public decimal? TongTien { get; set; }

        public int? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHang { get; set; }
    }
}
