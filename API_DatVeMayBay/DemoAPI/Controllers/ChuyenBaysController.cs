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
    /// Controller Chuyến Bay
    /// </summary>
    [RoutePrefix("api/ChuyenBays")]
    public class ChuyenBaysController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// Liệt kê danh sách các Chuyến bay hiện có
        /// </summary>
        /// <returns></returns>
        // GET: api/ChuyenBays
        public IQueryable<ChuyenBay> GetChuyenBays()
        {
            return db.ChuyenBays;
        }

        /// <summary>
        /// Lấy thông tin các Chuyến bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ChuyenBays/5
        [ResponseType(typeof(ChuyenBay))]
        public async Task<IHttpActionResult> GetChuyenBay(int id)
        {
            ChuyenBay chuyenBay = await db.ChuyenBays.FindAsync(id);
            if (chuyenBay == null)
            {
                return NotFound();
            }

            return Ok(chuyenBay);
        }

        /// <summary>
        /// Lấy danh sách Chuyến bay tìm kiếm theo Ngày cất cánh, Sân bay cất cánh, Sân bay hạ cánh
        /// </summary>
        /// <param name="ngaycatcanh"></param>
        /// <param name="catcanh"></param>
        /// <param name="hacanh"></param>
        /// <returns></returns>
        [Route("{catcanh}/{hacanh}/{ngaycatcanh}")]
        [ResponseType(typeof(ChuyenBay))]
        public async Task<IHttpActionResult> GetChuyenBayByNgay(DateTime ngaycatcanh, string catcanh, string hacanh)
        {
            var chuyenbays = await (from chuyenb in db.ChuyenBays
                                   join changb in db.ChangBays on chuyenb.MaChangBay equals changb.MaChangBay
                                   where changb.SanBay_CatCanh == catcanh && changb.SanBay_HaCanh == hacanh && chuyenb.NgayCatCanh == ngaycatcanh
                                   select chuyenb).ToListAsync();
            return Ok(chuyenbays);
        }

        ///
        [Route("{catcanh}/{hacanh}")]
        [ResponseType(typeof(ChuyenBay))]
        public async Task<IHttpActionResult> GetChuyenBayBySanBay(string catcanh, string hacanh)
        {
            var chuyenbays = await (from chuyenb in db.ChuyenBays
                                    join changb in db.ChangBays on chuyenb.MaChangBay equals changb.MaChangBay
                                    where changb.SanBay_CatCanh == catcanh && changb.SanBay_HaCanh == hacanh
                                    select chuyenb).ToListAsync();
            return Ok(chuyenbays);
        }

        /// <summary>
        /// Chỉnh sửa thông tin Chuyến bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chuyenBay"></param>
        /// <returns></returns>
        // PUT: api/ChuyenBays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChuyenBay(int id, ChuyenBay chuyenBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chuyenBay.MaChuyenBay)
            {
                return BadRequest();
            }

            db.Entry(chuyenBay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuyenBayExists(id))
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
        /// Tạo mới 1 Chuyến bay
        /// </summary>
        /// <param name="chuyenBay"></param>
        /// <returns></returns>
        // POST: api/ChuyenBays
        [ResponseType(typeof(ChuyenBay))]
        public async Task<IHttpActionResult> PostChuyenBay(ChuyenBay chuyenBay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChuyenBays.Add(chuyenBay);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = chuyenBay.MaChuyenBay }, chuyenBay);
        }

        /// <summary>
        /// Xóa 1 Chuyến bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ChuyenBays/5
        [ResponseType(typeof(ChuyenBay))]
        public async Task<IHttpActionResult> DeleteChuyenBay(int id)
        {
            ChuyenBay chuyenBay = await db.ChuyenBays.FindAsync(id);
            if (chuyenBay == null)
            {
                return NotFound();
            }

            db.ChuyenBays.Remove(chuyenBay);
            await db.SaveChangesAsync();

            return Ok(chuyenBay);
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
        /// Kiểm tra sự tồn tại của Chuyến bay theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ChuyenBayExists(int id)
        {
            return db.ChuyenBays.Count(e => e.MaChuyenBay == id) > 0;
        }
    }
}