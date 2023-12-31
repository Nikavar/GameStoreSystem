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
			var orderItem = currentOrder?.OrderItems?.FirstOrDefault(x => x.GameId == gameId);

			if(orderItem == null)
			{
				OrderItem newOrderItem = new OrderItem();
				newOrderItem.OrderId = orderId;
				newOrderItem.GameId = gameId;
				newOrderItem.ItemCount++;
				newOrderItem.GamePrice = gameToAdd.Price;

				await orderItemRepository.AddAsync(newOrderItem);
			}
   		else
			{
				orderItem.ItemCount++;
				await orderItemRepository.UpdateAsync(orderItem);
			}

			return await GetCurrentOrderAsync(accountId);
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{
			return await orderRepository.AddAsync(order);
		}

		// task 4.2
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


		// case: SignEnum.Minus
		public async Task<Order> RemoveGameFromCardAsync(int? gameId, int? accountId)
		{
			var currentOrder = await GetCurrentOrderAsync(accountId);
			var orderItem = currentOrder?.OrderItems?.FirstOrDefault(x => x.GameId == gameId);

			if (orderItem?.ItemCount == 1)
			{
				await orderItemRepository.DeleteAsync(orderItem);
			}

			else
			{
				orderItem.ItemCount--;
				await orderItemRepository.UpdateAsync(orderItem);
			}

			return await GetCurrentOrderAsync(accountId);
		}

		// case: SignEnum.Close
		public async Task RemoveAllGamesFromCardAsync(int? gameId, int? accountId)
		{
			var currentOrder = await GetCurrentOrderAsync(accountId);
			var orderItem = currentOrder?.OrderItems?.FirstOrDefault(x => x.GameId == gameId);

			if(orderItem != null)
				await orderItemRepository.DeleteAsync(orderItem);
		}

		// task 4.3
		public async Task<Order> UpdateOrderAsync(int? gameId, int? orderId, int accountId, SignEnum sign)
		{

				switch (sign)
				{
					case SignEnum.minus:
						await RemoveGameFromCardAsync(gameId, accountId);
						break;					

					case SignEnum.plus:
						await AddToCardAsync(gameId, orderId, accountId);
						break;

					case SignEnum.close:
						await RemoveAllGamesFromCardAsync(gameId, accountId);
						break;
				}

			return await GetCurrentOrderAsync(accountId);
		}
	}
}
