namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDatHang = new HashSet<ChiTietDatHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSanPham { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [StringLength(250)]
        public string Anh { get; set; }

        public string Anh_Them1 { get; set; }

        public string Anh_Them2 { get; set; }

        public string Anh_Them3 { get; set; }

        public decimal? Gia { get; set; }

        public decimal? GiaUuDai { get; set; }

        public string ThongTinChiTiet { get; set; }

        public string ThongTinThem { get; set; }

        [StringLength(250)]
        public string TuKhoa { get; set; }

        [StringLength(10)]
        public string MaThuongHieu { get; set; }

        public int? MaDanhMuc { get; set; }

        public int? SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHang { get; set; }

        public virtual DanhMucSanPham DanhMucSanPham { get; set; }

        public virtual ThuongHieuSanPham ThuongHieuSanPham { get; set; }
    }
}
