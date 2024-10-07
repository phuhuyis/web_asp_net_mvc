using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Bill
    {
        public int id { get; set; }
        public int customer { get; set; }
        public DateTime? booking_date { get; set; }

        public int? status { get; set; }
        public int product { get; set; }
        public int? quantity { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int? payment { get; set; }
        public int? color { get; set; }
    }
}