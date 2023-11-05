namespace Application.Repositpries
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Rollback();
        void Commit();
    }
}