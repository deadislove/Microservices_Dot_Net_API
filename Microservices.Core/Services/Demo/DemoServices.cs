using Microservices.Core.Interface;
using Microservices.Core.Repositories.Demo;
using Microservices.Infra.DBContext.Database;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microservices.Infra.DBContext.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Services.Demo
{
    public class DemoServices : AbstDemoRepository<Enterprise_MVC_Core>
    {
        public DemoServices(IGenericTypeRepository<Enterprise_MVC_Core> repo) : base(repo)
        {
        }

        public IEnumerable<Enterprise_MVC_Core> Demo()
        {            
            return GetAll();
        }
    }
}
