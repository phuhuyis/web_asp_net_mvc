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
    public class CustomerService
    {
        private EFDbContext context;

        public CustomerService(EFDbContext context)
        {
            this.context = context;
        }

        public List<Customer> list()
        {
            List<customer> customersEntity = context.customer.ToList();
            List <Customer> customersModel = new List<Customer> ();
            foreach (customer customer in customersEntity)
            {
                customersModel.Add(new Customer()
                {
                    id = customer.id,
                    name = customer.name,
                    address = customer.address,
                    phone = customer.phone,
                    email = customer.email,
                });
            }
            return customersModel;
        }

        public List<Customer> list(String key)
        {
            key = key.ToLower();
            List<customer> customersEntity = context.customer.Where(c => c.name.ToLower().Contains(key)).ToList();
            List<Customer> customersModel = new List<Customer>();
            foreach (customer customer in customersEntity)
            {
                customersModel.Add(new Customer()
                {
                    id = customer.id,
                    name = customer.name,
                    address = customer.address,
                    phone = customer.phone,
                    email = customer.email,
                });
            }
            return customersModel;
        }

        public int add(Customer customer)
        {
            account account = context.account.Where(a => a.username == customer.username).FirstOrDefault();
            if (account != null)
            {
                return -1;
            }
            else
            {
                using(DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    context.account.Add(new account()
                    {
                        username = customer.username,
                        password = BCrypt.Net.BCrypt.HashPassword(customer.password),
                        permission = 0
                    });
                    context.SaveChanges();
                    customer customerEntity = new customer()
                    {
                        name = customer.name,
                        address = customer.address,
                        phone = customer.phone,
                        username = customer.username,
                        email = customer.email,
                    };
                    try
                    {
                        customer afterAdd = context.customer.Add(customerEntity);
                        context.SaveChanges();
                        transaction.Commit();
                        return afterAdd.id;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                }    
            }
        }

        public Customer get(int id)
        {
            customer entity = context.customer.Where(c => c.id == id).FirstOrDefault();
            if(entity != null)
            {
                Customer model = new Customer() 
                {
                    name = entity.name,
                    address = entity.address,
                    phone = entity.phone,
                    id = entity.id,
                    username = entity.username,
                    email = entity.email,
                };
                return model;
            }
            return null;
        }

        public Customer findByUsername(String username)
        {
            customer entity = context.customer.Where(c => c.username == username).FirstOrDefault();
            if (entity != null)
            {
                Customer model = new Customer()
                {
                    name = entity.name,
                    address = entity.address,
                    phone = entity.phone,
                    id = entity.id,
                    username = entity.username,
                    email = entity.email,
                };
                return model;
            }
            return null;
        }

        public bool update(int id, Customer model)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                customer entityold = context.customer.Where(c => c.id == id).FirstOrDefault();
                customer entitynew = new customer()
                {
                    id = id,
                    name = model.name,
                    address = model.address,
                    phone = model.phone,
                    account = new account() { username = entityold.username, password = entityold.account.password },
                    username = entityold.username,
                    email = model.email,
                };
                try
                {
                    context.customer.AddOrUpdate(entitynew);
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

        public bool update(Customer model, String username)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                customer entityold = context.customer.Where(c => c.username == username).FirstOrDefault();
                customer entitynew = new customer()
                {
                    id = entityold.id,
                    name = model.name,
                    address = model.address,
                    phone = model.phone,
                    account = new account() { username = entityold.username, password = model.password },
                    username = entityold.username,
                    email = model.email,
                };
                try
                {
                    context.customer.AddOrUpdate(entitynew);
                    context.account.AddOrUpdate(new account()
                    {
                        username = entitynew.username,
                        password = BCrypt.Net.BCrypt.HashPassword(model.password),
                        permission = 0
                    });
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

        public bool delete(Customer model)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                customer entity = context.customer.Where(c => c.id == model.id).FirstOrDefault();
                try
                {
                    context.customer.Remove(entity);
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
    }
}