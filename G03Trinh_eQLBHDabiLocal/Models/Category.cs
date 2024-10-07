using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Category
    {
        public int id { get; set; }

        [StringLength(100, ErrorMessage = "Tên hãng chỉ được chứa tối đa 100 ký tự")]
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên hãng")]
        public string name { get; set; }

        [StringLength(100, ErrorMessage = "Đường dẫn chỉ được chứa tối đa 100 ký tự")]
        [DisplayName("Đường dẫn")]
        [Required(ErrorMessage = "Vui lòng nhập đường dẫn")]
        [RegularExpression("^[-a-zA-Z0-9_]*[-a-zA-Z0-9_]$", ErrorMessage = "Đường dẫn không hợp lệ, chỉ được phép chứa số, chữ, dấu gạch ngang và dấu gạch dưới")]
        public string metatitle { get; set; }
    }
}