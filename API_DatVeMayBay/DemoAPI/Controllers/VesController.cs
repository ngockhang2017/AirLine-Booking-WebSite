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
    /// Controller Vé: Số ghế, Khoang ghế.
    /// </summary>
    [RoutePrefix("api/Ves")]

    public class VesController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Vé
        /// </summary>
        /// <returns></returns>
        // GET: api/Ves
        public IQueryable<Ve> GetVes()
        {
            return db.Ves;
        }

        /// <summary>
        /// Lấy thông tin Vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Ves/5
        [ResponseType(typeof(Ve))]
        public async Task<IHttpActionResult> GetVe(int id)
        {
            Ve ve = await db.Ves.FindAsync(id);
            if (ve == null)
            {
                return NotFound();
            }

            return Ok(ve);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ve"></param>
        /// <returns></returns>
        // PUT: api/Ves/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVe(int id, Ve ve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ve.MaVe)
            {
                return BadRequest();
            }

            db.Entry(ve).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeExists(id))
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
        /// Tạo mới 1 Vé
        /// </summary>
        /// <param name="ve"></param>
        /// <returns></returns>
        // POST: api/Ves
        [ResponseType(typeof(Ve))]
        public async Task<IHttpActionResult> PostVe(Ve ve)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ves.Add(ve);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ve.MaVe }, ve);
        }

        /// <summary>
        /// Xóa 1 Vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Ves/5
        [ResponseType(typeof(Ve))]
        public async Task<IHttpActionResult> DeleteVe(int id)
        {
            Ve ve = await db.Ves.FindAsync(id);
            if (ve == null)
            {
                return NotFound();
            }

            db.Ves.Remove(ve);
            await db.SaveChangesAsync();

            return Ok(ve);
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
        /// Kiểm tra sự tồn tại của Vé theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool VeExists(int id)
        {
            return db.Ves.Count(e => e.MaVe == id) > 0;
        }
    }
}