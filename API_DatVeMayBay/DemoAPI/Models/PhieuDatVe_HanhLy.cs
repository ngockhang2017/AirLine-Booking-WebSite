namespace DemoAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhieuDatVe_HanhLy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuDatVe { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaHanhLy { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoLuong { get; set; }

        public virtual HanhLy HanhLy { get; set; }

        public virtual PhieuDatVe PhieuDatVe { get; set; }
    }
}
