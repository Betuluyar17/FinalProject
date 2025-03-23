using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    // Generic Constraint : IEntityRepository<T> için 'where T:' koşuluyla oluşturduğumuz filtrelere verilen genel isimdir.
    // class : Bir class anlamı taşımaz, referans tip demektir. 
    // IEntity : Class olarak yazdığımızda sistem tarafından var olan class'ları da belirtebileceğimiz için IEntity tanımlanır.
    // new : IEntity içeren class'ları kullanabileceğimiz gibi IEntity'nin kendisini de kullanabiliyor olduğumuz için daha da kısıt vermek adına new() koşulu eklenir. IEntity new'lenemediği, oluşturduğumuz class'lar new'lenebilir olduğu için ekledik.


    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
