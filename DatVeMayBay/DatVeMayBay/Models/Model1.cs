using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DatVeMayBay.Dao;

namespace DatVeMayBay.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DataConnect")
        {
        }

        public virtual List<Admin> Admins { get { return APIHelper.SendGetRequest<List<Admin>>("Admins?number"); } }
        public virtual List<ChangBay> ChangBays { get { return APIHelper.SendGetRequest<List<ChangBay>>("ChangBays?number"); } }
        public virtual List<ChuyenBay> ChuyenBays { get { return APIHelper.SendGetRequest<List<ChuyenBay>>("ChuyenBays?number"); } }
        public virtual List<HanhLy> HanhLies { get { return APIHelper.SendGetRequest<List<HanhLy>>("HanhLies?number"); } }
        public virtual List<HinhThucThanhToan> HinhThucThanhToans { get { return APIHelper.SendGetRequest<List<HinhThucThanhToan>>("HinhThucThanhToans?number"); } }
        public virtual List<HoaDon> HoaDons { get { return APIHelper.SendGetRequest<List<HoaDon>>("HoaDons?number"); } }
        public virtual List<KhachHang> KhachHangs { get { return APIHelper.SendGetRequest<List<KhachHang>>("KhachHangs?number"); } }
        public virtual List<KhuyenMai> KhuyenMais { get { return APIHelper.SendGetRequest<List<KhuyenMai>>("KhuyenMais?number"); } }
        public virtual List<LoaiVe> LoaiVes { get { return APIHelper.SendGetRequest<List<LoaiVe>>("LoaiVes?number"); } }
        public virtual List<MayBay> MayBays { get { return APIHelper.SendGetRequest<List<MayBay>>("MayBays?number"); } }
        public virtual List<NganHang> NganHangs { get { return APIHelper.SendGetRequest<List<NganHang>>("NganHangs?number"); } }
        public virtual List<PhieuDatVe> PhieuDatVes { get { return APIHelper.SendGetRequest<List<PhieuDatVe>>("PhieuDatVes?number"); } }
        public virtual List<SanBay> SanBays { get { return APIHelper.SendGetRequest<List<SanBay>>("SanBays?number"); } }
        public virtual List<ThongTinXuatHoaDon> ThongTinXuatHoaDons { get { return APIHelper.SendGetRequest<List<ThongTinXuatHoaDon>>("ThongTinXuatHoaDons?number"); } }
        public virtual List<Ve> Ves { get { return APIHelper.SendGetRequest<List<Ve>>("Ves?number"); } }
        public virtual List<KhachHang_DatVe> KhachHang_DatVes { get { return APIHelper.SendGetRequest<List<KhachHang_DatVe>>("KhachHang_DatVes?number"); } }

    }
}
