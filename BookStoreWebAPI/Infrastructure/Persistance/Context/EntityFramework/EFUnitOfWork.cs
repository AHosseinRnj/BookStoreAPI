using Application.Repositpries;
using Infrastructure.Persistance.Repositories;

namespace Infrastructure.Persistance
{
    public class EFUnitOfWork
    {
        private readonly EFContext _context;

        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IOrderItemRepository OrderItemRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IPublisherRepository PublisherRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public EFUnitOfWork(EFContext context)
        {
            _context = context;

            //BookRepository = new BookRepository(_context);
            //AuthorRepository = new AuthorRepository(_context);
            //CategoryRepository = new CategoryRepository(_context);
            //OrderItemRepository = new OrderItemRepository(_context);
            //OrderRepository = new OrderRepository(_context);
            //PublisherRepository = new PublisherRepository(_context);
            //UserRepository = new UserRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}