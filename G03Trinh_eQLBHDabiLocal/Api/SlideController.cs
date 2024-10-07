using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Api
{
    public class SlideController : ApiController
    {
        private ProductService productService = new ProductService(new Entity.EFDbContext());
        private SlideService slideService = new SlideService();

        [System.Web.Http.Route("api/admin/slide")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage setSlide()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (productService.checkFileName(postedFile))
                    {
                        String url = productService.saveFile(postedFile);
                        Slide slide = new Slide()
                        {
                            id = int.Parse(httpRequest.Form[0]),
                            url = url
                        };
                        slideService.setSlide(slide);
                        return Request.CreateResponse(HttpStatusCode.OK, "success");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Hình ảnh không đúng định dạng chỉ nhận file png, jpg hoặc jpeg");
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}