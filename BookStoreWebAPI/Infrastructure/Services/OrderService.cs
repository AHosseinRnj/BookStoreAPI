using Application;
using Application.Command.CreateOrder;
using Application.Query.GetOrder;
using Application.Repositpries;
using Application.Services;
using Domain.Entities;
using log4net;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _logger = LogManager.GetLogger(typeof(OrderService));
        }

        public async Task AddAsync(CreateOrderCommand request)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to add an Order.");

                var order = new Order
                {
                    Id = request.id,
                    UserId = request.userId,
                    OrderDate = request.orderDate
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

        public async Task<GetOrderQueryResponse> GetOrderByIdAsync(int id)
        {
            GetOrderQueryResponse result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get an Order by ID: " + id);

                var order = await _orderRepository.GetOrderByIdAsync(id);

                result = new GetOrderQueryResponse
                {
                    OrderDate = order.OrderDate,
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting an Order: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Order retrieved successfully.");
            return result;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync()
        {
            List<GetOrderQueryResponse> result;

            try
            {
                _unitOfWork.BeginTransaction();
                _logger.Info("Received a request to get all Orders");

                var listOfOrders = await _orderRepository.GetOrdersAsync();

                result = listOfOrders.Select(o => new GetOrderQueryResponse
                {
                    OrderDate = o.OrderDate
                }).ToList();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error("Error getting Orders: " + ex.Message, ex);
                throw;
            }
            finally
            {
                _unitOfWork.Commit();
            }

            _logger.Info("Orders retrieved successfully.");
            return result;
        }
    }
}