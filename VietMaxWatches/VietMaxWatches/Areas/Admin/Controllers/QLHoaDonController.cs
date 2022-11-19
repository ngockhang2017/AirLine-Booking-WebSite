using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;

namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class QLHoaDonController : Controller
    {
          // GET: QLHoaDon

          private static int mahd;
          public ActionResult Index()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();

                    var model = shop.HoaDon.Where(h => h.TrangThai == 0).ToList();
                    ViewBag.model = model;

                    var daduyet = shop.HoaDon.Where(h => h.TrangThai == 1).ToList();
                    ViewBag.list_daduyet = daduyet;
                    

                    return View(model);
               }

          }
          [HttpGet]
          public ActionResult ThemHd()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    return View();
               }

          }

          [HttpPost]
          public ActionResult ThemHd(HoaDon model)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    HoaDon hd = new HoaDon();

                    hd.NgayTao = DateTime.Now;
                    hd.TenKhachHang = model.TenKhachHang;
                    hd.DiaChiGiaoHang = model.DiaChiGiaoHang;
                    hd.ThoiGianGiaoHang = model.ThoiGianGiaoHang;
                    hd.Email = model.Email;
                    hd.SDT = model.SDT;           
                    hd.TongTien = 0;
                    shop.HoaDon.Add(hd);

                    shop.SaveChanges();
                    Response.Redirect("Index");
                    return View("Index");
               }

          }
          [HttpGet]
          public ActionResult SuaHd(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    mahd = id;
                    Entities shop = new Entities();
                    var model = shop.HoaDon.SingleOrDefault(s => s.MaHoaDon == id);

                    return View(model);
               }

          }
          [HttpPost]
          public ActionResult SuaHd(HoaDon hoaDon)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var hd = shop.HoaDon.SingleOrDefault(s => s.MaHoaDon == mahd);

                    hd.TenKhachHang = hoaDon.TenKhachHang;
                    hd.DiaChiGiaoHang = hoaDon.DiaChiGiaoHang;
                    hd.ThoiGianGiaoHang = hoaDon.ThoiGianGiaoHang;
                    hd.Email = hoaDon.Email;
                    hd.SDT = hoaDon.SDT; 
                    
                    shop.SaveChanges();
                    var model = shop.HoaDon.OrderByDescending(s => s.MaHoaDon).ToList();
                    return View("Index", model);
               }

          }


          public ActionResult XoaHd(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = new DonHangViewModel()
                    {
                         DonHangDaDuyet = shop.HoaDon.Where(h => h.TrangThai == 1).ToList(),
                         DonHangChuaDuyet = shop.HoaDon.Where(h => h.TrangThai == 0).ToList()
                    };


                    var hd = shop.HoaDon.SingleOrDefault(s => s.MaHoaDon == id);
                    if (hd != null)
                    {
                         foreach (var item in shop.ChiTietDatHang.Where(c => c.MaHoaDon == id).ToList())
                         {
                              var sp = shop.SanPham.SingleOrDefault(s => s.MaSanPham == item.MaSanPham);
                              sp.SoLuong += item.SoLuong;
                              shop.ChiTietDatHang.Remove(item);
                         }
                         shop.HoaDon.Remove(hd);
                         shop.SaveChanges();
                    }

                    return RedirectToAction("Index", model);
               }


          }

          public ActionResult ChiTietHd(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    mahd = id;
                    ViewBag.mahd = id;
                    Entities shop = new Entities();
                    var model = shop.ChiTietDatHang.Where(c => c.MaHoaDon == mahd).OrderByDescending(c => c.MaChiTietDatHang).ToList();
                    List<SelectListItem> sanPham = new List<SelectListItem>();
                    for (int i = 0; i < shop.SanPham.ToList().Count; i++)
                    {
                         SelectListItem sl = new SelectListItem() { Text = shop.SanPham.ToList()[i].TenSanPham, Value = shop.SanPham.ToList()[i].MaSanPham.ToString() };
                         sanPham.Add(sl);
                    }
                    ViewBag.Sp = sanPham;
                    return View(model);
               }

          }
          [HttpPost]
          public ActionResult ThemChiTietHd(int SanPham, int SoLuong)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var sp = shop.SanPham.SingleOrDefault(s => s.MaSanPham == SanPham);
                    var hd = shop.HoaDon.SingleOrDefault(h => h.MaHoaDon == mahd);
                    ChiTietDatHang ct = new ChiTietDatHang();
                    ct.MaHoaDon = mahd;
                    ct.MaSanPham = SanPham;
                    ct.SoLuong = SoLuong;
                    ct.TT_ThanhToan = SoLuong * sp.Gia;
                    shop.ChiTietDatHang.Add(ct);
                    hd.TongTien += ct.TT_ThanhToan;
                    shop.SaveChanges();
                    return RedirectToAction("ChiTietHd", new { id = mahd });
               }

          }
          public ActionResult XoaChiTietHd(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();

                    var ct = shop.ChiTietDatHang.SingleOrDefault(s => s.MaChiTietDatHang == id);
                    var hd = shop.HoaDon.SingleOrDefault(h => h.MaHoaDon == mahd);
                    if (ct != null)
                    {
                         shop.ChiTietDatHang.Remove(ct);
                         hd.TongTien -= ct.TT_ThanhToan;
                         shop.SaveChanges();
                    }

                    return RedirectToAction("ChiTietHd", new { id = mahd });

               }

          }
          //[HttpPost]
          //public ActionResult ThemChiTietHd(ChiTietHoaDon model)
          //{
          //    Entities shop=new Entities();

          //    return View();
          //}
          [HttpPost]
          public ActionResult SuaChiTietHd(int id, int soLuong)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var ct = shop.ChiTietDatHang.SingleOrDefault(c => c.MaChiTietDatHang == id);

                    ct.HoaDon.TongTien += (soLuong * ct.SanPham.Gia) - ct.TT_ThanhToan;
                    ct.SoLuong = soLuong;
                    ct.TT_ThanhToan = soLuong * ct.SanPham.Gia;
                    shop.SaveChanges();
                    return this.Json(new { data = 1 }, JsonRequestBehavior.AllowGet); ;
               }

          }
          public ActionResult XacNhanDonHang()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.HoaDon.Where(h => h.TrangThai == 0).ToList();
                    return View(model);
               }

          }

          public ActionResult Duyet(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = new DonHangViewModel()
                    {
                         DonHangDaDuyet = shop.HoaDon.Where(h => h.TrangThai == 1).ToList(),
                         DonHangChuaDuyet = shop.HoaDon.Where(h => h.TrangThai == 0).ToList()
                    };


                    var hd = shop.HoaDon.SingleOrDefault(h => h.MaHoaDon == id);
                    hd.TrangThai = 1;

                    var listChiTiet = shop.ChiTietDatHang.Where(c => c.MaHoaDon == id).ToList();
                    foreach (var item in listChiTiet)
                    {
                         var sp = shop.SanPham.SingleOrDefault(s => s.MaSanPham == item.MaSanPham);
                         if (sp.SoLuong > item.SoLuong)
                         {
                              sp.SoLuong -= item.SoLuong;
                         }

                    }
                    shop.SaveChanges();
                    return RedirectToAction("Index", model);
               }

          }
     }
}