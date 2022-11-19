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
    /// Controller Máy bay
    /// </summary>
    [RoutePrefix("api/MayBays")]

    public class MayBaysController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Máy bay
        /// </summary>
        /// <returns></returns>
        // GET: api/MayBays
        public IQueryable<MayBay> GetMayBays()
        {
            return db.MayBays;
        }

        /// <summary>
        /// Lấy Máy bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/MayBays/5
        [ResponseType(typeof(MayBay))]
        public async Task<IHttpActionResult> GetMayBay(string id)
        {
            MayBay mayBay = await db.MayBays.FindAsync(id);
            if (mayBay == null)
            {
                return NotFound();
            }

            return Ok(mayBay);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Máy bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mayBay"></param>
        /// <returns></returns>
        // PUT: api/MayBays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMayBay(string id, MayBay mayBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mayBay.LoaiMayBay)
            {
                return BadRequest();
            }

            db.Entry(mayBay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MayBayExists(id))
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
        /// Tạo mới 1 Máy bay
        /// </summary>
        /// <param name="mayBay"></param>
        /// <returns></returns>
        // POST: api/MayBays
        [ResponseType(typeof(MayBay))]
        public async Task<IHttpActionResult> PostMayBay(MayBay mayBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MayBays.Add(mayBay);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MayBayExists(mayBay.LoaiMayBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mayBay.LoaiMayBay }, mayBay);
        }

        /// <summary>
        /// Xóa 1 Máy bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/MayBays/5
        [ResponseType(typeof(MayBay))]
        public async Task<IHttpActionResult> DeleteMayBay(string id)
        {
            MayBay mayBay = await db.MayBays.FindAsync(id);
            if (mayBay == null)
            {
                return NotFound();
            }

            db.MayBays.Remove(mayBay);
            await db.SaveChangesAsync();

            return Ok(mayBay);
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
        /// Kiểm tra sự tồn tại của Máy bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MayBayExists(string id)
        {
            return db.MayBays.Count(e => e.LoaiMayBay == id) > 0;
        }
    }
}