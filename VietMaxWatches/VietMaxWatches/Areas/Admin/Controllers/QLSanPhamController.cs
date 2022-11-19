using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;

namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class QLSanPhamController : Controller
    {
          private static int MaSanPham;
          // GET: QLSanPham
          public ActionResult Index()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.SanPham.OrderByDescending(s => s.MaSanPham).ToList();
                    return View(model);
               }

          }
          [HttpGet]
          public ActionResult ThemSp()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    var listHang = shop.ThuongHieuSanPham.OrderByDescending(p => p.MaThuongHieu).ToList();
                    List<SelectListItem> slSanPham = listHang.Select(t => new SelectListItem() { Text = t.TenThuongHieu, Value = t.MaThuongHieu.ToString() }).ToList();

                    ViewBag.Hang = slSanPham;
                    return View();
               }


          }

          [HttpPost]
          [ValidateInput(false)]
          public ActionResult ThemSp(SanPham model, HttpPostedFileBase file)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    file = file ?? Request.Files["file"];
                    if (file != null && file.ContentLength > 0)
                    {
                         var fileName = Path.GetFileName(file.FileName);
                         if (fileName != null)
                         {
                              var path = Path.Combine(Server.MapPath("~/IMG/"), fileName);
                              file.SaveAs(path);

                              SanPham sp = new SanPham();
                              sp.MaThuongHieu = model.MaThuongHieu;
                              sp.TenSanPham = model.TenSanPham;
                              sp.MoTa = model.MoTa;
                              sp.Anh = "/IMG/" + fileName;
                              sp.Gia = model.Gia;
                              sp.GiaUuDai = model.GiaUuDai;   
                              
                              shop.SanPham.Add(sp);
                              shop.SaveChanges();
                              Response.Redirect("Index");
                         }
                    }
                    else
                    {
                         SanPham sp = new SanPham();
                         sp.MaThuongHieu = model.MaThuongHieu;
                         sp.TenSanPham = model.TenSanPham;
                         sp.MoTa = model.MoTa;
                         sp.Anh = "/IMG/loading.png";
                         sp.Gia = model.Gia;
                         sp.GiaUuDai = model.GiaUuDai;

                         shop.SanPham.Add(sp);
                         shop.SaveChanges();
                         Response.Redirect("Index");
                    }


                    return View("Index");
               }

          }
          [HttpGet]
          public ActionResult SuaSp(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    MaSanPham = id;
                    Entities shop = new Entities();
                    var model = shop.SanPham.SingleOrDefault(s => s.MaSanPham == id);
                    var listHang = shop.ThuongHieuSanPham.OrderByDescending(p => p.MaThuongHieu).ToList();
                    List<SelectListItem> slSanPham = listHang.Select(t => new SelectListItem() { Text = t.TenThuongHieu, Value = t.MaThuongHieu.ToString() }).ToList();

                    ViewBag.Hang = slSanPham;
                    return View(model);
               }

          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult SuaSp(SanPham sp, string img, HttpPostedFileBase file)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    file = file ?? Request.Files["file"];

                    Entities shop = new Entities();
                    var sanpham = shop.SanPham.SingleOrDefault(s => s.MaSanPham == MaSanPham);
                    sanpham.TenSanPham = sp.TenSanPham;
                    sanpham.MaThuongHieu = sp.MaThuongHieu;
                    sanpham.GiaUuDai = sp.GiaUuDai;
                    sanpham.Gia = sp.Gia;
                    sanpham.SoLuong = sp.SoLuong;
                    if (file != null && file.ContentLength > 0)
                    {
                         var fileName = Path.GetFileName(file.FileName);
                         if (fileName != null)
                         {
                              var path = Path.Combine(Server.MapPath("~/IMG/"), fileName);
                              file.SaveAs(path);
                              sanpham.Anh = "/IMG/" + fileName;
                         }
                    }
                    else
                    {
                         sanpham.Anh = img;
                    }
                    sanpham.MoTa = sp.MoTa;
                    sanpham.ThongTinChiTiet = sp.ThongTinChiTiet;
                    sanpham.ThongTinThem = sp.ThongTinThem;
                    shop.SaveChanges();

                    var model = shop.SanPham.OrderByDescending(s => s.MaSanPham).ToList();
                    return View("Index", model);
               }

          }


          public ActionResult XoaSp(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.SanPham.OrderByDescending(s => s.MaSanPham).ToList();

                    var sanpham = shop.SanPham.SingleOrDefault(s => s.MaSanPham == id);
                    if (sanpham != null)
                    {
                         shop.SanPham.Remove(sanpham);
                         shop.SaveChanges();
                    }

                    return RedirectToAction("Index", model);
               }


          }
     }
}