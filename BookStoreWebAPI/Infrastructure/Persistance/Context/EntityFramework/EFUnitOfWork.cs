using Application;
using Application.Repositories;
using Infrastructure.Persistance.Repositories;

namespace Infrastructure.Persistance
{
    public class EFUnitOfWork : IEFUnitOfWork
    {
        private readonly EFContext _context;

        public IBookWriteRepository BookRepository { get; private set; }
        public IAuthorWriteRepository AuthorRepository { get; private set; }
        public ICategoryWriteRepository CategoryRepository { get; private set; }
        public IOrderItemWriteRepository OrderItemRepository { get; private set; }
        public IOrderWriteRepository OrderRepository { get; private set; }
        public IPublisherWriteRepository PublisherRepository { get; private set; }
        public IUserWriteRepository UserRepository { get; private set; }

        public EFUnitOfWork(EFContext context)
        {
            _context = context;

            BookRepository = new BookWriteRepository(_context);
            AuthorRepository = new AuthorWriteRepository(_context);
            CategoryRepository = new CategoryWriteRepository(_context);
            OrderItemRepository = new OrderItemWriteRepository(_context);
            OrderRepository = new OrderWriteRepository(_context);
            PublisherRepository = new PublisherWriteRepository(_context);
            UserRepository = new UserWriteRepository(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}