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
    /// Controller Chặng bay
    /// </summary>
    [RoutePrefix("api/ChangBays")]
    public class ChangBaysController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các chặng bay chi tiết
        /// </summary>
        /// <returns></returns>
        // GET: api/ChangBays
        public IQueryable<ChangBay> GetChangBays()
        {
            return db.ChangBays;
        }

        /// <summary>
        /// Lấy thông tin Chặng bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ChangBays/5
        [ResponseType(typeof(ChangBay))]
        public async Task<IHttpActionResult> GetChangBay(int id)
        {
            ChangBay changBay = await db.ChangBays.FindAsync(id);
            if (changBay == null)
            {
                return NotFound();
            }

            return Ok(changBay);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Chặng bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changBay"></param>
        /// <returns></returns>
        // PUT: api/ChangBays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChangBay(int id, ChangBay changBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != changBay.MaChangBay)
            {
                return BadRequest();
            }

            db.Entry(changBay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChangBayExists(id))
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
        /// Tạo mới 1 Chặng bay
        /// </summary>
        /// <param name="changBay"></param>
        /// <returns></returns>
        // POST: api/ChangBays
        [ResponseType(typeof(ChangBay))]
        public async Task<IHttpActionResult> PostChangBay(ChangBay changBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChangBays.Add(changBay);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = changBay.MaChangBay }, changBay);
        }

        /// <summary>
        /// Xóa 1 Chặng bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ChangBays/5
        [ResponseType(typeof(ChangBay))]
        public async Task<IHttpActionResult> DeleteChangBay(int id)
        {
            ChangBay changBay = await db.ChangBays.FindAsync(id);
            if (changBay == null)
            {
                return NotFound();
            }

            db.ChangBays.Remove(changBay);
            await db.SaveChangesAsync();

            return Ok(changBay);
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
        /// Kiểm tra sự tồn tại của Chặng bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ChangBayExists(int id)
        {
            return db.ChangBays.Count(e => e.MaChangBay == id) > 0;
        }
    }
}