using DomainLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecification<TEntity,Tkey> where TEntity: BaseEntity<Tkey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; } 
        public List<Expression<Func<TEntity,object>>> Includes { get; }

        // order by, orderbydesc
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }
        // Pagination 
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; set; }
    }
}
