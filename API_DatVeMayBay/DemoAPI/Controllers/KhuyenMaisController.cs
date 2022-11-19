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
    /// Controller Khuyến Mại
    /// </summary>
    [RoutePrefix("api/KhuyenMais")]
    public class KhuyenMaisController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Khuyến Mãi hiện có
        /// </summary>
        /// <returns></returns>
        // GET: api/KhuyenMais
        public IQueryable<KhuyenMai> GetKhuyenMais()
        {
            return db.KhuyenMais;
        }

        /// <summary>
        /// Lấy Khuyến Mại theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/KhuyenMais/5
        [ResponseType(typeof(KhuyenMai))]
        public async Task<IHttpActionResult> GetKhuyenMai(int id)
        {
            KhuyenMai khuyenMai = await db.KhuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return Ok(khuyenMai);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Khuyến Mại theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="khuyenMai"></param>
        /// <returns></returns>
        // PUT: api/KhuyenMais/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKhuyenMai(int id, KhuyenMai khuyenMai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khuyenMai.MaKhuyenMai)
            {
                return BadRequest();
            }

            db.Entry(khuyenMai).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhuyenMaiExists(id))
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
        /// Tạo mới 1 khuyến mại
        /// </summary>
        /// <param name="khuyenMai"></param>
        /// <returns></returns>
        // POST: api/KhuyenMais
        [ResponseType(typeof(KhuyenMai))]
        public async Task<IHttpActionResult> PostKhuyenMai(KhuyenMai khuyenMai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KhuyenMais.Add(khuyenMai);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = khuyenMai.MaKhuyenMai }, khuyenMai);
        }

        /// <summary>
        /// Xóa 1 Khuyến Mãi theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/KhuyenMais/5
        [ResponseType(typeof(KhuyenMai))]
        public async Task<IHttpActionResult> DeleteKhuyenMai(int id)
        {
            KhuyenMai khuyenMai = await db.KhuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            db.KhuyenMais.Remove(khuyenMai);
            await db.SaveChangesAsync();

            return Ok(khuyenMai);
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
        /// Kiểm tra sự tồn tại của Khuyến mại theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool KhuyenMaiExists(int id)
        {
            return db.KhuyenMais.Count(e => e.MaKhuyenMai == id) > 0;
        }
    }
}