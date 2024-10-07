using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class DashboardService
    {
        private EFDbContext context = new EFDbContext();
        public int countBill()
        {
            return context.bill.Count();
        }
        public int countBillProgress()
        {
            return context.bill.Where(b => b.status == 0).Count();
        }
        public int countCustomer()
        {
            return context.customer.Count();
        }
        public decimal revenueMonthNew()
        {
            List<bill> bills = context
                .bill
                .Where(b => b.status == 2)
                .ToList();
            decimal revenue = 0;
            foreach (bill b in bills)
            {
                if(b.booking_date != null && ((DateTime)b.booking_date).Month == DateTime.Now.Month)
                {
                    List<bill_info> bill_Infos = b.bill_info.ToList();
                    foreach(bill_info info in bill_Infos)
                    {
                        revenue += (decimal)info.product1.price * (decimal)info.quantity;
                    }
                }
            }
            return revenue;
        }

        public decimal revenueMonthOld()
        {
            List<bill> bills = context
                .bill
                .Where(b => b.status == 2)
                .ToList();
            decimal revenue = 0;
            foreach (bill b in bills)
            {
                if (b.booking_date != null)
                {
                    if(DateTime.Now.Month == 1)
                    {
                        if(((DateTime)b.booking_date).Month == 12 && ((DateTime)b.booking_date).Year == DateTime.Now.Year - 1)
                        {
                            List<bill_info> bill_Infos = b.bill_info.ToList();
                            foreach (bill_info info in bill_Infos)
                            {
                                revenue += (decimal)info.product1.price * (decimal)info.quantity;
                            }
                        }    
                    }
                    else
                    {
                        if(((DateTime)b.booking_date).Month == DateTime.Now.Month - 1)
                        {
                            List<bill_info> bill_Infos = b.bill_info.ToList();
                            foreach (bill_info info in bill_Infos)
                            {
                                revenue += (decimal)info.product1.price * (decimal)info.quantity;
                            }
                        }    
                    }
                }
            }
            return revenue;
        }

        public decimal revenueYearNew()
        {
            List<bill> bills = context
                .bill
                .Where(b => b.status == 2)
                .ToList();
            decimal revenue = 0;
            foreach (bill b in bills)
            {
                if (b.booking_date != null && ((DateTime)b.booking_date).Year == DateTime.Now.Year)
                {
                    List<bill_info> bill_Infos = b.bill_info.ToList();
                    foreach (bill_info info in bill_Infos)
                    {
                        revenue += (decimal)info.product1.price * (decimal)info.quantity;
                    }
                }
            }
            return revenue;
        }

        public decimal revenueYearOld()
        {
            List<bill> bills = context
                .bill
                .Where(b => b.status == 2)
                .ToList();
            decimal revenue = 0;
            foreach (bill b in bills)
            {
                if (b.booking_date != null && ((DateTime)b.booking_date).Year == DateTime.Now.Year - 1)
                {
                    List<bill_info> bill_Infos = b.bill_info.ToList();
                    foreach (bill_info info in bill_Infos)
                    {
                        revenue += (decimal)info.product1.price * (decimal)info.quantity;
                    }
                }
            }
            return revenue;
        }
    }
}