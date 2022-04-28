using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Microservices.Infra.DBContext.Interface
{
    public interface IGenericTypeRepository<T> : IDisposable where T : class
    {
        DbContext GetDBContext { get; }

        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Execute the stored procedure.
        void ExeSp(string SP_Name, params object[] parameters);
        IEnumerable<T> ExeSpReturnObj(string SP_Name, params object[] parameters);
        IEnumerable<T> ExeSpReturnObj_ImportSQLInjection(string SP_Name, params object[] parameters);
    }
}
