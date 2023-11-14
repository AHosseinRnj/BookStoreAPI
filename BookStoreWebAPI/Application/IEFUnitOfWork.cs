using Application.Repositpries;

namespace Application
{
    public interface IEFUnitOfWork : IDisposable
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public IUserRepository UserRepository { get; }

        void Complete();
    }
}