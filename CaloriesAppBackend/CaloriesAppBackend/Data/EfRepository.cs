using CaloriesAppBackend.Interfaces;
using CaloriesAppBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaloriesAppBackend.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly CaloriesContext db;

        public EfRepository(CaloriesContext context)
        {
            db = context;
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }

    }
}
