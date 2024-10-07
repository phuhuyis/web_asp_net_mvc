using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class BillInfo
    {
        public int id { get; set; }
        public int bill { get; set; }
        public int product { get; set; }

        public int? quantity { get; set; }
        public int? color { get; set; }
    }
}