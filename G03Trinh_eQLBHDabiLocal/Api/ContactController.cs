using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Api
{
    public class ContactController : ApiController
    {
        private ContactService contactService = new ContactService();

        [Route("api/admin/contact")]
        [HttpDelete]
        public IHttpActionResult delete(int id)
        {
            if (contactService.get(id) == null)
                return NotFound();
            contactService.delete(id);
            return Ok("success");
        }

        [Route("api/admin/contact/{id}")]
        [HttpPut]
        public IHttpActionResult handle(int id)
        {
            if (contactService.get(id) == null)
                return NotFound();
            contactService.handle(id);
            return Ok("success");
        }

        [Route("api/admin/contact/{id}")]
        [HttpDelete]
        public IHttpActionResult progress(int id)
        {
            if (contactService.get(id) == null)
                return NotFound();
            contactService.progress(id);
            return Ok("success");
        }
    }
}