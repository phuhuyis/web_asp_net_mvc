using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class ContactService
    {
        private EFDbContext context = new EFDbContext();
        public bool add(Contact contact, String username)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                contact entity = new contact() { 
                    title = contact.title,
                    content = contact.content,
                    status = 0,
                    customer = context.customer.Where(c => c.username == username).FirstOrDefault().id
                };
                try
                {
                    context.contact.Add(entity);
                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<contact> list()
        {
            return context.contact.OrderBy(c => c.status).ToList();
        }

        public void handle(int id)
        {
            using(DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                contact contact = context.contact.Where(c => c.id == id).FirstOrDefault();
                contact.status = 1;
                context.contact.AddOrUpdate(contact);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public void progress(int id)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                contact contact = context.contact.Where(c => c.id == id).FirstOrDefault();
                contact.status = 0;
                context.contact.AddOrUpdate(contact);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public contact get(int id)
        {
            return context.contact.Where(c => c.id == id).FirstOrDefault();
        }

        public void delete(int id)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                contact contact = context.contact.Where(c => c.id == id).FirstOrDefault();
                context.contact.Remove(contact);
                context.SaveChanges();
                transaction.Commit();
            }
        }
    }
}