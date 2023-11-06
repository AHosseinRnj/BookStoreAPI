namespace Application
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Rollback();
        void Commit();
    }
}