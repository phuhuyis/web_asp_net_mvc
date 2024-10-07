using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class AccountService
    {
        private EFDbContext context;

        public AccountService(EFDbContext context) {
            this.context = context;
        }

        public bool login(Account user, int permission)
        {
            if (user.username.Length > 0 && user.password.Length > 0)
            {
               account acc = context.account
                .Where(a => a.username == user.username)
                .Where(a => a.permission == permission)
                .FirstOrDefault();
                if(acc != null && BCrypt.Net.BCrypt.Verify(user.password, acc.password))
                {
                    Session.setSession(Constant.userSession, new Account() { username = acc.username, permission = acc.permission });
                    return true;
                }
            }
            return false;
        }

        public int getPermission(String username)
        {
            account acc = context.account.Where(a => a.username == username).FirstOrDefault();
            return acc != null ? (int)acc.permission : -1;
        }

        public Account get(String username)
        {
            account entity = context.account.Where(a  => a.username == username).FirstOrDefault();
            if(entity != null)
            {
                Account model = new Account() { 
                    username = entity.username
                };
                return model;
            }
            return null;
        }

        public bool changePass(String username, String pass)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                account entityold = context.account.Where(a =>a.username == username).FirstOrDefault();
                account entitynew = new account()
                {
                    username = username,
                    password = BCrypt.Net.BCrypt.HashPassword(pass),
                    permission = entityold.permission
                };
                try
                {
                    context.account.AddOrUpdate(entitynew);
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

        public bool add(String username, String pass)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                account entitynew = new account()
                {
                    username = username,
                    password = BCrypt.Net.BCrypt.HashPassword(pass),
                    permission = 1
                };
                try
                {
                    context.account.Add(entitynew);
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