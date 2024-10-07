using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class BillService
    {
        private EFDbContext context = new EFDbContext();
        private CartService cartService = new CartService();
        public bool pay(String username)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    DateTime booking_date = DateTime.Now;
                    List<cart> carts = context.cart.Where(c => c.customer1.username == username).ToList();
                    bill billAfter = new bill()
                    {
                        booking_date = booking_date,
                        customer = carts[0].customer,
                        status = 0
                    };
                    bill billBefore = context.bill.Add(billAfter);
                    context.SaveChanges();
                    foreach (cart i in carts)
                    {
                        bill_info bill_InfoAfter = new bill_info(){
                            bill = billBefore.id,
                            product = i.product,
                            quantity = i.quantity,
                        };
                        bill_info bill_InfoBefore = context.bill_info.Add(bill_InfoAfter);
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }    
            
        }

        public List<bill> list()
        {
            return context.bill.OrderBy(b => b.status).ToList();
        }

        public List<bill> list(String username)
        {
            return context.bill.Where(b => b.customer1.username == username).OrderBy(b => b.status).ToList();
        }

        public void handle(int id)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                bill bill = context.bill.Where(c => c.id == id).FirstOrDefault();
                bill.status = 2;
                context.bill.AddOrUpdate(bill);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public void progress(int id)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                bill bill = context.bill.Where(c => c.id == id).FirstOrDefault();
                bill.status = 1;
                context.bill.AddOrUpdate(bill);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public void showDetail(int id)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                bill bill = context.bill.Where(c => c.id == id).FirstOrDefault();
                if(bill.status != 2)
                    bill.status = 1;
                context.bill.AddOrUpdate(bill);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public bill get(int id)
        {
            return context.bill.Where(c => c.id == id).FirstOrDefault();
        }

        public List<bill_info> listAllDetails(int id)
        {
            return context.bill_info.Where(b => b.bill == id).ToList();
        }

        public bool add(Bill bill)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    bill entityAfter = new bill()
                    {
                        booking_date = bill.booking_date,
                        customer = bill.customer,
                        status = bill.status,
                        payment = bill.payment,
                        address = bill.address,
                        phone = bill.phone,
                        name = bill.name,
                    };
                    bill entityBefore = context.bill.Add(entityAfter);
                    context.SaveChanges();
                    List<cart> carts = context.cart.Where(c => c.customer == bill.customer).ToList();
                    foreach (cart i in carts)
                    {
                        bill_info bill_InfoAfter = new bill_info()
                        {
                            bill = entityBefore.id,
                            product = i.product,
                            quantity = i.quantity,
                            color = i.color,
                        };
                        bill_info bill_InfoBefore = context.bill_info.Add(bill_InfoAfter);
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    String body = "";
                    string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
                    using (StreamReader reader = new StreamReader(path + "Resources\\web\\mail\\customer.txt"))
                    {
                        body += reader.ReadToEnd();
                        body = body.Replace("@name", bill.name);
                        body = body.Replace("@address", bill.address);
                        body = body.Replace("@phone", bill.phone);
                        body = body.Replace("@date", ((DateTime)bill.booking_date).ToString("dd/MM/yyyy"));
                    }
                    List<bill_info> bill_infoList = context.bill_info.Where(b => b.bill == entityBefore.id).ToList();
                    int index = 1;
                    System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    foreach (var item in bill_infoList)
                    {
                        body += "<tr style=\"margin-top: 20px;\">\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + index;
                        product product = context.product.Where(p => p.id == item.product).FirstOrDefault();
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px; width: 200px;\">" + product.name;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + item.color;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + String.Format(cul, "{0:C0}", product.price);
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + item.quantity;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + String.Format(cul, "{0:C0}", product.price * item.quantity);
                        body += "</td>\r\n                </tr>";
                        index++;
                    }
                    decimal sumPrice = 0;
                    foreach (G03Trinh_eQLBHDabiLocal.Entity.bill_info item in bill_infoList)
                    {
                        product product = context.product.Where(p => p.id == item.product).FirstOrDefault();
                        sumPrice += (decimal)product.price * (decimal)item.quantity;
                    }
                    using (StreamReader reader = new StreamReader(path + "Resources\\web\\mail\\End.txt"))
                    {
                        body += reader.ReadToEnd();
                        body = body.Replace("@sum", String.Format(cul, "{0:C0}", sumPrice));
                    }
                    Utils.MailUtil.sendMail("Đặt hàng thành công", body, context.customer.Where(c => c.id == entityBefore.customer).FirstOrDefault().email);
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool create(Bill bill)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    bill entityAfter = new bill()
                    {
                        booking_date = bill.booking_date,
                        customer = bill.customer,
                        status = bill.status,
                        payment = bill.payment,
                        address = bill.address,
                        phone = bill.phone,
                        name = bill.name,
                    };
                    bill entityBefore = context.bill.Add(entityAfter);
                    context.SaveChanges();
                    bill_info bill_InfoAfter = new bill_info()
                    {
                        bill = entityBefore.id,
                        product = bill.product,
                        quantity = bill.quantity,
                        color = bill.color,
                    };
                    bill_info bill_InfoBefore = context.bill_info.Add(bill_InfoAfter);
                    context.SaveChanges();
                    transaction.Commit();
                    String body = "";
                    string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
                    using (StreamReader reader = new StreamReader(path + "Resources\\web\\mail\\customer.txt"))
                    {
                        body += reader.ReadToEnd();
                        body = body.Replace("@Model[0].bill1.name", bill.name);
                        body = body.Replace("@Model[0].bill1.address", bill.address);
                        body = body.Replace("@Model[0].bill1.phone", bill.phone);
                        body = body.Replace("@Html.Raw(Model[0].bill1.booking_date != null ? ((DateTime)Model[0].bill1.booking_date).ToString(\"dd/MM/yyyy\") : \"\"", ((DateTime)bill.booking_date).ToString("dd/MM/yyyy"));
                    }
                    List<bill_info> bill_infoList = context.bill_info.Where(b => b.bill == entityBefore.id).ToList();
                    int index = 1;
                    System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    foreach (var item in bill_infoList)
                    {
                        body += "<tr style=\"margin-top: 20px;\">\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + index;
                        product product = context.product.Where(p => p.id == item.product).FirstOrDefault();
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px; width: 200px;\">" + product.name;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + item.color;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + String.Format(cul, "{0:C0}", product.price);
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + item.quantity;
                        body += "</td>\r\n                    <td style=\"text-align: center; font-color: 18px;\">" + String.Format(cul, "{0:C0}", product.price * item.quantity);
                        body += "</td>\r\n                </tr>";
                        index++;
                    }
                    decimal sumPrice = 0;
                    foreach (G03Trinh_eQLBHDabiLocal.Entity.bill_info item in bill_infoList)
                    {
                        product product = context.product.Where(p => p.id == item.product).FirstOrDefault();
                        sumPrice += (decimal)product.price * (decimal)item.quantity;
                    }
                    using (StreamReader reader = new StreamReader(path + "Resources\\web\\mail\\End.txt"))
                    {
                        body += reader.ReadToEnd();
                        body = body.Replace("@sum", String.Format(cul, "{0:C0}", sumPrice));
                    }
                    Utils.MailUtil.sendMail("Đặt hàng thành công", body, context.customer.Where(c => c.id == entityBefore.customer).FirstOrDefault().email);
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}