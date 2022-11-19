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
    /// Controller Ngân Hàng
    /// </summary>
    [RoutePrefix("api/NganHangs")]

    public class NganHangsController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Ngân Hàng
        /// </summary>
        /// <returns></returns>
        // GET: api/NganHangs
        public IQueryable<NganHang> GetNganHangs()
        {
            return db.NganHangs;
        }

        /// <summary>
        /// Lấy Ngân Hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/NganHangs/5
        [ResponseType(typeof(NganHang))]
        public async Task<IHttpActionResult> GetNganHang(string id)
        {
            NganHang nganHang = await db.NganHangs.FindAsync(id);
            if (nganHang == null)
            {
                return NotFound();
            }

            return Ok(nganHang);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Ngân hàng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nganHang"></param>
        /// <returns></returns>
        // PUT: api/NganHangs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNganHang(string id, NganHang nganHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nganHang.MaNganHang)
            {
                return BadRequest();
            }

            db.Entry(nganHang).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NganHangExists(id))
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
        /// Tạo mới 1 Ngân hàng liên kết
        /// </summary>
        /// <param name="nganHang"></param>
        /// <returns></returns>
        // POST: api/NganHangs
        [ResponseType(typeof(NganHang))]
        public async Task<IHttpActionResult> PostNganHang(NganHang nganHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NganHangs.Add(nganHang);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NganHangExists(nganHang.MaNganHang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nganHang.MaNganHang }, nganHang);
        }

        /// <summary>
        /// Xóa 1 Ngân hàng liên kết theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/NganHangs/5
        [ResponseType(typeof(NganHang))]
        public async Task<IHttpActionResult> DeleteNganHang(string id)
        {
            NganHang nganHang = await db.NganHangs.FindAsync(id);
            if (nganHang == null)
            {
                return NotFound();
            }

            db.NganHangs.Remove(nganHang);
            await db.SaveChangesAsync();

            return Ok(nganHang);
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
        /// Kiểm tra sự tồn tại của Ngân hàng liên kết theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool NganHangExists(string id)
        {
            return db.NganHangs.Count(e => e.MaNganHang == id) > 0;
        }
    }
}