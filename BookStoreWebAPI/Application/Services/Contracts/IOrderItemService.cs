﻿using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;

namespace Application.Services
{
    public interface IOrderItemService
    {
        Task AddAsync(CreateOrderItemCommand request);
        Task<IEnumerable<GetOrderItemQueryResponse>> GetOrderBooksAsync();
    }
}