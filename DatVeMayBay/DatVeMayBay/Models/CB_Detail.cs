using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DatVeMayBay.Models
{
    public class CB_Detail
    {
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
        [StringLength(10)]
        public string SanBay_CatCanh { get; set; }

        [StringLength(10)]
        public string SanBay_HaCanh { get; set; }
        [StringLength(50)]
        public string TenSanBay { get; set; }

        [StringLength(100)]
        public string ViTri { get; set; }

        [StringLength(50)]
        public string KhuVuc { get; set; }

        public int? NoiDia { get; set; }

        public int? QuocTe { get; set; }
    }
}