namespace DatVeMayBay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HanhLy")]
    public partial class HanhLy
    {
        [Key]
        [StringLength(10)]
        public string MaHanhLy { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        public double? KhoiLuong { get; set; }

        public decimal? GiaTien { get; set; }
    }
}
