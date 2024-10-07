using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Services
{
    public class ProductService
    {
        private EFDbContext context;

        public ProductService(EFDbContext context)
        {
            this.context = context;
        }

        public bool checkFile(HttpPostedFileBase image)
        {
            String extension = Path.GetExtension(image.FileName);
            if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkFileName(HttpPostedFile image)
        {
            String extension = Path.GetExtension(image.FileName);
            if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String saveFile(HttpPostedFile image)
        {
            string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            path += "Resources\\admin\\img\\upload\\slide\\";
            String fileName = "slide-" +
                DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture) + Path.GetExtension(image.FileName);
            String rs = "/" + "Resources/admin/img/upload/slide/" + fileName;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                image.SaveAs(path + fileName);
                return rs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String saveFile(HttpPostedFileBase image)
        {
            string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            path += "Resources\\admin\\img\\upload\\product\\";
            String fileName = "product-" +
                DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture) + Path.GetExtension(image.FileName);
            String rs = "/" + "Resources/admin/img/upload/product/" + fileName;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                image.SaveAs(path + fileName);
                return rs;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool add(Product product)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                String url = saveFile(product.image);
                if (url != null)
                {
                    product entity = new product()
                    {
                        name =  product.name,
                        category = product.category,
                        description = product.description,
                        price = product.price,
                        image = url
                    };
                    try
                    {
                        product afterAdd = context.product.Add(entity);
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
                transaction.Rollback();
                return false;
            }
        }

        public List<Product> list(int? page)
        {
            List<product> productsEntity = context
                .product
                .OrderBy(p => p.id)
                .Skip((int)(3 * (page - 1)))
                .Take(3)
                .ToList();
            List<Product> productsModel = new List<Product>();
            foreach (product product in productsEntity)
            {
                productsModel.Add(new Product()
                {
                    id = product.id,
                    name = product.name,
                    category = product.category,
                    description = product.description,
                    price = product.price,
                    url_img = product.image
                });
            }
            return productsModel;
        }

        public List<Product> list(String key, int? page)
        {
            key = key.ToLower();
            List<product> productsEntity = context
                .product
                .Where(p => p.name.ToLower().Contains(key))
                .OrderBy(p => p.id)
                .Skip((int)(3 * (page - 1)))
                .Take(3)
                .ToList();
            List<Product> productsModel = new List<Product>();
            foreach (product product in productsEntity)
            {
                productsModel.Add(new Product()
                {
                    id = product.id,
                    name = product.name,
                    category = product.category,
                    description = product.description,
                    price = product.price,
                    url_img = product.image
                });
            }
            return productsModel;
        }

        public bool delete(Product model)
        {
            String url = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + model.url_img.Substring(1).Replace("/", "\\");
            if (File.Exists(url))
            {
                File.Delete(url);
            }
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                product entity = context.product.Where(c => c.id == model.id).FirstOrDefault();
                try
                {
                    context.product.Remove(entity);
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

        public Product get(int? id)
        {
            product entity = context.product.Where(c => c.id == id).FirstOrDefault();
            if (entity != null)
            {
                Product model = new Product()
                {
                    name = entity.name,
                    id = entity.id,
                    category = entity.category,
                    description = entity.description,
                    price = entity.price,
                    url_img = entity.image
                };
                return model;
            }
            return null;
        }

        public bool update(int id, Product model)
        {
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                if (model.change_img)
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) +  get(id).url_img.Substring(1).Replace("/", "\\")))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + get(id).url_img.Substring(1).Replace("/", "\\"));
                    }
                    String url = saveFile(model.image);
                    if (url != null)
                    {
                        product entitynew = new product()
                        {
                            id = id,
                            name = model.name,
                            category = model.category,
                            description = model.description,
                            price = model.price,
                            image = url
                        };
                        try
                        {
                            context.product.AddOrUpdate(entitynew);
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
                    transaction.Rollback();
                    return false;
                }
                else
                {
                    product entitynew = new product()
                    {
                        id = id,
                        name = model.name,
                        category = model.category,
                        description = model.description,
                        price = model.price,
                        image = get(id).url_img
                    };
                    try
                    {
                        context.product.AddOrUpdate(entitynew);
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

        public List<Product> listByMetaTitle(String metaTitle, int? page)
        {
            List<product> productsEntity = context
                .product
                .Where(p => p.category1.metatitle == metaTitle)
                .OrderBy(p => p.id)
                .Skip((int)(12 * (page - 1)))
                .Take(12)
                .ToList();
            List<Product> productsModel = new List<Product>();
            foreach (product product in productsEntity)
            {
                productsModel.Add(new Product()
                {
                    id = product.id,
                    name = product.name,
                    category = product.category,
                    description = product.description,
                    price = product.price,
                    url_img = product.image
                });
            }
            return productsModel;
        }

        public int getFullPageByMetaTitle(String metaTitle)
        {
            int count = context.product.Where(p => p.category1.metatitle == metaTitle).Count();
            return count % 12 == 0 ? count / 12 : count / 12 + 1;
        }

        public int getFullPage()
        {
            int count = context.product.Count();
            return count % 3 == 0 ? count / 3 : count / 3 + 1;
        }
        public int getFullPage(String key)
        {
            key = key.ToLower();
            int count = context.product.Where(p => p.name.ToLower().Contains(key)).Count();
            return count % 3 == 0 ? count / 3 : count / 3 + 1;
        }

        public int getFullPageSearch(String key)
        {
            int count = context
                .product
                .Where(p => p.name.ToLower().Contains(key))
                .Count();
            return count % 12 == 0 ? count / 12 : count / 12 + 1;
        }

        public List<Product> search(String key, int? page)
        {
            List<product> productsEntity = context
                .product
                .Where(p => p.name.ToLower().Contains(key))
                .OrderBy(p => p.id)
                .Skip((int)(12 * (page - 1)))
                .Take(12)
                .ToList();
            List<Product> productsModel = new List<Product>();
            foreach (product product in productsEntity)
            {
                productsModel.Add(new Product()
                {
                    id = product.id,
                    name = product.name,
                    category = product.category,
                    description = product.description,
                    price = product.price,
                    url_img = product.image
                });
            }
            return productsModel;
        }

        public List<Product> similar(int? id)
        {
            product productNow = context.product.Where(p => p.id == id).FirstOrDefault();
            if (productNow == null)
            {
                return null;
            }
            List<product> products = context
                .product
                .Where(p => p.category == productNow.category)
                .Where(p => p.id != productNow.id)
                .OrderBy(p => Math.Abs((decimal)(p.price - productNow.price)))
                .Take(4)
                .ToList();
            if(products.Count < 4)
            {
                List<int> ids = new List<int>();
                foreach (product product in products)
                {
                    ids.Add(product.id);
                }
                List<product> productsPrice = context
                    .product
                    .Where(p => !ids.Contains(p.id))
                    .OrderBy(p => Math.Abs((decimal)(p.price - productNow.price)))
                    .Take(4 - products.Count)
                    .ToList();
                foreach (product product in productsPrice)
                {
                    products.Add(product);
                }
            }
            List<Product> rs = new List<Product> ();
            foreach(product product in products)
            {
                rs.Add(new Product()
                {
                    id = product.id,
                    name = product.name,
                    category = product.category,
                    description = product.description,
                    price = product.price,
                    url_img = product.image
                });
            }
            return rs;
        }
    }
}