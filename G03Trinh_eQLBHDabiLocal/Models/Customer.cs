using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Customer
    {
        public int id { get; set; }

        [StringLength(100, ErrorMessage = "Tên khách hàng chỉ được chứa tối đa 100 ký tự")]
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string address { get; set; }

        [StringLength(10, ErrorMessage = "Số điện thoại phải có 10 số")]
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression("^(0)(\\s|\\.)?((3[2-9])|(5[689])|(7[06-9])|(8[1-689])|(9[0-46-9]))(\\d)(\\s|\\.)?(\\d{3})(\\s|\\.)?(\\d{3})$",
            ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone { get; set; }
        [StringLength(100, ErrorMessage = "Email chỉ được chứa tối đa 100 ký tự")]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email không đúng định dạng")]
        public string email { get; set; }

        [StringLength(100, ErrorMessage = "Tài khoản phải từ 5 đến 100 ký tự", MinimumLength = 5)]
        [DisplayName("Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Tài khoản không chứa ký tự đặc biệt")]
        public string username { get; set; }

        [StringLength(100, ErrorMessage = "Mật khẩu phải từ 8 đến 100 ký tự", MinimumLength = 8)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        [StringLength(100, ErrorMessage = "Nhập lại mật khẩu phải từ 8 đến 100 ký tự", MinimumLength = 8)]
        [DisplayName("Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập nhập lại mật khẩu")]
        public string enterPassword { get; set; }
    }
}