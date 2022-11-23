using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatVeMayBay.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Data.Entity;
//using PagedList;    // dung phan trang
//using Final_APP.Models;

namespace DatVeMayBay.Controllers
{
    public class AdminController : Controller
    {
        private Model1 conn = new Model1();
        // GET: Admin
        public ActionResult Index()

        {
            return View();
            //if (Session["Admin"] != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");
            //}
        }
        //Quan li nguoi dung 

        //Thong tin tai khona khach hang
        public ActionResult UserAccount(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            // truy vấn
            List<Admin> ketqua = conn.Admins.OrderBy(x => x.MaTaiKhoan).ToList();
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //return View(ketqua.ToPagedList(pageNumber, pageSize));
            return View(ketqua);
        }

        //----------------------end thong tin tai khoan

        //Xoa tai khoan
        public ActionResult DeleteAccount(int maTaiKhoan)
        {
            if (maTaiKhoan == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Admin taiKhoan = conn.Admins.Find(Admin => Admin.Contains("maTaiKhoan"));
         
            foreach (var taikhoan in conn.Admins)
            {
                if(taikhoan.MaTaiKhoan == maTaiKhoan)
                    return View(taikhoan);
            }

       
                return HttpNotFound();
            
            //return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountConfirmed(string TenDangNhap)
        {


            //conn.Admins.SqlQuery("ALTER TABLE Taikhoan NOCHECK CONSTRAINT ALL;");
            //conn.Admins.SqlQuery("ALTER TABLE NGUOIDUNG NOCHECK CONSTRAINT ALL;");
            //Admin taiKhoan_admin = conn.Admins.Find(TenDangNhap);
            Admin taikhoan_admin = new Admin();
            foreach (var tk in conn.Admins)
            {
                if (tk.TaiKhoan == TenDangNhap)
                    taikhoan_admin = tk;
            }
            conn.Admins.Remove(taikhoan_admin);
            conn.SaveChanges();
            //conn.TaiKhoans.SqlQuery("ALTER TABLE TAIKHOAN CHECK CONSTRAINT ALL");
            //conn.TaiKhoans.SqlQuery("ALTER TABLE NGUOIDUNG NOCHECK CONSTRAINT ALL;");
            return RedirectToAction("UserAccount");
        }

        //------------End xoa tai khoan



        //Them nguoi dung
        public ActionResult AddUser()
        {
            return View();
        }


        // POST:NguoiDung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(KhachHang khachhang_Account)
        {
            if (ModelState.IsValid)
            {
                // kiem tra ten đăng nhập có tồn tại chưa
                var data = conn.KhachHangs.Where(x => x.TaiKhoan.Equals(khachhang_Account.TaiKhoan)).FirstOrDefault();
                if (data != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                    return View(khachhang_Account);
                }

               
                conn.KhachHangs.Add(khachhang_Account);
                
                conn.SaveChanges();
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Tạo tài khoản thành công";
            }
            else
            {
                return View(khachhang_Account);
            }
            //  return Content(message);


            return RedirectToAction("AddUser");
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(User_Account user_Account)
        {
            
            var taiKhoan = new TaiKhoan()
            {
                TenDangNhap = user_Account.TenDangNhap,
                MatKhau = user_Account.MatKhau,
                Quyen = user_Account.Quyen =false,
            };

            var nguoiDung = new NguoiDung()
            {
                MaNguoiDung = user_Account.MaNguoiDung,
                HoTen = user_Account.HoTen,
                SDT = user_Account.SDT,
                Email = user_Account.Email,
                TenDangNhap = user_Account.TenDangNhap,
            };
            
            conn.TaiKhoans.Add(taiKhoan);
            conn.NguoiDungs.Add(nguoiDung);
            conn.SaveChanges();
            return RedirectToAction("AddUser");
        }*/
        //-----------End them nguoi dung




        //Thong tin nguoi dung
        public ActionResult UserDetail(int? page)
        {
            if (page == null) page = 1;
            List<KhachHang> ketqua = conn.KhachHangs.OrderBy(x => x.MaKH).ToList();

            // List<TaiKhoan> account = conn.TaiKhoans.ToList();
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //return View(ketqua.ToPagedList(pageNumber, pageSize));
            return View(ketqua);
        }
        [HttpPost]
        public ActionResult UserDetail(string timkiem, int? page)
        {
            if (page == null) page = 1;

            ViewBag.timkiem = timkiem;
            List<KhachHang> nd = conn.KhachHangs.ToList();
            var NguoiDung = (from n in nd
                             where (n.MaKH == Int32.Parse(timkiem) || n.TenKH.Contains(timkiem)
                                    || n.SDT.Contains(timkiem) || n.DiaChi.Contains(timkiem))
                             select new KhachHang
                             {
                                 MaKH = n.MaKH,
                                 TenKH = n.TenKH,
                                 SDT = n.SDT,
                                 DiaChi = n.DiaChi,
                             }).OrderBy(x => x.MaKH).ToList();

            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //return View(NguoiDung.ToPagedList(pageNumber, pageSize));
            return View(NguoiDung);

        }
        //Sua thong tin nguoi dung
        // GET: NguoiDungs/Edit
        public ActionResult EditUser(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang nguoiDung = new KhachHang();
            foreach (var kh in conn.KhachHangs)
            {
                if (kh.MaKH == id)
                {
                    nguoiDung = kh;
                    return View(nguoiDung);
                }    
                    
            }

           
                return HttpNotFound();
            
            //ViewBag.TenDangNhap = new SelectList(conn.TaiKhoans, "TenDangNhap", "MatKhau", nguoiDung.TenDangNhap);
            
        }

        // POST: NguoiDungs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser( KhachHang nguoiDung)
        {
            if (ModelState.IsValid)
            {
                conn.Entry(nguoiDung).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("UserDetail");
            }
            //ViewBag.TenDangNhap = new SelectList(conn.TaiKhoans, "TenDangNhap", "MatKhau", nguoiDung.TenDangNhap);
            return View(nguoiDung);
        }



        //---------------End quan li nguoi dung

        //-----------------Lich su dat ve

        public ActionResult LichSuDatVe(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            // truy vấn
            List<LichSuDatVe> lichSuDatVe = new List<LichSuDatVe>();
            List<KhachHang> nguoidung = conn.KhachHangs.ToList();
            List<PhieuDatVe> phieudatve = conn.PhieuDatVes.ToList();
            List<KhachHang_DatVe> kh_datve = conn.KhachHang_DatVes.ToList();
            List<HoaDon> hoadon = conn.HoaDons.ToList();
            List<Ve> ve = conn.Ves.ToList();
            var LichSuDatVe = (from n in nguoidung
                               join p in kh_datve on n.MaKH equals p.MaKH
                               join h in phieudatve on p.MaPhieuDatVe equals h.MaPhieuDatVe
                               join v in ve on h.MaPhieuDatVe equals v.MaPhieuDatVe
                               join hd in hoadon on v.MaVe equals hd.MaVe
                               select new LichSuDatVe
                               {
                                   MaKH = n.MaKH,
                                   HoKH = n.HoKH,
                                   TenKH = n.TenKH,
                                   NgayDat = h.NgayDat,
                                   ThanhTien = hd.ThanhTien,

                               }).OrderBy(x => x.NgayDat);
            lichSuDatVe = LichSuDatVe.ToList();
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //return View(lichSuDatVe.ToPagedList(pageNumber, pageSize));
            return View(lichSuDatVe);
        }


        [HttpPost]
        public ActionResult LichSuDatVe(string timkiem, int? page)
        {
            /*if (timkiem == "12:00:00 AM")
            {
                timkiem = "";
            }
            else
            {
                timkiem = timkiem.Replace("12:00:00 AM", "");
            }*/
            timkiem = timkiem.Replace("12:00:00 AM", "");
            ViewBag.timkiem = timkiem;

            List<LichSuDatVe> lichSuDatVe = new List<LichSuDatVe>();
            List<KhachHang> nguoidung = conn.KhachHangs.ToList();
            List<PhieuDatVe> phieudatve = conn.PhieuDatVes.ToList();
            List<KhachHang_DatVe> kh_datve = conn.KhachHang_DatVes.ToList();
            List<HoaDon> hoadon = conn.HoaDons.ToList();
            List<Ve> ve = conn.Ves.ToList();
            if (page == null) page = 1;
            // truy vấn
            var LichSuDatVe = (from n in nguoidung
                               join p in kh_datve on n.MaKH equals p.MaKH
                               join h in phieudatve on p.MaPhieuDatVe equals h.MaPhieuDatVe
                               join v in ve on h.MaPhieuDatVe equals v.MaPhieuDatVe
                               join hd in hoadon on v.MaVe equals hd.MaVe
                               where (n.MaKH == Int32.Parse(timkiem) || n.TenKH.Contains(timkiem)
                                      || h.NgayDat.ToString().Contains(timkiem) || hd.ThanhTien.ToString().Contains(timkiem))
                               select new LichSuDatVe
                               {
                                   MaKH = n.MaKH,
                                   HoKH = n.HoKH,
                                   TenKH = n.TenKH,
                                   NgayDat = h.NgayDat,
                                   ThanhTien = hd.ThanhTien,

                               }).OrderBy(x => x.NgayDat);
            lichSuDatVe = LichSuDatVe.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //return View(lichSuDatVe.ToPagedList(pageNumber, pageSize));
            return View(lichSuDatVe);
        }

        //-----------------Lich su dat ve end

        // Quan li chuyen bay start
        // GET: ChangBays
        public ActionResult IndexChangBay(int? page)
        {
            if (page == null) page = 1;

            var changBays = conn.ChangBays.OrderBy(x => x.MaChangBay).ToList();

            var sanbay = (from s in conn.SanBays select s).ToList();

            ViewBag.SanBay = sanbay;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //return View(changBays.ToPagedList(pageNumber, pageSize));

            return View(changBays);
        }

        // GET: ChangBays/Edit/5
        public ActionResult EditChangBay(int id)
        {
            ChangBay changBay = new ChangBay();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var chab in conn.ChangBays)
            {
                if (chab.MaChangBay == id)
                {
                    changBay = chab;
                    ViewBag.SanBay_CatCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_CatCanh);
                    ViewBag.SanBay_HaCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_HaCanh);
                    return View(changBay);
                }

            }                   
            return HttpNotFound();           
        }

