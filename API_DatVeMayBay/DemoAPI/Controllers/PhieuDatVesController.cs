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
    /// Controller Phiếu đặt vé
    /// </summary>
    [RoutePrefix("api/PhieuDatVes")]

    public class PhieuDatVesController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Phiếu đã đặt
        /// </summary>
        /// <returns></returns>
        // GET: api/PhieuDatVes
        public IQueryable<PhieuDatVe> GetPhieuDatVes()
        {
            return db.PhieuDatVes;
        }

        /// <summary>
        /// Lấy Thông tin Phiếu vé đã đặt theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/PhieuDatVes/5
        [ResponseType(typeof(PhieuDatVe))]
        public async Task<IHttpActionResult> GetPhieuDatVe(int id)
        {
            PhieuDatVe phieuDatVe = await db.PhieuDatVes.FindAsync(id);
            if (phieuDatVe == null)
            {
                return NotFound();
            }

            return Ok(phieuDatVe);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Phiếu vé đã đặt theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phieuDatVe"></param>
        /// <returns></returns>
        // PUT: api/PhieuDatVes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhieuDatVe(int id, PhieuDatVe phieuDatVe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phieuDatVe.MaPhieuDatVe)
            {
                return BadRequest();
            }

            db.Entry(phieuDatVe).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhieuDatVeExists(id))
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
        /// Tạo mới 1 Phiếu Vé đặt
        /// </summary>
        /// <param name="phieuDatVe"></param>
        /// <returns></returns>
        // POST: api/PhieuDatVes
        [ResponseType(typeof(PhieuDatVe))]
        public async Task<IHttpActionResult> PostPhieuDatVe(PhieuDatVe phieuDatVe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhieuDatVes.Add(phieuDatVe);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = phieuDatVe.MaPhieuDatVe }, phieuDatVe);
        }

        /// <summary>
        /// Xóa 1 Phiếu vé đã đặt theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/PhieuDatVes/5
        [ResponseType(typeof(PhieuDatVe))]
        public async Task<IHttpActionResult> DeletePhieuDatVe(int id)
        {
            PhieuDatVe phieuDatVe = await db.PhieuDatVes.FindAsync(id);
            if (phieuDatVe == null)
            {
                return NotFound();
            }

            db.PhieuDatVes.Remove(phieuDatVe);
            await db.SaveChangesAsync();

            return Ok(phieuDatVe);
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
        /// Kiểm tra sự tồn tại của Phiếu vé đã đặt theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PhieuDatVeExists(int id)
        {
            return db.PhieuDatVes.Count(e => e.MaPhieuDatVe == id) > 0;
        }
    }
}