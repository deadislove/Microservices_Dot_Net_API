using Microservices.Infra.DBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microservices.Infra.DBContext.Database
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {            
        }

        public DbSet<Enterprise_MVC_Core> Enterprise_MVC_Cores { get; set; }
    }
}
