using Application.Repositories;

namespace Application
{
    public interface IEFUnitOfWork : IDisposable
    {
        public IBookWriteRepository BookRepository { get; }
        public IAuthorWriteRepository AuthorRepository { get; }
        public ICategoryWriteRepository CategoryRepository { get; }
        public IOrderItemWriteRepository OrderItemRepository { get; }
        public IOrderWriteRepository OrderRepository { get; }
        public IPublisherWriteRepository PublisherRepository { get; }
        public IUserWriteRepository UserRepository { get; }

        void Complete();
    }
}