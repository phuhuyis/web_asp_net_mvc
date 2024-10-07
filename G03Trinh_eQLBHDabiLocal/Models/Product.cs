using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace G03Trinh_eQLBHDabiLocal.Models
{
    public class Product
    {
        public int id { get; set; }

        [StringLength(100, ErrorMessage = "Tên túi sách chỉ được chứa tối đa 100 ký tự")]
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên túi sách")]
        public string name { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string description { get; set; }
        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        public decimal? price { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh")]
        public HttpPostedFileBase image { get; set; }
        [DisplayName("Hãng")]
        [Required(ErrorMessage = "Vui lòng chọn hãng")]
        public int category { get; set; }
        public String url_img { get; set; }
        [DisplayName("Thay đổi đường dẫn hình ảnh")]
        public bool change_img { get; set; }
    }
}