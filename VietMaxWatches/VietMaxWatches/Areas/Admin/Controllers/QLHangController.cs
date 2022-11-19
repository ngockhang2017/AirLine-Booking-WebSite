using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;

namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class QLHangController : Controller
    {
          // GET: QLHang
          private static string mahang;
          public ActionResult Index()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.ThuongHieuSanPham.ToList();
                    return View(model);
               }


          }
          [HttpGet]
          public ActionResult ThemHang()
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
          public ActionResult ThemHang(ThuongHieuSanPham model)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    ThuongHieuSanPham hang = new ThuongHieuSanPham();
                    hang.MaThuongHieu = model.MaThuongHieu;
                    hang.TenThuongHieu = model.TenThuongHieu;

                    shop.ThuongHieuSanPham.Add(hang);
                    shop.SaveChanges();
                    Response.Redirect("Index");
                    return View("Index");
               }

          }
          [HttpGet]
          public ActionResult SuaHang(string id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    mahang = id;
                    Entities shop = new Entities();
                    var model = shop.ThuongHieuSanPham.SingleOrDefault(s => s.MaThuongHieu == id);

                    return View(model);
               }


          }
          [HttpPost]
          public ActionResult SuaHang(ThuongHieuSanPham hangsx)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var hang = shop.ThuongHieuSanPham.SingleOrDefault(s => s.MaThuongHieu == mahang);
                    hang.TenThuongHieu = hangsx.TenThuongHieu;

                    shop.SaveChanges();
                    var model = shop.ThuongHieuSanPham.OrderByDescending(s => s.MaThuongHieu).ToList();
                    return View("Index", model);
               }

          }


          public ActionResult XoaHang(string id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "AdminHome");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.ThuongHieuSanPham.OrderByDescending(s => s.MaThuongHieu).ToList();

                    var hang = shop.ThuongHieuSanPham.SingleOrDefault(s => s.MaThuongHieu == id);
                    if (hang != null)
                    {
                         shop.ThuongHieuSanPham.Remove(hang);
                         shop.SaveChanges();
                    }

                    return RedirectToAction("Index", model);
               }


          }
     }
}