namespace DatVeMayBay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLap { get; set; }

        public decimal? ThanhTien { get; set; }

        public int? MaVe { get; set; }

        [StringLength(10)]
        public string MaHinhThucTT { get; set; }

        public int? MaTTXuat { get; set; }

        public int? MaKhuyenMai { get; set; }

        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }

        public virtual KhuyenMai KhuyenMai { get; set; }

        public virtual ThongTinXuatHoaDon ThongTinXuatHoaDon { get; set; }

        public virtual Ve Ve { get; set; }
    }
}
