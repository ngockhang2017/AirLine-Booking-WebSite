using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DemoAPI.Models
{
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities1")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChangBay> ChangBays { get; set; }
        public virtual DbSet<ChuyenBay> ChuyenBays { get; set; }
        public virtual DbSet<HanhLy> HanhLies { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LoaiVe> LoaiVes { get; set; }
        public virtual DbSet<MayBay> MayBays { get; set; }
        public virtual DbSet<NganHang> NganHangs { get; set; }
        public virtual DbSet<PhieuDatVe> PhieuDatVes { get; set; }
        public virtual DbSet<SanBay> SanBays { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<ThongTinXuatHoaDon> ThongTinXuatHoaDons { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }
        public virtual DbSet<KhachHang_DatVe> KhachHang_DatVe { get; set; }
        public virtual DbSet<PhieuDatVe_HanhLy> PhieuDatVe_HanhLy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangBay>()
                .Property(e => e.SanBay_CatCanh)
                .IsUnicode(false);

            modelBuilder.Entity<ChangBay>()
                .Property(e => e.SanBay_HaCanh)
                .IsUnicode(false);

            modelBuilder.Entity<ChangBay>()
                .HasMany(e => e.ChuyenBays)
                .WithRequired(e => e.ChangBay)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuyenBay>()
                .Property(e => e.ThoiGianBay)
                .HasPrecision(5);

            modelBuilder.Entity<ChuyenBay>()
                .Property(e => e.LoaiMayBay)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenBay>()
                .Property(e => e.GiaNguoiLon)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChuyenBay>()
                .Property(e => e.GiaTreEm)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HanhLy>()
                .Property(e => e.MaHanhLy)
                .IsUnicode(false);

            modelBuilder.Entity<HanhLy>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HanhLy>()
                .HasMany(e => e.PhieuDatVe_HanhLy)
                .WithRequired(e => e.HanhLy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .Property(e => e.MaHinhThucTT)
                .IsUnicode(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .HasMany(e => e.NganHangs)
                .WithMany(e => e.HinhThucThanhToans)
                .Map(m => m.ToTable("ThanhToan_NganHang").MapLeftKey("MaHinhThucTT").MapRightKey("MaNganHang"));

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHinhThucTT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.CMND)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.KhachHang_DatVe)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiVe>()
                .Property(e => e.MaLoaiVe)
                .IsUnicode(false);

            modelBuilder.Entity<MayBay>()
                .Property(e => e.LoaiMayBay)
                .IsUnicode(false);

            modelBuilder.Entity<MayBay>()
                .Property(e => e.KyHieuHang)
                .IsUnicode(false);

            modelBuilder.Entity<NganHang>()
                .Property(e => e.MaNganHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDatVe>()
                .Property(e => e.MaLoaiVe)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDatVe>()
                .HasMany(e => e.KhachHang_DatVe)
                .WithRequired(e => e.PhieuDatVe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuDatVe>()
                .HasMany(e => e.PhieuDatVe_HanhLy)
                .WithRequired(e => e.PhieuDatVe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanBay>()
                .Property(e => e.MaSanBay)
                .IsUnicode(false);

            modelBuilder.Entity<SanBay>()
                .HasMany(e => e.ChangBays)
                .WithOptional(e => e.SanBay)
                .HasForeignKey(e => e.SanBay_CatCanh);

            modelBuilder.Entity<SanBay>()
                .HasMany(e => e.ChangBays1)
                .WithOptional(e => e.SanBay1)
                .HasForeignKey(e => e.SanBay_HaCanh);

            modelBuilder.Entity<Ve>()
                .Property(e => e.KhoangGhe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDatVe_HanhLy>()
                .Property(e => e.MaHanhLy)
                .IsUnicode(false);
        }
    }
}
