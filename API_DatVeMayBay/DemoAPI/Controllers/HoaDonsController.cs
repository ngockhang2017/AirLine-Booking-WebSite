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
    /// Controller Hóa Đơn chi tiết
    /// </summary>
    [RoutePrefix("api/HoaDons")]
    public class HoaDonsController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Hóa đơn chi tiết đã có
        /// </summary>
        /// <returns></returns>
        // GET: api/HoaDons
        public IQueryable<HoaDon> GetHoaDons()
        {
            return db.HoaDons;
        }

        /// <summary>
        /// Lấy Hóa đơn theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/HoaDons/5
        [ResponseType(typeof(HoaDon))]
        public async Task<IHttpActionResult> GetHoaDon(int id)
        {
            HoaDon hoaDon = await db.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return Ok(hoaDon);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Hóa đơn chi tiết theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hoaDon"></param>
        /// <returns></returns>
        // PUT: api/HoaDons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHoaDon(int id, HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hoaDon.MaHoaDon)
            {
                return BadRequest();
            }

            db.Entry(hoaDon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonExists(id))
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
        /// Tạo mới 1 Hóa đơn
        /// </summary>
        /// <param name="hoaDon"></param>
        /// <returns></returns>
        // POST: api/HoaDons
        [ResponseType(typeof(HoaDon))]
        public async Task<IHttpActionResult> PostHoaDon(HoaDon hoaDon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HoaDons.Add(hoaDon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hoaDon.MaHoaDon }, hoaDon);
        }

        /// <summary>
        /// Xóa 1 Hóa đơn chi tiết theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/HoaDons/5
        [ResponseType(typeof(HoaDon))]
        public async Task<IHttpActionResult> DeleteHoaDon(int id)
        {
            HoaDon hoaDon = await db.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            db.HoaDons.Remove(hoaDon);
            await db.SaveChangesAsync();

            return Ok(hoaDon);
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
        /// Kiểm tra sự tồn tại của Hóa đơn theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HoaDonExists(int id)
        {
            return db.HoaDons.Count(e => e.MaHoaDon == id) > 0;
        }
    }
}