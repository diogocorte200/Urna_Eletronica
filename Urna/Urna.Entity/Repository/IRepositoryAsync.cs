﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Urna.Entity.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate, string navigation);
        Task<T> GetOne(Expression<Func<T, bool>> predicate);
        Task Insert(T entity);
        void Delete(T entity);
        Task Delete(object id);
        Task Update(object id, T entity);
    }
}
