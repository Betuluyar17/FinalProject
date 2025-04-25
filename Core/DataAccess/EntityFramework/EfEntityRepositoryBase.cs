using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>: IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of C# : USING kullanımı
            //using ile method olşturursak daha sonra sistem tarafından arka planda kullanılmamış olur. Garbage collector otomatikman o komut satırını çalıştırmayı durdurur.

            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //EF'ye ait bir kullanımdır. Veri kaynağından gönderdiğimiz Product entity ile eşleşen nesneyi bul demektir. 
                addedEntity.State = EntityState.Added; //addedEntity'nin durumunu (state) eklenecek bir nesne olarak belirttik. (Ekle komutu)
                context.SaveChanges(); //Değişiklikleri Db'ye kaydet.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
