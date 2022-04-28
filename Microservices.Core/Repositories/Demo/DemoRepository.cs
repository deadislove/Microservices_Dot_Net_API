using Microservices.Core.Interface;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microservices.Infra.DBContext.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Core.Repositories.Demo
{
    public class DemoRepository<T> : UnitOfWork<T>, IDemoRepository<T> where T : Enterprise_MVC_Core, new()
    {
        public DemoRepository(IGenericTypeRepository<T> repo) : base(repo)
        { }

        public IEnumerable<T> GetDemoAll()
        {
            return _repo.GetAll().ToList();
        }
    }

    public abstract class AbstDemoRepository<T> : UnitOfWork<T> where T : Enterprise_MVC_Core, new()
    {
        public AbstDemoRepository(IGenericTypeRepository<T> repo) : base(repo)
        { }

        public IEnumerable<T> GetAll()
        {
            return _repo.GetAll().ToList();
        }
    }
}
