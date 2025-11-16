using DomainLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Tkey id);

        //Specification based
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, Tkey> spec);
        Task<int> CountAsync(ISpecification<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);


    }
}
