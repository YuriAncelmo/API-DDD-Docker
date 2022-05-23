using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDWebAPI.Infrastruture.Repository.Repositorys
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly MySqlContext _context;

        public RepositoryBase(MySqlContext Context)
        {
            _context = Context;
        }

        public virtual void Add(TEntity obj)
        {
            //_context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual void Update(TEntity obj)
        {

            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public virtual void Remove(TEntity obj)
        {
            _context.Remove<TEntity>(obj);
            _context.SaveChanges();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }


    }

}
