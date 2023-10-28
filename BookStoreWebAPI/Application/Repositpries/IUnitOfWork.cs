namespace Application.Repositpries
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }

        void Commit();
    }
}