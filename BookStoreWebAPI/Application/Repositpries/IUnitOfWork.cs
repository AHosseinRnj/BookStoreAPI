namespace Application.Repositpries
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }

        void Commit();
    }
}