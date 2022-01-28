using Microsoft.EntityFrameworkCore;
using UserAPI.Entities.Models;
using UserAPI.Repository.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UserAPI.Repository.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>  where T : class
    {
        protected RepositoryContext RepositoryContext;
        private DbSet<T> RepContextSet;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            RepContextSet = RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
            RepContextSet
            .AsNoTracking() :
            RepContextSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            RepContextSet
            .Where(expression)
            .AsNoTracking() :
            RepContextSet
            .Where(expression);

        public void Create(T entity) => RepContextSet.Add(entity);

        public void Update(T entity)
        {
            RepContextSet.Update(entity);
        }

        //public void Delete(T entity)
        //{
        //    entity.IsEnable = false;
        //    RepContextSet.Update(entity);
        //}
        public void /*NotSoft*/Delete(T entity) => RepContextSet.Remove(entity);
    }
}
