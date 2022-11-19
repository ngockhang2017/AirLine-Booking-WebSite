namespace VietMaxWatches.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Suport")]
    public partial class Suport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        public int? Type { get; set; }

        [StringLength(50)]
        public string Nick { get; set; }

        public int? TrangThai { get; set; }
    }
}
