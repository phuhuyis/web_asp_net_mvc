using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int customer { get; set; }
        public int product { get; set; }

        public int? quantity { get; set; }
        public int? color { get; set; }
    }
}