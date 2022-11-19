using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietMaxWatches.Models;
using VietMaxWatches.Areas;

namespace VietMaxWatches.Areas.Admin.Controllers
{
    public class QLNguoiDungController : Controller
    {
        // GET: QLNguoiDung
        private static int matk;
        public ActionResult Index()
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                Entities shop = new Entities();
                var model = shop.NguoiDung.ToList();
                return View(model);
            }
           
        }
        [HttpGet]
        public ActionResult ThemTk()
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
        public ActionResult ThemTk(NguoiDung model)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                Entities shop = new Entities();
                NguoiDung tk = new NguoiDung();
                tk.UserName = model.UserName;
                tk.Password = model.Password;
                shop.NguoiDung.Add(tk);
                shop.SaveChanges();
                Response.Redirect("Index");
                return View("Index");
            }
          
        }
        [HttpGet]
        public ActionResult SuaTk(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                matk = id;
                Entities shop = new Entities();
                var model = shop.NguoiDung.SingleOrDefault(s => s.MaNguoiDung == id);

                return View(model);
            }
           
        }
        [HttpPost]
        public ActionResult SuaTk(NguoiDung NguoiDung)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                Entities shop = new Entities();
                var tk = shop.NguoiDung.SingleOrDefault(s => s.MaNguoiDung == matk);
                tk.UserName = NguoiDung.UserName;
                tk.Password = NguoiDung.Password;
                shop.SaveChanges();
                var model = shop.NguoiDung.OrderByDescending(s => s.MaNguoiDung).ToList();
                return View("Index", model);
            }
           
        }


        public ActionResult XoaTk(int id)
        {
            if (Session["MaTk"] == null)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            else
            {
                Entities shop = new Entities();
                var model = shop.NguoiDung.OrderByDescending(s => s.MaNguoiDung).ToList();

                var tk = shop.NguoiDung.SingleOrDefault(s => s.MaNguoiDung == id);
                if (tk != null)
                {
                    shop.NguoiDung.Remove(tk);
                    shop.SaveChanges();
                }

                return RedirectToAction("Index", model);
            }
           

        }
    }
}