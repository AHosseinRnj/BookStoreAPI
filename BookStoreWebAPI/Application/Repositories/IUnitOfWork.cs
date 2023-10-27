namespace Application.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}