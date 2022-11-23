namespace DatVeMayBay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang_DatVe")]
    public partial class KhachHang_DatVe
    {
        [Key]
        public int MaPhieuDatVe { get; set; }

        
        public int MaKH { get; set; }

       
        public int NguoiDat { get; set; }

        
    }
}
