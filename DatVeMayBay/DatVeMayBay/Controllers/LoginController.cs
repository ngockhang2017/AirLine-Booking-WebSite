using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatVeMayBay.Models;
using DatVeMayBay.Dao;
namespace Final_APP.Controllers
{
    public class LoginController : Controller
    {
        private Model1 conn = new Model1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        // Post: Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Admin account)
        {
            if (ModelState.IsValid)
            {
                var data = conn.Admins.Where(s => s.TaiKhoan.Equals(account.TaiKhoan) && s.MatKhau.Equals(account.MatKhau)).FirstOrDefault();
                var tdn = conn.Admins.Where(s => s.TaiKhoan == account.TaiKhoan && s.MatKhau != account.MatKhau).FirstOrDefault();
                var mk = conn.Admins.Where(s => s.TaiKhoan != account.TaiKhoan && s.MatKhau == account.MatKhau).FirstOrDefault();
                if (data != null)
                {

                    Session["Admin"] = data.TaiKhoan.ToString();
                    return RedirectToAction("Index", "Admin");
                    //add session
                }
                else
                {
                    if (mk != null)
                    {
                        ModelState.AddModelError("", "Tên đăng nhập không đúng");

                    }
                    else if (tdn != null)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không đúng");
                    }

                }
            }
            ViewBag.ten = account.TaiKhoan;
            ViewBag.matk = account.MatKhau;
            return View(account);
        }
        //------------------Register -------------------------


        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(string tendn, string mk1, string mk2, string hoten)
        {
            //Boolean quyen = false;
            ViewBag.hoten = hoten;
            ViewBag.tendn = tendn;
            ViewBag.mk1 = mk1;
            ViewBag.mk2 = mk2;
            /*TaiKhoan _user = new TaiKhoan();
            _user.TenDangNhap = */
            // kiểm tra tên đăng nhập có trong csdl chưa
            if (ModelState.IsValid)
            {
                var check = conn.Admins.FirstOrDefault(s => s.TaiKhoan == tendn);
                if (check == null)  // ten dang nhap chua có trong csdl
                {
                    // kiểm tra hai mk1 mk2 có trùng nhau không
                    if (mk1 != mk2)
                    {
                        ModelState.AddModelError("", "Mật khẩu không trùng nhau!");
                        return View();
                    }
                    conn.Configuration.ValidateOnSaveEnabled = false;
                    // tạo đối tượng tài khoản và lưu vào csdl
                    Admin _user = new Admin();
                    _user.TaiKhoan = tendn;
                    _user.MatKhau = mk1;
                    _user.HoTen = hoten;
                    TempData["Name"] = hoten;
                    APIHelper.SendPostRequest("Admins/", _user);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                    return View();
                }
            }

            return View();
        }
    }
}