namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [StringLength(500)]
        public string UserName { get; set; }

        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(500)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? TrangThai { get; set; }
    }
}
