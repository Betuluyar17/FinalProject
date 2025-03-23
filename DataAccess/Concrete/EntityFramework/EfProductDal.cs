using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of C# : USING kullanımı
            //using ile method olşturursak daha sonra sistem tarafından arka planda kullanılmamış olur. Garbage collector otomatikman o komut satırını çalıştırmayı durdurur.

            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity); //EF'ye ait bir kullanımdır. Veri kaynağından gönderdiğimiz Product entity ile eşleşen nesneyi bul demektir. 
                addedEntity.State = EntityState.Added; //addedEntity'nin durumunu (state) eklenecek bir nesne olarak belirttik. (Ekle komutu)
                context.SaveChanges(); //Değişiklikleri Db'ye kaydet.
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
