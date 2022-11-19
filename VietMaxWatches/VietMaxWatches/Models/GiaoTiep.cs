namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaoTiep")]
    public partial class GiaoTiep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaGiaoTiep { get; set; }

        [StringLength(500)]
        public string HoTen { get; set; }

        [StringLength(500)]
        public string CongTy { get; set; }

        [Column(TypeName = "ntext")]
        public string DiaChi { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(300)]
        public string Mal { get; set; }

        [Column(TypeName = "ntext")]
        public string ChiTiet { get; set; }

        public DateTime? Date { get; set; }
    }
}
