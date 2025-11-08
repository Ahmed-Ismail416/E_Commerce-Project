using DomainLayer.Contracts;
using DomainLayer.Models.Products;
using Microsoft.Identity.Client;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.UnitOfWork
{
    public class UnitOfWork(StoreDbContext _dbcontext) : IUnitOfWork
    {
        public readonly Dictionary<string, object> repos = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typename = typeof(TEntity).Name;
            if(repos.TryGetValue(typename, out object repo))
                return (IGenericRepository<TEntity, TKey>)repo;
            else
            {
                //create object
                var repository = new GenericRepository<TEntity, TKey>(_dbcontext);
                // add to dict
                repos.Add(typename, repository);
                //return object
                return repository;

            }
        }

        public Task<int> SaveChangesAsync()  => _dbcontext.SaveChangesAsync();
    }
}
