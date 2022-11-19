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
    /// Controller Hành Lý hỗ trợ
    /// </summary>
    [RoutePrefix("api/HanhLies")]
    public class HanhLiesController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các loại hành lý được hỗ trợ
        /// </summary>
        /// <returns></returns>
        // GET: api/HanhLies
        public IQueryable<HanhLy> GetHanhLies()
        {
            return db.HanhLies;
        }

        /// <summary>
        /// Lấy Thông tin Hành lý theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/HanhLies/5
        [ResponseType(typeof(HanhLy))]
        public async Task<IHttpActionResult> GetHanhLy(string id)
        {
            HanhLy hanhLy = await db.HanhLies.FindAsync(id);
            if (hanhLy == null)
            {
                return NotFound();
            }

            return Ok(hanhLy);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Hành lý theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hanhLy"></param>
        /// <returns></returns>
        // PUT: api/HanhLies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHanhLy(string id, HanhLy hanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hanhLy.MaHanhLy)
            {
                return BadRequest();
            }

            db.Entry(hanhLy).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HanhLyExists(id))
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
        /// Tạo mới 1 hành lý
        /// </summary>
        /// <param name="hanhLy"></param>
        /// <returns></returns>
        // POST: api/HanhLies
        [ResponseType(typeof(HanhLy))]
        public async Task<IHttpActionResult> PostHanhLy(HanhLy hanhLy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HanhLies.Add(hanhLy);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HanhLyExists(hanhLy.MaHanhLy))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hanhLy.MaHanhLy }, hanhLy);
        }

        /// <summary>
        /// Xóa 1 Hành lý theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/HanhLies/5
        [ResponseType(typeof(HanhLy))]
        public async Task<IHttpActionResult> DeleteHanhLy(string id)
        {
            HanhLy hanhLy = await db.HanhLies.FindAsync(id);
            if (hanhLy == null)
            {
                return NotFound();
            }

            db.HanhLies.Remove(hanhLy);
            await db.SaveChangesAsync();

            return Ok(hanhLy);
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
        /// Kiểm tra sự tồn tại của hành lý theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool HanhLyExists(string id)
        {
            return db.HanhLies.Count(e => e.MaHanhLy == id) > 0;
        }
    }
}