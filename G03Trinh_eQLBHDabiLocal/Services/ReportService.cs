using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class ReportService
    {
        private EFDbContext context = new EFDbContext();
        public List<DataReport> reportProduct(DataReport dataReport)
        {
            List<DataReport> rs = context.Database.SqlQuery<DataReport>("EXEC reportProduct @startDate, @endDate", new object[]
            {
                new SqlParameter("@startDate", dataReport.startDate),
                new SqlParameter("@endDate", dataReport.endDate)
            }).ToList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            int i = 15;
            foreach (var item in rs)
            {
                item.color = propInfos[i].Name;
                i++;
            }
            return rs;
        }

        public List<DataReport> reportCategory(DataReport dataReport)
        {
            List<DataReport> rs = context.Database.SqlQuery<DataReport>("EXEC reportCategory @startDate, @endDate", new object[]
            {
                new SqlParameter("@startDate", dataReport.startDate),
                new SqlParameter("@endDate", dataReport.endDate)
            }).ToList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            int i = 20;
            foreach (var item in rs)
            {
                item.color = propInfos[i].Name;
                i++;
            }
            return rs;
        }
    }
}