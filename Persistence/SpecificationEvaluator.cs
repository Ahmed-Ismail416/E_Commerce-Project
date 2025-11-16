using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator 
    {
        //Creat Query
        public static IQueryable<TEntity> CreatQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity,TKey> specification) where TEntity : BaseEntity<TKey>
        {
            //Create Query
            var Query = inputQuery;
            if(specification.Criteria != null)
            {
                Query = Query.Where(specification.Criteria);
            }
            if(specification.OrderBy != null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }
            if(specification.OrderByDescending != null)
            {
                Query = Query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.Includes != null)
            {
                Query = specification.Includes.Aggregate(Query, (current, include) => current.Include(include));
            }
            if (specification.IsPagingEnabled)
            {
                Query = Query.Skip(specification.Skip).Take(specification.Take);
            }
            return Query;
        }
    }
}
