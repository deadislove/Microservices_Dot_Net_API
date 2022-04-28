using System.Collections.Generic;

namespace Microservices.Core.Interface
{
    public interface IDemoRepository<T> where T : class
    {
        IEnumerable<T> GetDemoAll();
    }
}
