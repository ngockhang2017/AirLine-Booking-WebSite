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
    /// Controller Khách Hàng
    /// </summary>
    [RoutePrefix("api/KhachHangs")]
    public class KhachHangsController : ApiController
    {
        private Entities db = new Entities();
        /// <summary>
        /// Liệt kê danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        // GET: api/KhachHangs
        public IQueryable<KhachHang> GetKhachHangs()
        {
            return db.KhachHangs;
        }

        /// <summary>
        /// Lấy Khách hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/KhachHangs/5
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> GetKhachHang(int id)
        {
            KhachHang khachHang = await db.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return Ok(khachHang);
        }

        /// <summary>
        /// Chỉnh sửa thông tin khách hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="khachHang"></param>
        /// <returns></returns>
        // PUT: api/KhachHangs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachHang.MaKH)
            {
                return BadRequest();
            }

            db.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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
        /// Tạo mới 1 Khách hàng
        /// </summary>
        /// <param name="khachHang"></param>
        /// <returns></returns>
        // POST: api/KhachHangs
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> PostKhachHang(KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KhachHangs.Add(khachHang);
            
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = khachHang.MaKH }, khachHang);
        }

        /// <summary>
        /// Xóa 1 khách hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/KhachHangs/5
        [ResponseType(typeof(KhachHang))]
        public async Task<IHttpActionResult> DeleteKhachHang(int id)
        {
            KhachHang khachHang = await db.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            db.KhachHangs.Remove(khachHang);
            await db.SaveChangesAsync();

            return Ok(khachHang);
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
        /// Kiểm tra sự tồn tại của khách hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool KhachHangExists(int id)
        {
            return db.KhachHangs.Count(e => e.MaKH == id) > 0;
        }
    }
}