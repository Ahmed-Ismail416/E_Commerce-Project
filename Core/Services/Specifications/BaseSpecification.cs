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
    public class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria) 
                => Criteria = criteria; //public BaseSpecification(Expression<Func<TEntity, bool>)
        
        public Expression<Func<TEntity, bool>> Criteria {get; private set;}

        public List<Expression<Func<TEntity, bool>>> Includes {get;} = [];

        // AddInclude()
        public void AddInclude(Expression<Func<TEntity, bool>> includeExpression)
        {
            Includes.Add(includeExpression);
            
        }
    }
}
