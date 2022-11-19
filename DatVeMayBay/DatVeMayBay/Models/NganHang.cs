namespace DatVeMayBay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NganHang")]
    public partial class NganHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NganHang()
        {
            HinhThucThanhToans = new HashSet<HinhThucThanhToan>();
        }

        [Key]
        [StringLength(10)]
        public string MaNganHang { get; set; }

        [StringLength(50)]
        public string TenNganHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhThucThanhToan> HinhThucThanhToans { get; set; }
    }
}
