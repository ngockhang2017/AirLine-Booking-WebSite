using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VietMaxWatches.Models;

namespace VietMaxWatches.Controllers
{
     public class GioHangController : Controller
     {
          // GET: GioHang
          public ActionResult XoaSanPham(int id)
          {
               GioHang objCart = (GioHang)Session["Cart"];
               if (objCart != null)
               {
                    objCart.XoaSanPham(id);
                    Session["Cart"] = objCart;
               }
               return RedirectToAction("index");
          }
          // thêm vào giỏ hàng 1 sản phẩm có id = id của sản phẩm
          [HttpPost]
          public ActionResult ThemVaoGioHang(int id, int soLuong)
          {

               Entities db = new Entities();

               var model = db.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var p = db.SanPham.SingleOrDefault(s => s.MaSanPham.Equals(id));

               if (p != null)
               {
                    GioHang objCart = (GioHang)Session["Cart"];
                    if (objCart == null)
                    {
                         objCart = new GioHang();
                    }
                    GioHang.GioHangItem item = new GioHang.GioHangItem()
                    {
                         Anh = p.Anh,
                         TenSanPham = p.TenSanPham,
                         MaSp = p.MaSanPham,

                         Gia = p.Gia.ToString(),
                         SoLuong = soLuong,
                         Tong = Convert.ToDouble(p.Gia.ToString().Trim().Replace(",", string.Empty).Replace(".", string.Empty)) * soLuong
                    };
                    objCart.AddToCart(item);
                    Session["Cart"] = objCart;

               }
               JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
               var result = JsonConvert.SerializeObject("Thêm thành công", Formatting.Indented, jss);
               return this.Json(result, JsonRequestBehavior.AllowGet); ;

          }
          // cập nhật giỏ hàng theo loại sản phẩm và số lượng

          public ActionResult CapNhatSoLuong(string maSp, int soLuong)
          {
               int id = Convert.ToInt32(maSp.Substring(7, maSp.Length - 7));
               GioHang objCart = (GioHang)Session["Cart"];
               if (objCart != null)
               {
                    objCart.CapNhatSoLuong(id, soLuong);
                    Session["Cart"] = objCart;
               }
               return RedirectToAction("index");
          }
          // giỏ hàng mặc định, nếu session = null thì hiện không có hàng trong giỏ, nếu có thì trả lại list các sản phẩm
          [HttpGet]
          public ActionResult Index()
          {
               Entities shop = new Entities();

               var model_1 = shop.DanhMucSanPham.ToList();
               ViewBag.model = model_1;

               ViewBag.Title = "Giỏ hàng";
               GioHangViewModel model = new GioHangViewModel();
               model.GioHang = (GioHang)Session["Cart"];
               return View(model);
          }
          [HttpPost]
          public ActionResult ThanhToan(HoaDon hoadon)
          {
               Entities shop = new Entities();

               var model_1 = shop.DanhMucSanPham.ToList();
               ViewBag.model = model_1;

               GioHangViewModel model = new GioHangViewModel();
               model.GioHang = (GioHang)Session["Cart"];
               int tong = model.GioHang.ListItem.Sum(item => int.Parse(item.Gia) * item.SoLuong);
               
               HoaDon hd = new HoaDon();

               hd.TenKhachHang = hoadon.TenKhachHang;
               hd.DiaChiGiaoHang = hoadon.DiaChiGiaoHang;
               hd.ThoiGianGiaoHang = DateTime.Now;
               hd.Email = hoadon.Email;
               hd.SDT = hoadon.SDT;
               hd.TongTien = tong;
               hd.TrangThai = 0;
               hd.MaHoaDon = 9;
               shop.HoaDon.Add(hd);

               shop.SaveChanges();


               //shop.SaveChanges();

               var hoaDon = (from h in shop.HoaDon orderby h.MaHoaDon descending select h).FirstOrDefault();
               foreach (var item in model.GioHang.ListItem)
               {
                    ChiTietDatHang ct = new ChiTietDatHang();
                    ct.MaHoaDon = hoaDon.MaHoaDon;
                    ct.MaSanPham = item.MaSp;
                    ct.SoLuong = item.SoLuong;
                    ct.TT_ThanhToan = decimal.Parse(item.Tong.ToString(CultureInfo.InvariantCulture));

                    shop.ChiTietDatHang.Add(ct);
                    shop.SaveChanges();
               }
               model.GioHang.ListItem.Clear();
               return View("ThanhToan");
          }

          public ActionResult ThanhToan()
          {
               return View();
          }

          public ActionResult NhapThongTin()
          {
               Entities shop = new Entities();

               var model_1 = shop.DanhMucSanPham.ToList();
               ViewBag.model = model_1;
               
               return View();
          }
     }
}
