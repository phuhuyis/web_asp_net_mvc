using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Api
{
    [RoutePrefix("api/admin/report")]
    public class ReportController : ApiController
    {
        private ReportService reportService = new ReportService();

        [HttpPost]
        [Route("product")]
        [ResponseType(typeof(List<DataReport>))]
        public IHttpActionResult reportProduct(DataReport dataReport)
        {
            return Ok(reportService.reportProduct(dataReport));
        }

        [HttpPost]
        [Route("category")]
        [ResponseType(typeof(List<DataReport>))]
        public IHttpActionResult reportCategory(DataReport dataReport)
        {
            return Ok(reportService.reportCategory(dataReport));
        }
    }
}
