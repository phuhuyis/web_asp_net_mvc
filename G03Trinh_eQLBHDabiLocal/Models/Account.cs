using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Account
    {
        [StringLength(100, ErrorMessage = "Tài khoản phải từ 5 đến 100 ký tự", MinimumLength = 5)]
        [DisplayName("Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tài khoản không chứa ký tự đặc biệt")]
        public string username { get; set; }

        [StringLength(100, ErrorMessage = "Mật khẩu phải từ 8 đến 100 ký tự", MinimumLength = 8)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        public int? permission { get; set; }
        [StringLength(100, ErrorMessage = "Nhập lại mật khẩu phải từ 8 đến 100 ký tự", MinimumLength = 8)]
        [DisplayName("Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập nhập lại mật khẩu")]
        public String enterPass { get; set; }
    }
}