using RecipeWebsiteMVC.DataAccess.Interfaces;
using RecipeWebsiteMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace RecipeWebsiteMVC.DataAccess
{
    /// <summary>
    /// Creating a Generic Repo that uses the patterns from the Interface
    /// For Simple Comands :)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _db;
        internal DbSet<T> dbSet;

        public Repository(DbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);   
        }

        public T Get(string id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
               return dbSet.FirstOrDefault(predicate);  
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);   
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
