using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class SlideService
    {
        private EFDbContext context = new EFDbContext();

        public List<Slide> list()
        {
            List<slide> entity = context.slide.OrderBy(s => s.position).ToList();
            List<Slide> result = new List<Slide>();
            foreach (slide slide in entity)
            {
                result.Add(new Slide()
                {
                    id = slide.id,
                    position = slide.position,
                    username = slide.username,
                    url = slide.url
                });
            }
            return result;
        }

        public void setSlide(Slide slide)
        {
            slide entity = context.slide.Where(s => s.id == slide.id).FirstOrDefault();
            entity.username = (Common.Session.getSession(Constant.userSession) as Account).username;
            entity.url = slide.url;
            context.slide.AddOrUpdate(entity);
            context.SaveChanges();
        }
    }
}