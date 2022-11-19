using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using System.IO;

namespace VietMaxWatches.Controllers
{
    public class CNTT_DinhVietController : Controller
    {
        // GET: CNTT_DinhViet
        public ActionResult Index()
        {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;
               return View();
        }

          public ActionResult Delete(int id)
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

          [HttpGet]
          public ActionResult ThemSp()
          {
               
                    Entities shop = new Entities();
                    var listHang = shop.ThuongHieuSanPham.OrderByDescending(p => p.MaThuongHieu).ToList();
                    List<SelectListItem> slSanPham = listHang.Select(t => new SelectListItem() { Text = t.TenThuongHieu, Value = t.MaThuongHieu.ToString() }).ToList();

                    ViewBag.Hang = slSanPham;
                    return View();
               
          }

          [HttpPost]
          [ValidateInput(false)]
          public ActionResult ThemSp(SanPham model, HttpPostedFileBase file)
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
                              sp.MaSanPham = model.MaSanPham;
                              sp.MaThuongHieu = model.MaThuongHieu;
                              sp.TenSanPham = model.TenSanPham;
                              sp.Anh = "/IMG/" + fileName;
                              sp.Gia = model.Gia;

                              shop.SanPham.Add(sp);
                              shop.SaveChanges();
                              Response.Redirect("Index");
                         }
                    }
                    else
                    {
                         SanPham sp = new SanPham();
                         sp.MaSanPham = model.MaSanPham;
                         sp.MaThuongHieu = model.MaThuongHieu;
                         sp.TenSanPham = model.TenSanPham;
                         sp.Anh = "/IMG/loading.png";
                         sp.Gia = model.Gia;

                         shop.SanPham.Add(sp);
                         shop.SaveChanges();
                         Response.Redirect("Index");
                    }


                    return View("Index");
               

          }
     }
}