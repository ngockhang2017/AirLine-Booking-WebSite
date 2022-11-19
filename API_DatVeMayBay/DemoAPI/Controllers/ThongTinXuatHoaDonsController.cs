using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DemoAPI.Models;

namespace DemoAPI.Controllers
{
    /// <summary>
    /// Controller Hóa Đơn Xuất
    /// </summary>
    [RoutePrefix("api/ThongTinXuatHoaDons")]

    public class ThongTinXuatHoaDonsController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Hóa đơn đã xuất
        /// </summary>
        /// <returns></returns>
        // GET: api/ThongTinXuatHoaDons
        public IQueryable<ThongTinXuatHoaDon> GetThongTinXuatHoaDons()
        {
            return db.ThongTinXuatHoaDons;
        }

        /// <summary>
        /// Lấy Thông tin hóa đơn xuất theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ThongTinXuatHoaDons/5
        [ResponseType(typeof(ThongTinXuatHoaDon))]
        public async Task<IHttpActionResult> GetThongTinXuatHoaDon(int id)
        {
            ThongTinXuatHoaDon thongTinXuatHoaDon = await db.ThongTinXuatHoaDons.FindAsync(id);
            if (thongTinXuatHoaDon == null)
            {
                return NotFound();
            }

            return Ok(thongTinXuatHoaDon);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Hóa đơn Xuất theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="thongTinXuatHoaDon"></param>
        /// <returns></returns>
        // PUT: api/ThongTinXuatHoaDons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutThongTinXuatHoaDon(int id, ThongTinXuatHoaDon thongTinXuatHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thongTinXuatHoaDon.MaTTXuat)
            {
                return BadRequest();
            }

            db.Entry(thongTinXuatHoaDon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongTinXuatHoaDonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Tạo mới 1 Hóa đơn Xuất
        /// </summary>
        /// <param name="thongTinXuatHoaDon"></param>
        /// <returns></returns>
        // POST: api/ThongTinXuatHoaDons
        [ResponseType(typeof(ThongTinXuatHoaDon))]
        public async Task<IHttpActionResult> PostThongTinXuatHoaDon(ThongTinXuatHoaDon thongTinXuatHoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThongTinXuatHoaDons.Add(thongTinXuatHoaDon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = thongTinXuatHoaDon.MaTTXuat }, thongTinXuatHoaDon);
        }

        /// <summary>
        /// Xóa 1 Hóa đơn Xuất theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ThongTinXuatHoaDons/5
        [ResponseType(typeof(ThongTinXuatHoaDon))]
        public async Task<IHttpActionResult> DeleteThongTinXuatHoaDon(int id)
        {
            ThongTinXuatHoaDon thongTinXuatHoaDon = await db.ThongTinXuatHoaDons.FindAsync(id);
            if (thongTinXuatHoaDon == null)
            {
                return NotFound();
            }

            db.ThongTinXuatHoaDons.Remove(thongTinXuatHoaDon);
            await db.SaveChangesAsync();

            return Ok(thongTinXuatHoaDon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của Hóa đơn xuất theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ThongTinXuatHoaDonExists(int id)
        {
            return db.ThongTinXuatHoaDons.Count(e => e.MaTTXuat == id) > 0;
        }
    }
}