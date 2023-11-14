using Application.Repositories;

namespace Infrastructure.Persistance
{
    public class EFUnitOfWork
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