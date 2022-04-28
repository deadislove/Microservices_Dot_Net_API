namespace Microservices.Infra.DBContext.Interface
{
    public interface IUnitOfWork<T> where T :class
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
