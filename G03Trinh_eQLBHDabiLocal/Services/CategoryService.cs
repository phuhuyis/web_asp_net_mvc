using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class CategoryService
    {
        private EFDbContext context;

        public CategoryService(EFDbContext context)
        {
            this.context = context;
        }

        public List<Category> list()
        {
            List<category> categoriesEntity = context.category.ToList();
            List<Category> categoriesModel = new List<Category>();
            foreach (category category in categoriesEntity)
            {
                categoriesModel.Add(new Category()
                {
                    id = category.id,
                    name = category.name,
                    metatitle = category.metatitle
                });
            }
            return categoriesModel;
        }

        public List<Category> list(String key)
        {
            key = key.ToLower();
            List<category> categoriesEntity = context.category.Where(c => c.name.ToLower().Contains(key)).ToList();
            List<Category> categoriesModel = new List<Category>();
            foreach (category category in categoriesEntity)
            {
                categoriesModel.Add(new Category()
                {
                    id = category.id,
                    name = category.name,
                    metatitle = category.metatitle
                });
            }
            return categoriesModel;
        }

        public List<category> listEntity()
        {
            return context.category.ToList();
        }

        public bool add(Category category)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                category categoryEntity = new category()
                {
                    name = category.name,
                    metatitle = category.metatitle
                };
                try
                {
                    category afterAdd = context.category.Add(categoryEntity);
                    context.SaveChanges();
                    transaction.Commit();
                    return afterAdd.id != 0;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public Category get(int id)
        {
            category entity = context.category.Where(c => c.id == id).FirstOrDefault();
            if (entity != null)
            {
                Category model = new Category()
                {
                    name = entity.name,
                    id = entity.id,
                    metatitle = entity.metatitle
                };
                return model;
            }
            return null;
        }
        
        public bool update(int id, Category model)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                category entitynew = new category()
                {
                    id = id,
                    name = model.name,
                    metatitle = model.metatitle
                };
                try
                {
                    context.category.AddOrUpdate(entitynew);
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

        public bool delete(Category model)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                category entity = context.category.Where(c => c.id == model.id).FirstOrDefault();
                try
                {
                    context.category.Remove(entity);
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

        public Category getByMetaTitle(String metaTitle)
        {
            category category = context.category.Where(c => c.metatitle == metaTitle).FirstOrDefault();
            if (category == null)
                return null;
            Category rs = new Category() { 
                id = category.id,
                name = category.name,
                metatitle = category.metatitle
            };
            return rs;
        }
    }
}