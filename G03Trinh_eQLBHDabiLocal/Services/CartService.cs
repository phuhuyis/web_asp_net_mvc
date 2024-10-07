using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class CartService
    {
        private EFDbContext context = new EFDbContext();

        public List<cart> list(String username)
        {
            List<cart> carts = context.cart.Where(c => c.customer1.account.username == username).ToList();
            if (carts.Count == 0)
                return null;
            return carts;
        }

        public cart get(Cart cartModel)
        {
            cart cart = context.cart.Where(c => c.customer == cartModel.customer).Where(c => c.product == cartModel.product).FirstOrDefault();
            return cart;
        }

        public List<Cart> listModel(String username)
        {
            List<cart> carts = context.cart.Where(c => c.customer1.account.username == username).ToList();
            if (carts.Count == 0)
                return null;
            List<Cart> cartsModel = new List<Cart>();
            foreach (cart item in carts)
            {
                Cart model = new Cart()
                {
                    id = item.id,
                    customer = item.customer,
                    product = item.product,
                    quantity = item.quantity,
                };
                cartsModel.Add(model);
            }
            return cartsModel;
        }

        public Cart add(Cart cart)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                cart entity = new cart()
                {
                    product = cart.product,
                    customer = cart.customer,
                    quantity = cart.quantity,
                    color = cart.color
                };
                cart cartOld = context.cart.Where(c => c.product == entity.product).Where(p => p.customer == entity.customer).Where(p => p.color == entity.color).FirstOrDefault();
                if (cartOld != null)
                {
                    try
                    {
                        entity.quantity += cartOld.quantity;
                        entity.id = cartOld.id;
                        context.cart.AddOrUpdate(entity);
                        context.SaveChanges();
                        transaction.Commit();
                        Cart cartReturn = new Cart()
                        {
                            product = cart.product,
                            customer = cart.customer,
                            quantity = cart.quantity,
                            id = cart.id,
                            color = cart.color
                        };
                        return cartReturn;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        cart rs = context.cart.Add(entity);
                        context.SaveChanges();
                        transaction.Commit();
                        Cart cartReturn = new Cart()
                        {
                            product = rs.product,
                            customer = rs.customer,
                            quantity = rs.quantity,
                            id = rs.id,
                            color = cart.color
                        };
                        return cartReturn;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public void removeAll(String username)
        {
            context.Database.ExecuteSqlCommand(
                "Delete cart where customer = @customerid",
                new SqlParameter(
                    "@customerid",
                    context.customer.Where(c => c.username == username).FirstOrDefault().id
                    )
                );
        }

        public void updateAll(List<Cart> carts)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach(Cart cart in carts)
                    {
                        cart entity = new cart()
                        {
                            id = context.cart.Where(c => c.product == cart.product).Where(c => c.customer == cart.customer).FirstOrDefault().id,
                            customer = cart.customer,
                            quantity = cart.quantity,
                            product = cart.product
                        };
                        context.cart.AddOrUpdate(entity);
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }    
        }

        public void update(Cart cart)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    cart entity = new cart()
                    {
                        id = context.cart.Where(c => c.product == cart.product).Where(c => c.customer == cart.customer).Where(c => c.color == cart.color).FirstOrDefault().id,
                        customer = cart.customer,
                        quantity = cart.quantity,
                        product = cart.product,
                        color = cart.color
                    };
                    context.cart.AddOrUpdate(entity);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void delete(Cart cart)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    cart entity = context.cart.Where(c => c.product == cart.product).Where(c => c.customer == cart.customer).Where(c => c.color == cart.color).FirstOrDefault();
                    context.cart.Remove(entity);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}