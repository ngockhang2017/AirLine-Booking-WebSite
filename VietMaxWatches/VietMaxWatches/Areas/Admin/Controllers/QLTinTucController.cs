using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;
using System.IO;

namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class QLTinTucController : Controller
    {
          private static int MaNguonTin;
          // GET: QLTinTuc
          public ActionResult Index()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.NguonTinTuc.OrderByDescending(t => t.MaNguonTin).ToList();
                    return View(model);
               }

          }
          [HttpGet]
          public ActionResult ThemTin()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    return View();
               }

          }

          [HttpPost]
          [ValidateInput(false)]
          public ActionResult ThemTin(NguonTinTuc model, HttpPostedFileBase file)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    NguonTinTuc tin = new NguonTinTuc();
                    tin.TieuDe = model.TieuDe;
                    file = file ?? Request.Files["file"];
                    if (file != null && file.ContentLength > 0)
                    {
                         var fileName = Path.GetFileName(file.FileName);
                         if (fileName != null)
                         {
                              var path = Path.Combine(Server.MapPath("~/images/news/"), fileName);
                              file.SaveAs(path);
                              tin.Anh = "/images/news/" + fileName;


                         }
                    }
                    else
                    {
                         tin.Anh = "/images/p-1.png";


                    }
                    tin.NoiDung = model.NoiDung;
                    tin.NguoiTao = model.NguoiTao;
                    tin.NgayTao = model.NgayTao;
                    shop.NguonTinTuc.Add(tin);
                    shop.SaveChanges();
                    Response.Redirect("Index");
                    return View("Index");
               }

          }
          [HttpGet]
          public ActionResult SuaTin(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    MaNguonTin = id;
                    Entities shop = new Entities();
                    var model = shop.NguonTinTuc.SingleOrDefault(s => s.MaNguonTin == id);

                    return View(model);
               }

          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult SuaTin(NguonTinTuc tin, string img, HttpPostedFileBase file)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    file = file ?? Request.Files["file"];

                    Entities shop = new Entities();
                    var tintuc = shop.NguonTinTuc.SingleOrDefault(s => s.MaNguonTin == MaNguonTin);


                    if (file != null && file.ContentLength > 0)
                    {
                         var fileName = Path.GetFileName(file.FileName);
                         if (fileName != null)
                         {
                              var path = Path.Combine(Server.MapPath("~/images/news/"), fileName);
                              file.SaveAs(path);
                              tintuc.TieuDe = tin.TieuDe;
                              tintuc.NoiDung = tin.NoiDung;
                              tintuc.Anh = "/images/news/" + fileName;
                              shop.SaveChanges();
                         }
                    }
                    else
                    {
                         tintuc.TieuDe = tin.TieuDe;
                         tintuc.NoiDung = tin.NoiDung;
                         tintuc.Anh = img;
                         shop.SaveChanges();
                    }


                    var model = shop.NguonTinTuc.OrderByDescending(s => s.MaNguonTin).ToList();
                    return View("Index", model);
               }

          }


          public ActionResult XoaTin(int id)
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    Entities shop = new Entities();
                    var model = shop.NguonTinTuc.OrderByDescending(s => s.MaNguonTin).ToList();

                    var tintuc = shop.NguonTinTuc.SingleOrDefault(s => s.MaNguonTin == id);
                    if (tintuc != null)
                    {
                         shop.NguonTinTuc.Remove(tintuc);
                         shop.SaveChanges();
                    }

                    return RedirectToAction("Index", model);
               }


          }
     }
}