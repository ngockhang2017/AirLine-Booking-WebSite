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
    /// Controller Hình thức thanh toán
    /// </summary>
    [RoutePrefix("api/HinhThucThanhToans")]
    public class HinhThucThanhToansController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Hình thức thanh toán
        /// </summary>
        /// <returns></returns>
        // GET: api/HinhThucThanhToans
        public IQueryable<HinhThucThanhToan> GetHinhThucThanhToans()
        {
            return db.HinhThucThanhToans;
        }

        /// <summary>
        /// Lấy Hình thức thanh toán theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/HinhThucThanhToans/5
        [ResponseType(typeof(HinhThucThanhToan))]
        public async Task<IHttpActionResult> GetHinhThucThanhToan(string id)
        {
            HinhThucThanhToan hinhThucThanhToan = await db.HinhThucThanhToans.FindAsync(id);
            if (hinhThucThanhToan == null)
            {
                return NotFound();
            }

            return Ok(hinhThucThanhToan);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Hình thức thanh toán theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hinhThucThanhToan"></param>
        /// <returns></returns>
        // PUT: api/HinhThucThanhToans/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHinhThucThanhToan(string id, HinhThucThanhToan hinhThucThanhToan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hinhThucThanhToan.MaHinhThucTT)
            {
                return BadRequest();
            }

            db.Entry(hinhThucThanhToan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HinhThucThanhToanExists(id))
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
        /// Tạo mới 1 Hình thức thanh toán
        /// </summary>
        /// <param name="hinhThucThanhToan"></param>
        /// <returns></returns>
        // POST: api/HinhThucThanhToans
        [ResponseType(typeof(HinhThucThanhToan))]
        public async Task<IHttpActionResult> PostHinhThucThanhToan(HinhThucThanhToan hinhThucThanhToan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HinhThucThanhToans.Add(hinhThucThanhToan);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HinhThucThanhToanExists(hinhThucThanhToan.MaHinhThucTT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hinhThucThanhToan.MaHinhThucTT }, hinhThucThanhToan);
        }

        /// <summary>
        /// Xóa 1 Hình thức thanh toán theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/HinhThucThanhToans/5
        [ResponseType(typeof(HinhThucThanhToan))]
        public async Task<IHttpActionResult> DeleteHinhThucThanhToan(string id)
        {
            HinhThucThanhToan hinhThucThanhToan = await db.HinhThucThanhToans.FindAsync(id);
            if (hinhThucThanhToan == null)
            {
                return NotFound();
            }

            db.HinhThucThanhToans.Remove(hinhThucThanhToan);
            await db.SaveChangesAsync();

            return Ok(hinhThucThanhToan);
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
        /// Kiểm tra sự tồn tại của Hình thức thanh toán theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HinhThucThanhToanExists(string id)
        {
            return db.HinhThucThanhToans.Count(e => e.MaHinhThucTT == id) > 0;
        }
    }
}