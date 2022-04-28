using Microservices.Infra.DBContext.Database;
using Microservices.Infra.DBContext.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Infra.DBContext.Repositories
{
    public class GenericTypeRepository<T> : IGenericTypeRepository<T> where T : class
    {
        private bool disposedValue;

        private readonly DemoDbContext _context;

        internal DbSet<T> _entities;

        public GenericTypeRepository(DemoDbContext context)
        {
            _context = context;
        }

        public virtual DbSet<T> Entities {
            get { return _entities ??= _context.Set<T>(); }
        }

        public virtual IQueryable<T> Table
        {
            get { return _entities; }
        }

        public DbContext GetDBContext => _context;

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void ExeSp(string SP_Name, params object[] parameters)
        {
            CancellationToken token = new(true);
            _context.Database.ExecuteSqlRaw(SP_Name, parameters, token);            
        }

        public IEnumerable<T> ExeSpReturnObj(string SP_Name, params object[] parameters)
        {
            return _context.Set<T>().FromSqlRaw(SP_Name, parameters);
        }

        public IEnumerable<T> ExeSpReturnObj_ImportSQLInjection(string SP_Name, params object[] parameters)
        {
            return _context.Set<T>().FromSqlInterpolated($"EXEC {SP_Name} {parameters}");
        }

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GenericTypeRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }        
        #endregion
    }
}
