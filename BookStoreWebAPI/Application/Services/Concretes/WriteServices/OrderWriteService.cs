using Application.Command.CreateOrder;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Services;
using log4net;

namespace Application.Services
{
    public class OrderWriteService : IOrderWriteService
    {
        private readonly ILog _logger;
        private readonly IDapperUnitOfWork _unitOfWork;
        private readonly IOrderWriteRepository _orderRepository;
        public OrderWriteService(IDapperUnitOfWork unitOfWork, IOrderWriteRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = LogManager.GetLogger(typeof(OrderWriteService));
        }

        public async Task AddAsync(CreateOrderCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an Order.");

                var order = new Order
                {
                    UserId = request.UserId,
                    OrderDate = request.OrderDate
                };

                await _orderRepository.AddAsync(order);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error adding an Order: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Order added successfully.");
        }
    }
}