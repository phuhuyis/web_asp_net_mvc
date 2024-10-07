using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class DataReport
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String label { get; set; }
        public int data { get; set; }
        public String color { get; set; }
    }
}