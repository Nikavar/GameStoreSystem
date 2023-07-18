﻿using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository orderRepository;
		private readonly IOrderItemRepository orderItemRepository;
		private readonly IGameRepository gameRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public OrderService(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.orderItemRepository = orderItemRepository;
			this.gameRepository = gameRepository;
			this.orderRepository = orderRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}


		// task 4.1
		public async Task<Order> AddToCardAsync(int? gameId, int? orderId, int accountId)
		{
			// Finds Game Add to Order
			var gameToAdd = await gameRepository.GetByIdAsync(gameId);

			// Finds Current Order
			var currentOrder = await GetCurrentOrderAsync(accountId);

			// Creates new OrderItem to Add in Order
			OrderItemModel orderItem = new OrderItemModel();

			orderItem.OrderId = orderId;
			orderItem.GameId = gameId;
			orderItem.ItemCount++;
			orderItem.GamePrice = gameToAdd.Price;

			var entity = mapper.Map<OrderItem>(orderItem);

			// Checks Order contains New OrderItem or not:

			if(currentOrder.OrderItems.Contains(entity))
			{
				await orderItemRepository.UpdateAsync(entity);
			}

            else
            {
				await orderItemRepository.AddAsync(entity);
            }

			return currentOrder;
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{
			return await orderRepository.AddAsync(order);
		}

		public async Task<Order> GetCurrentOrderAsync(int? accountId)
		{
			var order = await orderRepository.GetManyAsync(x => x.AccountId == accountId && x.DateCompleted == null);
			var currOrder = order.FirstOrDefault();

			if(order == null)
			{
				Order newOrder = new Order();
				newOrder.AccountId = accountId;

				newOrder = await CreateOrderAsync(newOrder);
				currOrder = newOrder;
			}

			return currOrder;
		}
		public async Task UpdateOrderAsync(OrderModel model)
		{
			await orderRepository.UpdateAsync(mapper.Map<Order>(model));	
		}

	}
}
