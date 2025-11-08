using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext context) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)  => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Tkey id) => await context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

        #region withspecficiation
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec)
        {
            return await SpecificationEvaluator.CreatQuery(context.Set<TEntity>(), spec).ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity,Tkey> spec)
        {
            return await SpecificationEvaluator.CreatQuery(context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        #endregion
    }
}
