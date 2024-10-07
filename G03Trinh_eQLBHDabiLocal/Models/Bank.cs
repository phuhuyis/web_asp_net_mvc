using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Bank
    {
        public int id { get; set; }
        [DisplayName("Ngân hàng")]
        [Required(ErrorMessage = "Vui lòng chọn ngân hàng")]
        public string idBank { get; set; }
        [DisplayName("Số tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập số tài khoản")]
        public string number { get; set; }
        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string name { get; set; }
        [DisplayName("Nội dung chuyển khoản")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung chuyển khoản")]
        public string content { get; set; }
    }
}