using DatVeMayBay.Dao;
using DatVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace DatVeMayBay.Controllers
{
    public class HomeController : Controller
    {

        //static void Main(string[] args)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://192.168.43.151:45455/api/tbladmins");
        //        //HTTP GET
        //        var responseTask = client.GetAsync("tblAdmins");
        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {

        //            var readTask = result.Content.ReadAsAsync<tblAdmin[]>();
        //            readTask.Wait();

        //            var admins = readTask.Result;

        //            foreach (var admin in admins)
        //            {
        //                Console.WriteLine(admin.Phone);
        //                Console.WriteLine(admin.Ten);
        //            }
        //        }
        //    }
        //    Console.ReadLine();
        //}

        private Model1 model = new Model1();

        public ActionResult Index()
        {
            //string url = string.Format("SanBay?{0}", "Miền Bắc");
            //var sanbayMB = APIHelper.SendGetRequest<List<SanBay>>(url);
            //ViewBag.sbMB = sanbayMB;

            return View();
        }

        //[HttpGet]
        //public ActionResult TimKiemChuyenBay()
        //{

        //    return View();

        //}
        [HttpPost]
        public ActionResult TimKiemChuyenBay(string SanBay_CatCanh, string SanBay_HaCanh, string NgayCatCanh)
        {

            SanBay_CatCanh = SanBay_CatCanh.Substring(SanBay_CatCanh.Length - 4, 3);
            SanBay_HaCanh = SanBay_HaCanh.Substring(SanBay_HaCanh.Length - 4, 3);
            //string CatCanh = Request.Form["SanBay_CatCanh"].ToString();
            //string HaCanh = Request.Form["SanBay_HaCanh"].ToString();
            //DateTime NgayCatCanh1 = Convert.ToDateTime(NgayCatCanh);
            //DateTime NgayCatCanh1 = DateTime.ParseExact(NgayCatCanh, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string url = string.Format("ChuyenBays/{0}/{1}/{2}", SanBay_CatCanh, SanBay_HaCanh, NgayCatCanh);
            var chuyenbays = APIHelper.SendGetRequest<List<ChuyenBay>>(url);

            string url1 = string.Format("ChuyenBays/{0}/{1}", SanBay_CatCanh, SanBay_HaCanh);
            var chuyenbaya = APIHelper.SendGetRequest<List<ChuyenBay>>(url1);

            string url2 = string.Format("SanBays/{0}", SanBay_CatCanh);
            var sanbayscc = APIHelper.SendGetRequest<List<SanBay>>(url2);
            ViewBag.SanBcc = sanbayscc;

            string url3 = string.Format("SanBays/{0}", SanBay_HaCanh);
            var sanbayshc = APIHelper.SendGetRequest<List<SanBay>>(url3);
            ViewBag.SanBhc = sanbayshc;

            return View(chuyenbaya);
        }


        [HttpGet]
        public ActionResult ThongTinKhachHang(int id)
        {
            CB_Detail detail = new CB_Detail();
            List<ChuyenBay> cb = model.ChuyenBays.ToList();
            List<ChangBay> chb = model.ChangBays.ToList();
            List<SanBay> sb = model.SanBays.ToList();

            detail = (from c in cb
                      join chab in chb on c.MaChangBay equals chab.MaChangBay
                      where c.MaChuyenBay == id
                      select new CB_Detail
                      {
                          MaChuyenBay = c.MaChuyenBay,
                          NgayCatCanh = c.NgayCatCanh,
                          GioCatCanh = c.GioCatCanh,
                          GiaNguoiLon = c.GiaNguoiLon,
                          ThoiGianBay = c.ThoiGianBay,
                          SanBay_CatCanh = chab.SanBay_CatCanh,
                          SanBay_HaCanh = chab.SanBay_HaCanh


                      }).ToList().FirstOrDefault();

            ViewBag.CC = (from chab in chb
                          join s in sb on chab.SanBay_CatCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_CatCanh = chab.SanBay_CatCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();
            ViewBag.HC = (from chab in chb
                          join s in sb on chab.SanBay_HaCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_HaCanh = chab.SanBay_HaCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();
            return View(detail);
        }


        [HttpPost]
        public ActionResult ThongTinKhachHang()
        {
            return View();
        }


        //public static List<string> tenkh = new List<string>();
        //[HttpGet]
        //public ActionResult KiemTraThongTin(int id)
        //{
        //    CB_Detail detail = new CB_Detail();
        //    List<ChuyenBay> cb = model.ChuyenBays.ToList();
        //    List<ChangBay> chb = model.ChangBays.ToList();
        //    List<SanBay> sb = model.SanBays.ToList();

        //    detail = (from c in cb
        //                      join chab in chb on c.MaChangBay equals chab.MaChangBay
        //                      where c.MaChuyenBay == id
        //                      select new CB_Detail
        //                      {
        //                          MaChuyenBay = c.MaChuyenBay,
        //                          NgayCatCanh = c.NgayCatCanh,
        //                          GioCatCanh = c.GioCatCanh,
        //                          GiaNguoiLon = c.GiaNguoiLon,
        //                          ThoiGianBay = c.ThoiGianBay,
        //                          SanBay_CatCanh = chab.SanBay_CatCanh,
        //                          SanBay_HaCanh = chab.SanBay_HaCanh


        //                      }).ToList().FirstOrDefault();


        //    return View(detail);
        //}
        

        [HttpPost]

        public ActionResult KiemTraThongTin(string TenKH1, string DanhXung1, string TenKH2, string DanhXung2, string DanhXung3, string TenKH3, string SDT, string DiaChi, string ngaysinh, string MaHanhLy, int id)
        {
            CB_Detail detail = new CB_Detail();
            List<ChuyenBay> cb = model.ChuyenBays.ToList();
            List<ChangBay> chb = model.ChangBays.ToList();
            List<SanBay> sb = model.SanBays.ToList();

            detail = (from c in cb
                      join chab in chb on c.MaChangBay equals chab.MaChangBay
                      where c.MaChuyenBay == id
                      select new CB_Detail
                      {
                          MaChuyenBay = c.MaChuyenBay,
                          NgayCatCanh = c.NgayCatCanh,
                          GioCatCanh = c.GioCatCanh,
                          GiaNguoiLon = c.GiaNguoiLon,
                          ThoiGianBay = c.ThoiGianBay,
                          SanBay_CatCanh = chab.SanBay_CatCanh,
                          SanBay_HaCanh = chab.SanBay_HaCanh


                      }).ToList().FirstOrDefault();
            ViewBag.CC = (from chab in chb
                          join s in sb on chab.SanBay_CatCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_CatCanh = chab.SanBay_CatCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();
            ViewBag.HC = (from chab in chb
                          join s in sb on chab.SanBay_HaCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_HaCanh = chab.SanBay_HaCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();


            //-------------------------------------------
            List<KhachHang> khach = new List<KhachHang>();
            KhachHang KH1 = new KhachHang();
            KH1.TenKH = TenKH1;
            KH1.DanhXung = DanhXung1;
            KH1.SDT = SDT;
            KH1.DiaChi = DiaChi;
            khach.Add(KH1);

            KhachHang KH2 = new KhachHang();
            KH2.TenKH = TenKH2;
            KH2.DanhXung = DanhXung2;
            KH2.DiaChi = DiaChi;
            khach.Add(KH2);

            KhachHang KH3 = new KhachHang();
            KH3.DiaChi = DiaChi;
            KH3.TenKH = TenKH3;
            DateTime ngaysinh1 = Convert.ToDateTime(ngaysinh);
            KH3.NgaySinh = ngaysinh1;
            KH3.DanhXung = DanhXung3;
            khach.Add(KH3);

            ViewBag.kh = khach;



            APIHelper.SendPostRequest<KhachHang>("KhachHangs/", KH1);
            APIHelper.SendPostRequest<KhachHang>("KhachHangs/", KH2);
            APIHelper.SendPostRequest<KhachHang>("KhachHangs/", KH3);


            PhieuDatVe phieuDat = new PhieuDatVe();
            phieuDat.NgayDat = DateTime.Now;
            phieuDat.MaChuyenBay = Convert.ToInt32(Request.Form["MaChuyenBay"]);
            phieuDat.MaLoaiVe = "MC";
            APIHelper.SendPostRequest<PhieuDatVe>("PhieuDatVes/", phieuDat);
            return View(detail);
        }


        [HttpGet]
        public ActionResult ThanhToan(int id)
        {
            CB_Detail detail = new CB_Detail();
            List<ChuyenBay> cb = model.ChuyenBays.ToList();
            List<ChangBay> chb = model.ChangBays.ToList();
            List<SanBay> sb = model.SanBays.ToList();

            detail = (from c in cb
                      join chab in chb on c.MaChangBay equals chab.MaChangBay
                      where c.MaChuyenBay == id
                      select new CB_Detail
                      {
                          MaChuyenBay = c.MaChuyenBay,
                          NgayCatCanh = c.NgayCatCanh,
                          GioCatCanh = c.GioCatCanh,
                          GiaNguoiLon = c.GiaNguoiLon,
                          ThoiGianBay = c.ThoiGianBay,
                          SanBay_CatCanh = chab.SanBay_CatCanh,
                          SanBay_HaCanh = chab.SanBay_HaCanh


                      }).ToList().FirstOrDefault();
            ViewBag.CC = (from chab in chb
                          join s in sb on chab.SanBay_CatCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_CatCanh = chab.SanBay_CatCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();
            ViewBag.HC = (from chab in chb
                          join s in sb on chab.SanBay_HaCanh equals s.MaSanBay
                          where chab.SanBay_CatCanh == detail.SanBay_CatCanh
                          where chab.SanBay_HaCanh == detail.SanBay_HaCanh
                          select new CB_Detail
                          {
                              SanBay_HaCanh = chab.SanBay_HaCanh,
                              ViTri = s.ViTri



                          }).ToList().FirstOrDefault();

            return View(detail);
        }

        public ActionResult XacNhanThanhToan()
        {
            return View();
        }

        public ActionResult HoanDoiVe()
        {
            return View();
        }
        public ActionResult KhuyenMai()
        {
            return View();
        }
        public ActionResult HuongDan()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult LienHe1()
        {
            return View();
        }
        public ActionResult DiemDenYeuThich()
        {
            return View();
        }

        public ActionResult CamNangDL()
        {
            return View();
        }

        public ActionResult KM()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }


    }
}