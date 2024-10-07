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
    public class Contact
    {
        [StringLength(1000)]
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }

        public int? status { get; set; }
    }
}