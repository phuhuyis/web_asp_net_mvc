using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G03Trinh_eQLBHDabiLocal.Common
{
    public class Constant
    {
        public const String userSession = "userSession";
        public const String websiteName = "Website bán túi sách thể thao";
        public static List<SelectListItem> statusContact = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "0", Text = "Đang chờ xử lý" },
            new SelectListItem() { Value = "1", Text = "Đã xử lý", Selected = true },
        };
        public static List<SelectListItem> statusBill = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "0", Text = "Đang chờ xử lý" },
            new SelectListItem() { Value = "1", Text = "Đang xử lý", Selected = true },
            new SelectListItem() { Value = "2", Text = "Đã giao hàng", Selected = true },
        };
        public static List<SelectListItem> colors = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "35", Text = "Trắng", Selected = true },
            new SelectListItem() { Value = "36", Text = "Hồng" },
            new SelectListItem() { Value = "37", Text = "Tím" },
            new SelectListItem() { Value = "38", Text = "Xanh" },
            new SelectListItem() { Value = "39", Text = "Đen" }
        };
        public static List<SelectListItem> payments = new List<SelectListItem>()
        {
            new SelectListItem() { Value = "0", Text = "Thanh toán khi nhận hàng" },
            new SelectListItem() { Value = "1", Text = "Chuyển khoản" },
        };
    }
}