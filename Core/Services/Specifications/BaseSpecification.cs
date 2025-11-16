using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    abstract class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria) 
                => Criteria = criteria; //public BaseSpecification(Expression<Func<TEntity, bool>)
        
        public Expression<Func<TEntity, bool>>? Criteria {get; private set;}

        public List<Expression<Func<TEntity, object>>> Includes {get;} = [];

        public Expression<Func<TEntity, object>>? OrderBy {get; private set;}


        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take {get;private set;}

        public int Skip { get; private set; }
        public bool IsPagingEnabled { get;  set; }

        // AddInclude()
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            
        }

        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression; //public void AddOrderBy(Expression<Func<TEntity, object>>)

        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;

        public void ApplyPaging(int PageSize, int PageIndex) 
        {
            IsPagingEnabled = true;
            Skip = (PageIndex - 1)  * PageSize;
            Take = PageSize;
        }
    }
}
