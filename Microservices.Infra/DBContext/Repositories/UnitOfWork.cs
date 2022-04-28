using Microservices.Infra.DBContext.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Microservices.Infra.DBContext.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, new()
    {
        private bool disposedValue;

        private DbContext _context;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _objTran = null;

        public IGenericTypeRepository<T> _repo;

        public UnitOfWork(IGenericTypeRepository<T> repo)
        {
            _context = repo.GetDBContext;
            _repo = repo;
        }

        public void Commit()
        {
            if (_objTran is not null)
                _objTran.Commit();
        }

        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();            
        }
        
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(_errorMessage, dbEx);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
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
    }
}
