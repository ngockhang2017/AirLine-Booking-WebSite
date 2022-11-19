using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;

namespace VietMaxWatches.Controllers
{
    public class Admin1Controller : Controller
    {
          // GET: Admin
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

          }
          public ActionResult DangNhap()
          {
               return View();
          }
          //[HttpPost]
          //public ActionResult DangNhap(NguoiDung tk)
          //{

          //     Entities shop = new Entities();
          //     var taikhoan = shop.NguoiDung.SingleOrDefault(t => t.UserName.ToLower() == tk.UserName.ToLower() && t.Password.ToLower() == tk.Password.ToLower());
          //     if (ModelState.IsValid)
          //     {
          //          if (taikhoan != null)
          //          {
          //               Session["MaTk"] = taikhoan.MaNguoiDung;
          //               Session["TenTk"] = taikhoan.UserName;
          //               return Redirect("/QLHoaDon/Index");
          //          }
          //          else
          //          {
          //               ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
          //          }
          //     }
          //     return View();

          //}

          public ActionResult DangXuat()
          {
               Session["MaTk"] = null;
               Session["TenTk"] = null;
               return View("DangNhap");
          }
     }
}