        // POST: ChangBays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChangBay( ChangBay changBay)
        {
            if (ModelState.IsValid)
            {
                conn.Entry(changBay).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("IndexChanBay");
            }
            ViewBag.SanBay_CatCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_CatCanh);
            ViewBag.SanBay_HaCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_HaCanh);
            return View(changBay);
        }
        // GET: ChangBays/Create


        public ActionResult CreateChangBay()
        {
            ViewBag.SanBay_CatCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay");
            ViewBag.SanBay_HaCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay");
            return View();
        }

        // POST: ChangBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult CreateChangBay(ChangBay changBay)
        {

            if (ModelState.IsValid)
            {
                // kiểm tra chặng bay
                var data = conn.ChangBays.Where(x => x.SanBay_CatCanh.Equals(changBay.SanBay_CatCanh) && x.SanBay_HaCanh.Equals(changBay.SanBay_HaCanh)).FirstOrDefault();
                if (data != null)
                {
                    TempData["AlertType"] = "alert-warning";
                    TempData["AlertMessage"] = "Chặng bay này đã tồn tại";
                    //  ModelState.AddModelError("", "Chặng bay này đã tồn tại");
                    return RedirectToAction("CreateChangBay");
                }
                //======
                //string machangbay = "ChaB";
                //var last_ChuyenBay = conn.ChangBays.ToList();
                //var l = last_ChuyenBay.LastOrDefault();
                //string ma_last_ChuyenBay = l.MaChangBay;
                //string str_number = ma_last_ChuyenBay.Replace("ChaB", "");
                //int int_MaChuyenBay = Int32.Parse(str_number) + 1;
                //if (int_MaChuyenBay < 10)
                //{
                //    machangbay = machangbay + "0" + int_MaChuyenBay;
                //}
                //else
                //{
                //    machangbay = machangbay + int_MaChuyenBay;
                //}

                //changBay.MaChangBay = machangbay;
                conn.ChangBays.Add(changBay);
                conn.SaveChanges();
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Tạo tài khoản thành công";
                return RedirectToAction("CreateChangBay");
            }

            /*ViewBag.SanBay_CatCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_CatCanh);
            ViewBag.SanBay_HaCanh = new SelectList(conn.SanBays, "MaSanBay", "TenSanBay", changBay.SanBay_HaCanh);*/
            return View(changBay);
        }
        //Chuyen bay ------------
        // GET: ChuyenBays
        public ActionResult IndexChuyenBay(int? page)
        {
            if (page == null) page = 1;
            var chuyenBays = conn.ChuyenBays
                .OrderBy(c => c.MaChuyenBay).ToList();
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //return View(chuyenBays.ToPagedList(pageNumber, pageSize));
            return View(chuyenBays);
        }

        // GET: ChuyenBays/ChiTiet
        public ActionResult DetailChuyenBay(int id)
        {
            ChuyenBay chuyenBay = new ChuyenBay();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var chb in conn.ChuyenBays)
            {
                if (chb.MaChuyenBay == id)
                {
                    chuyenBay = chb;
                    return View(chuyenBay);
                }

            }
            
                return HttpNotFound();
            
        }

        // GET: ChuyenBays/Edit/5
        public ActionResult EditChuyenBay(int id)
        {
            ChuyenBay chuyenBay = new ChuyenBay();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var chb in conn.ChuyenBays)
            {
                if (chb.MaChuyenBay == id)
                {
                    chuyenBay = chb;
                    ViewBag.MaChangBay = new SelectList(conn.ChangBays, "MaChangBay", "SanBay_CatCanh", chuyenBay.MaChangBay);
                    ViewBag.LoaiMayBay = new SelectList(conn.MayBays, "LoaiMayBay", "TenMayBay", chuyenBay.LoaiMayBay);
                    return View(chuyenBay);
                }

            }
           
                return HttpNotFound();
            
         
        }

        // POST: ChuyenBays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditChuyenBay( ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                conn.Entry(chuyenBay).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChangBay = new SelectList(conn.ChangBays, "MaChangBay", "SanBay_CatCanh", chuyenBay.MaChangBay);
            ViewBag.LoaiMayBay = new SelectList(conn.MayBays, "LoaiMayBay", "TenMayBay", chuyenBay.LoaiMayBay);
            return View(chuyenBay);
        }

        // GET: ChuyenBays/Create
        public ActionResult CreateChuyenBay()
        {
            ViewBag.MaChangBay = new SelectList(conn.ChangBays, "MaChangBay", "MaChangBay");
            ViewBag.LoaiMayBay = new SelectList(conn.MayBays, "LoaiMayBay", "TenMayBay");
            return View();
        }

        // POST: ChuyenBays/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult CreateChuyenBay([Bind(Include = "MaChuyenBay,TGbay,NgayBay,GioBay,LoaiMayBay,Gia,SaleTreEm,SaleEmBe,MaChangBay")] ChuyenBay chuyenBay)
        public ActionResult CreateChuyenBay(ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                //string MaChuyenBay = "ChuB";
                //var last_ChuyenBay = conn.ChuyenBays.ToList();
                //var l = last_ChuyenBay.LastOrDefault();
                //string ma_last_ChuyenBay = l.MaChuyenBay;
                //string str_number = ma_last_ChuyenBay.Replace("ChuB", "");
                //int int_MaChuyenBay = Int32.Parse(str_number) + 1;
                //if (int_MaChuyenBay < 10)
                //{
                //    MaChuyenBay = MaChuyenBay + "0" + int_MaChuyenBay;
                //}
                //else
                //{
                //    MaChuyenBay = MaChuyenBay + int_MaChuyenBay;
                //}
                //chuyenBay.MaChuyenBay = MaChuyenBay;
                conn.ChuyenBays.Add(chuyenBay);
                try
                {
                    conn.SaveChanges();
                }
                catch
                {
                    // TempData["AlertMessage"] = "Vui lòng kiểm tra lại các trường!";
                    TempData["AlertType"] = "alert-warning";
                    TempData["AlertMessage"] = "Kiểm tra lại các thông số nhập vào";
                    return RedirectToAction("CreateChuyenBay"); //=====================
                }

                return RedirectToAction("CreateChuyenBay");
            }

            ViewBag.MaChangBay = new SelectList(conn.ChangBays, "MaChangBay", "SanBay_CatCanh", chuyenBay.MaChangBay);
            ViewBag.LoaiMayBay = new SelectList(conn.MayBays, "LoaiMayBay", "TenMayBay", chuyenBay.LoaiMayBay);
            return View(chuyenBay);
        }


        // POST: ChuyenBays/Delete/5
        [HttpPost]

        public ActionResult DeleteChuyenBay(int id)
        {
            ChuyenBay chuyenBay = new ChuyenBay();
            try
            {
                using (var conn = new Model1())
                {
                    foreach (var chb in conn.ChuyenBays)
                    {
                        if (chb.MaChuyenBay == id)
                        {
                            chuyenBay = chb;
                            conn.ChuyenBays.Remove(chuyenBay);
                            conn.SaveChanges();
                            return Json(new { message = "Success!" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                   
                }
            }
            catch (Exception e)
            {
                return Json(new { message = "Fail!" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        //---------------------End Chuyen Bay

        public ActionResult IndexSanBay(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            var sanbay = conn.SanBays.OrderBy(x => x.MaSanBay).ToList();
            //return View(sanbay.ToPagedList(pageNumber, pageSize));
            return View(sanbay);
        }

        // GET: SanBays/Create
        public ActionResult CreateSanBay()
        {
            return View();
        }

        // POST: SanBays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSanBay(SanBay sanBay)
        {
            if (ModelState.IsValid)
            {
                var data = conn.SanBays.Where(x => x.MaSanBay == sanBay.MaSanBay).FirstOrDefault();
                if (data != null)
                {
                    ModelState.AddModelError("", "Mã sân bay đã tồn tại");
                    return View(sanBay);
                }
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Tạo sân bay thành công";
                conn.SanBays.Add(sanBay);
                conn.SaveChanges();
                return RedirectToAction("CreateSanBay");
            }

            return View(sanBay);
        }

        // index
        public ActionResult IndexHoaDon(int? page)
        {
            if (page == null) page = 1;
            // truy vấn
            List<HoaDon> hoaDons = conn.HoaDons.OrderBy(x => x.MaHoaDon).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //return View(hoaDons.ToPagedList(pageNumber, pageSize));

            return View(hoaDons);
        }


        [HttpPost]
        public ActionResult IndexHoaDon(string timkiem, int? page)
        {
            if (page == null) page = 1;
            // truy vấn
            timkiem = timkiem.Replace("12:00:00 AM", "");
            ViewBag.timkiem = timkiem;
            List<HoaDon> hoadon = conn.HoaDons.ToList();

            /* List<HoaDon> hoadon = conn.HoaDons.Where(x => x.MaHoaDon.Contains(timkiem)
                 || x => x.NgayXuatDon.Contains(timkiem) || x => x.ThanhTien.Contains(timkiem)).ToList();*/
            if (page == null) page = 1;
            // truy vấn
            var hoaDons = (from hd in hoadon

                           where (hd.MaHoaDon == Int32.Parse(timkiem) || hd.NgayLap.ToString().Contains(timkiem)
                                  || hd.ThanhTien.ToString().Contains(timkiem))
                           select new HoaDon
                           {
                               MaHoaDon = hd.MaHoaDon,
                               NgayLap = hd.NgayLap,
                               ThanhTien = hd.ThanhTien,
                           }).OrderBy(x => x.MaHoaDon);
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //return View(hoaDons.ToPagedList(pageNumber, pageSize));

            return View(hoaDons);
        }

        // Chi tiet hoa don

        // GET: HoaDons/Details/5
        public ActionResult DetailHoaDon(int id)
        {
            HoaDon hoaDon = new HoaDon();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var hd in conn.HoaDons)
            {
                if (hd.MaHoaDon == id)
                {
                    hoaDon = hd;

                    return View(hoaDon);
                }

            }          
            
                return HttpNotFound();
            
           
        }

        //-----------Hoa Don end
        //--------------Ngan hANG

        //index
        public ActionResult IndexNganHang(int? page)
        {
            if (page == null) page = 1;

            var nganHangs = conn.NganHangs.OrderBy(x => x.MaNganHang).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

           // return View(nganHangs.ToPagedList(pageNumber, pageSize));
            return View(nganHangs);
        }

        // GET: NganHangs/Edit/5
        public ActionResult EditNganHang(string id)
        {
            NganHang nganhang = new NganHang();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var nh in conn.NganHangs)
            {
                if (nh.MaNganHang == id)
                {
                    nganhang = nh;

                    return View(nganhang);
                }

            }
           
            
                return HttpNotFound();
            
            //ViewBag.MaHinhThucTT = new SelectList(conn.HinhThucThanhToans, "MaHinhThucTT", "TenHinhThucTT", nganHang.MaHinhThucTT);
          
        }

        // POST: NganHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNganHang(NganHang nganHang)
        {
            if (ModelState.IsValid)
            {
                conn.Entry(nganHang).State = EntityState.Modified;
                conn.SaveChanges();
                return RedirectToAction("IndexNganHang");
            }
            // ViewBag.MaHinhThucTT = new SelectList(conn.HinhThucThanhToans, "MaHinhThucTT", "TenHinhThucTT", nganHang.MaHinhThucTT);
            return RedirectToAction("IndexNganHang");
        }

        // GET: NganHangs/Create
        public ActionResult CreateNganHang()
        {
            ViewBag.MaHinhThucTT = new SelectList(conn.HinhThucThanhToans, "MaHinhThucTT", "TenHinhThucTT");
            return View();
        }

        // POST: NganHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNganHang(NganHang nganHang)
        {
            if (ModelState.IsValid)
            {
                var data = conn.NganHangs.Where(x => x.MaNganHang.Equals(nganHang.MaNganHang)
                                                || x.TenNganHang.Equals(nganHang.TenNganHang)).FirstOrDefault();
                if (data != null)
                {
                    ModelState.AddModelError("", "Mã ngân hàng hoặc tên ngân hàng đã tồn tại");
                    return View(nganHang);
                }
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Thêm ngân hàng thành công";
                conn.NganHangs.Add(nganHang);
                conn.SaveChanges();
                return RedirectToAction("CreateNganHang");
            }

            //ViewBag.MaHinhThucTT = new SelectList(conn.HinhThucThanhToans, "MaHinhThucTT", "TenHinhThucTT", nganHang.MaHinhThucTT);
            return View(nganHang);
        }


        //Delete Ngan Hang ----------------
        // POST: NganHang/Delete
        [HttpPost]

        public ActionResult DeleteNganHang(string id)
        {
            NganHang nganHang = new NganHang();
            try
            {
                using (var conn = new Model1())
                {
                    foreach (var nh in conn.NganHangs)
                    {
                        if (nh.MaNganHang == id)
                        {
                            nganHang = nh;
                            conn.NganHangs.Remove(nganHang);
                            conn.SaveChanges();
                            return Json(new { message = "Success!" }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                return Json(new { message = "Fail!" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        //Khuyen mai ------------------------------------------

        public ActionResult IndexKhuyenMai(int? page)
        {
            if (page == null) page = 1;
            var khuyenmai = conn.KhuyenMais.OrderBy(x => x.MaKhuyenMai).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //return View(khuyenmai.ToPagedList(pageNumber, pageSize));
            return View(khuyenmai);

        }

        // GET: KhuyenMais/Create
        public ActionResult CreateKhuyenMai()
        {
            return View();
        }

        // POST: KhuyenMai/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateKhuyenMai( KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                conn.KhuyenMais.Add(khuyenMai);
                conn.SaveChanges();
                return RedirectToAction("IndexKhuyenMai");
            }

            return View(khuyenMai);
        }

        [HttpPost]

        public ActionResult DeleteKhuyenMai(int id)
        {
            KhuyenMai khuyenMai = new KhuyenMai();
            try
            {
                using (var conn = new Model1())
                {
                    foreach (var km in conn.KhuyenMais)
                    {
                        if (km.MaKhuyenMai == id)
                        {
                            khuyenMai = km;
                            conn.KhuyenMais.Remove(khuyenMai);
                            conn.SaveChanges();
                            return Json(new { message = "Success!" }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                return Json(new { message = "Fail!" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

    }
}



