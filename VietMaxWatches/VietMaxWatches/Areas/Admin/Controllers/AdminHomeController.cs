using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;
namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
          // GET: Admin/Home
          public ActionResult Index()
          {
               if (Session["MaTk"] == null)
               {
                    return RedirectToAction("DangNhap", "Admin");
               }
               else
               {
                    return View();
               }

               //Entities context = new Entities();
               //var model = context.SanPham.OrderBy(p => p.MaSanPham).ToList();
               //return View(model);
          }
          public ActionResult DangNhap()
          {
               return View();
          }

          [HttpPost]
          public ActionResult DangNhap(NguoiDung tk)
          {

               Entities shop = new Entities();
               var taikhoan =

                        shop.NguoiDung.SingleOrDefault(t => t.UserName.ToLower() == tk.UserName.ToLower() && t.Password.ToLower() == tk.Password.ToLower());
               if (ModelState.IsValid)
               {
                    if (taikhoan != null)
                    {
                         Session["MaTk"] = taikhoan.MaNguoiDung;
                         Session["TenTk"] = taikhoan.UserName;
                         return Redirect("/Admin/QLHoaDon/Index");
                    }
                    else
                    {
                         ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                    }
               }
               return View();

          }

          public ActionResult DangXuat()
          {
               Session["MaTk"] = null;
               Session["TenTk"] = null;
               return View("DangNhap");
          }


     }
}