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
    /// Controller Loại vé: Vé Khứ hồi + Vé Một chiều
    /// </summary>
    [RoutePrefix("api/LoaiVes")]

    public class LoaiVesController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các loại vé
        /// </summary>
        /// <returns></returns>
        // GET: api/LoaiVes
        public IQueryable<LoaiVe> GetLoaiVes()
        {
            return db.LoaiVes;
        }

        /// <summary>
        /// Lấy Loại vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/LoaiVes/5
        [ResponseType(typeof(LoaiVe))]
        public async Task<IHttpActionResult> GetLoaiVe(string id)
        {
            LoaiVe loaiVe = await db.LoaiVes.FindAsync(id);
            if (loaiVe == null)
            {
                return NotFound();
            }

            return Ok(loaiVe);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Loại vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loaiVe"></param>
        /// <returns></returns>
        // PUT: api/LoaiVes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoaiVe(string id, LoaiVe loaiVe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loaiVe.MaLoaiVe)
            {
                return BadRequest();
            }

            db.Entry(loaiVe).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiVeExists(id))
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
        /// Tạo mới 1 loại vé
        /// </summary>
        /// <param name="loaiVe"></param>
        /// <returns></returns>
        // POST: api/LoaiVes
        [ResponseType(typeof(LoaiVe))]
        public async Task<IHttpActionResult> PostLoaiVe(LoaiVe loaiVe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoaiVes.Add(loaiVe);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoaiVeExists(loaiVe.MaLoaiVe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loaiVe.MaLoaiVe }, loaiVe);
        }

        /// <summary>
        /// Xóa 1 Loại vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/LoaiVes/5
        [ResponseType(typeof(LoaiVe))]
        public async Task<IHttpActionResult> DeleteLoaiVe(string id)
        {
            LoaiVe loaiVe = await db.LoaiVes.FindAsync(id);
            if (loaiVe == null)
            {
                return NotFound();
            }

            db.LoaiVes.Remove(loaiVe);
            await db.SaveChangesAsync();

            return Ok(loaiVe);
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
        /// Kiểm tra sự tồn tại của Loại vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LoaiVeExists(string id)
        {
            return db.LoaiVes.Count(e => e.MaLoaiVe == id) > 0;
        }
    }
}