using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using System.ComponentModel;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Slide
    {
        public int id { get; set; }
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh")]
        public string url { get; set; }
        [DisplayName("Vị trí")]
        [Required(ErrorMessage = "Vui lòng nhập vị trí")]
        public int position { get; set; }
        public string username { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}