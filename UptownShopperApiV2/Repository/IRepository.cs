using System.Collections.Generic;
using UptownShopperApiV2.Models;

namespace UptownShopperApiV2.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Find(long id);
        long Add(T item);
        void Update(T item);
        void Remove(long id);
    }
}