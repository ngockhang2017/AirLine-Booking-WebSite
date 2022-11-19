using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VietMaxWatches.Models
{
     public partial class Entities : DbContext
     {
          public Entities()
              : base("name=Entities")
          {
          }

          public virtual DbSet<ChiTietDatHang> ChiTietDatHang { get; set; }
          public virtual DbSet<DanhMucSanPham> DanhMucSanPham { get; set; }
          public virtual DbSet<GiaoTiep> GiaoTiep { get; set; }
          public virtual DbSet<HoaDon> HoaDon { get; set; }
          public virtual DbSet<NguoiDung> NguoiDung { get; set; }
          public virtual DbSet<NguonTinTuc> NguonTinTuc { get; set; }
          public virtual DbSet<PhanHoi> PhanHoi { get; set; }
          public virtual DbSet<SanPham> SanPham { get; set; }
          public virtual DbSet<Sliders> Sliders { get; set; }
          public virtual DbSet<Suport> Suport { get; set; }
          public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
          public virtual DbSet<ThuongHieuSanPham> ThuongHieuSanPham { get; set; }

          protected override void OnModelCreating(DbModelBuilder modelBuilder)
          {
               modelBuilder.Entity<ChiTietDatHang>()
                   .Property(e => e.TT_ThanhToan)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<GiaoTiep>()
                   .Property(e => e.SDT)
                   .IsUnicode(false);

               modelBuilder.Entity<GiaoTiep>()
                   .Property(e => e.Mal)
                   .IsUnicode(false);

               modelBuilder.Entity<HoaDon>()
                   .Property(e => e.TongTien)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<NguoiDung>()
                   .Property(e => e.UserName)
                   .IsUnicode(false);

               modelBuilder.Entity<NguoiDung>()
                   .Property(e => e.Password)
                   .IsUnicode(false);

               modelBuilder.Entity<SanPham>()
                   .Property(e => e.Gia)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<SanPham>()
                   .Property(e => e.GiaUuDai)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<SanPham>()
                   .Property(e => e.MaThuongHieu)
                   .IsFixedLength();

               modelBuilder.Entity<SanPham>()
                   .HasMany(e => e.ChiTietDatHang)
                   .WithRequired(e => e.SanPham)
                   .WillCascadeOnDelete(false);

               modelBuilder.Entity<Sliders>()
                   .Property(e => e.CreatedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Sliders>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Suport>()
                   .Property(e => e.Tel)
                   .IsUnicode(false);

               modelBuilder.Entity<Suport>()
                   .Property(e => e.Nick)
                   .IsUnicode(false);

               modelBuilder.Entity<ThuongHieuSanPham>()
                   .Property(e => e.MaThuongHieu)
                   .IsFixedLength();
          }
     }
}
