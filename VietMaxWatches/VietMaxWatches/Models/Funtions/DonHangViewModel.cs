using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VietMaxWatches.Models
{
     public class DonHangViewModel
     {
          public List<HoaDon> DonHangChuaDuyet { get; set; }
          public List<HoaDon> DonHangDaDuyet { get; set; }
     }
}