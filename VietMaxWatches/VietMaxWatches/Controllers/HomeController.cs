using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using VietMaxWatches.Models;

namespace VietMaxWatches.Controllers
{
     public class HomeController : Controller
     {
          public ActionResult Index()
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var thuonghieu = shop.ThuongHieuSanPham.ToList();
               ViewBag.list_ThuongHieu = thuonghieu;

               var sanpham = shop.SanPham.ToList();
               ViewBag.list_SanPham = sanpham;

               var tintuc = shop.NguonTinTuc.ToList().Skip(0).Take(3).ToList();
               ViewBag.tintuc = tintuc;
               
               return View(model);
          }



          public ActionResult DanhSachThuongHieu()
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var model_1 = shop.ThuongHieuSanPham.OrderBy(h => h.MaThuongHieu).ToList();

               return View(model_1);
          }
          public ActionResult DanhSachDongHo(string id)
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var thuonghieu = shop.ThuongHieuSanPham.FirstOrDefault(s => s.MaThuongHieu == id);
               ViewBag.thuong_hieu = thuonghieu.TenThuongHieu;

               var sanpham = shop.SanPham.Where(s => s.MaThuongHieu == id).ToList();
               ViewBag.list_SanPham = sanpham;

               return View();
          }

          public ActionResult ChiTietSanPham(int id)
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;
               
               var sanpham = shop.SanPham.FirstOrDefault(s => s.MaSanPham == id);
               ViewBag.SanPham = sanpham;

               var thuonghieu = shop.ThuongHieuSanPham.FirstOrDefault(s => s.MaThuongHieu == sanpham.MaThuongHieu);
               ViewBag.ThuongHieu = thuonghieu;

               var list_sanpham = shop.SanPham.Where(s => s.MaThuongHieu == sanpham.MaThuongHieu).ToList();
               ViewBag.list_sanpham = list_sanpham;

               return View();
          }
          
          public ActionResult DanhMucDongHo(int id)
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var danhmuc = shop.DanhMucSanPham.FirstOrDefault(s => s.MaDanhMuc == id);
               ViewBag.DanhMuc = danhmuc;

               var sanpham = shop.SanPham.Where(s => s.MaDanhMuc == id).ToList();
               ViewBag.SanPham = sanpham;

               return View();
          }
          public ActionResult LienHe()
          {
               Entities shop = new Entities();
               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;
               return View();
          }
          public ActionResult TinTuc()
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var list_TinTuc = shop.NguonTinTuc.ToList();
               ViewBag.list_tintuc = list_TinTuc;

               return View();
          }

          public ActionResult ChiTietTinTuc(int id)
          {
               Entities shop = new Entities();

               var model = shop.DanhMucSanPham.ToList();
               ViewBag.model = model;

               var TinTuc = shop.NguonTinTuc.FirstOrDefault(s => s.MaNguonTin == id);
               ViewBag.tintuc = TinTuc;

               return View();
          }

          public ActionResult TimKiem(string search, int? page)
          {
               Entities shop = new Entities();

               var model_1 = shop.DanhMucSanPham.ToList();
               ViewBag.model = model_1;

               ViewBag.KQ = search;
               var model = DanhSachTimKiem(search, page);
               return View(model);
          }

          public IPagedList<SanPham> DanhSachTimKiem(string search, int? page)
          {
               Entities shop = new Entities();
               var model_1 = shop.DanhMucSanPham.ToList();
               ViewBag.model = model_1;

               var model = shop.SanPham.Where(s => s.TenSanPham.ToLower().Contains(search.ToLower())).OrderByDescending(c => c.MaSanPham);
               int pageSize = 10;
               int pageNumber = (page ?? 1);
               model.ToPagedList(pageNumber, pageSize);
               return model.ToPagedList(pageNumber, pageSize);
          }
     }
}