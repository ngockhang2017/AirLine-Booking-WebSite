namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguonTinTuc")]
    public partial class NguonTinTuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNguonTin { get; set; }

        [StringLength(250)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string NguoiTao { get; set; }

        public string Anh { get; set; }

        public string NgayTao { get; set; }
    }
}
