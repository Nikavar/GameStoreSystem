using AutoMapper;
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
		private readonly IGameRepository gameRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public OrderService(IOrderRepository orderRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.gameRepository = gameRepository;
			this.orderRepository = orderRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}


		// task 4.1
		public async Task<Order> AddToCardAsync(int? gameId, int? orderId, int accountId)
		{
			// Find Game Add to Order
			var gameToAdd = await gameRepository.GetByIdAsync(gameId);

			// Finds Current Order
			var currentOrder = await GetCurrentOrderAsync(accountId);

			// Creates new OrderItem to Add in Order
			OrderItemModel orderItem = new OrderItemModel();

			orderItem.OrderId = orderId;
			orderItem.GameId = gameId;
			orderItem.ItemCount = orderItem.ItemCount + 1;
			orderItem.GamePrice = gameToAdd.Price;

			// Checks Current Order and if it is out of date, creates new one:

			if (currentOrder.DateCompleted < DateTime.Now)
			{
				// Created new Order and Adds new Data into it
				OrderModel newOrder = new OrderModel();
				newOrder.AccountId = accountId;				
				newOrder.OrderItems.Add(orderItem);

				currentOrder = await CreateOrderAsync(newOrder);
			}

			else
			{
				// if Current Order is Active, then:
				var OrderModel = mapper.Map<OrderModel>(currentOrder);

				// Cheks Order Item exists in it or not: Yes => Increases only its count, No => Adds as new OrderItem 
				if (OrderModel.OrderItems.Contains(orderItem))
				{
				    orderItem.ItemCount++;
					await UpdateOrderAsync(OrderModel);
				}

				else
				{
					OrderModel.AccountId = accountId;
					OrderModel.OrderItems.Add(orderItem);
					await UpdateOrderAsync(OrderModel);
				}
			}

			return currentOrder;
		}

		public async Task UpdateOrderAsync(OrderModel model)
		{
			await orderRepository.UpdateAsync(mapper.Map<Order>(model));	
		}

		public async Task<Order> CreateOrderAsync(OrderModel model)
		{
			var entity = mapper.Map<Order>(model);
			return await orderRepository.AddAsync(entity);
		}

		public async Task<Order> GetCurrentOrderAsync(int? accountId)
		{
			var order = await orderRepository.GetManyAsync(x => x.AccountId == accountId);
			return order.FirstOrDefault();
		}

	}
}
