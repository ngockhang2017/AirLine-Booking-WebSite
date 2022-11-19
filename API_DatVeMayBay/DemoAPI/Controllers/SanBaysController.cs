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
    /// Controller Sân bay
    /// </summary>
    [RoutePrefix("api/SanBays")]

    public class SanBaysController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Sân bay
        /// </summary>
        /// <returns></returns>
        // GET: api/SanBays
        public IQueryable<SanBay> GetSanBays()
        {
            return db.SanBays;
        }

        /// <summary>
        /// Lấy thông tin Sân bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/SanBays/5
        [ResponseType(typeof(SanBay))]
        public async Task<IHttpActionResult> GetSanBay(string id)
        {
            SanBay sanBay = await db.SanBays.FindAsync(id);
            if (sanBay == null)
            {
                return NotFound();
            }

            return Ok(sanBay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KhuVuc"></param>
        /// <returns></returns>
        [Route("{MaSanBay}")]
        [ResponseType(typeof(SanBay))]
        public async Task<IHttpActionResult> GetSanBayKV(string MaSanBay)
        {
            var sanBay = await (from sb in db.SanBays                               
                                where sb.MaSanBay == MaSanBay
                                select sb).ToListAsync();
            if (sanBay == null)
            {
                return NotFound();
            }

            return Ok(sanBay);
        }



        /// <summary>
        /// Chỉnh sửa thông tin Sân bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sanBay"></param>
        /// <returns></returns>
        // PUT: api/SanBays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSanBay(string id, SanBay sanBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanBay.MaSanBay)
            {
                return BadRequest();
            }

            db.Entry(sanBay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanBayExists(id))
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
        /// Tạo mới 1 Sân bay
        /// </summary>
        /// <param name="sanBay"></param>
        /// <returns></returns>
        // POST: api/SanBays
        [ResponseType(typeof(SanBay))]
        public async Task<IHttpActionResult> PostSanBay(SanBay sanBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SanBays.Add(sanBay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SanBayExists(sanBay.MaSanBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sanBay.MaSanBay }, sanBay);
        }

        /// <summary>
        /// Xóa 1 Sân bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/SanBays/5
        [ResponseType(typeof(SanBay))]
        public async Task<IHttpActionResult> DeleteSanBay(string id)
        {
            SanBay sanBay = await db.SanBays.FindAsync(id);
            if (sanBay == null)
            {
                return NotFound();
            }

            db.SanBays.Remove(sanBay);
            await db.SaveChangesAsync();

            return Ok(sanBay);
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
        /// Kiểm tra sự tồn tại của Sân bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool SanBayExists(string id)
        {
            return db.SanBays.Count(e => e.MaSanBay == id) > 0;
        }
    }
}