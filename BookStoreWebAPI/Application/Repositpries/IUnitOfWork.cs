namespace Application.Repositpries
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }

        void Commit();
    }
}