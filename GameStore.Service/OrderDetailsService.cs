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
	public class OrderDetailsService : IOrderDetailsService
	{
		private readonly IOrderDetailsRepository orderDetailsRepository;
		private readonly IOrderRepository orderRepository;
		private readonly IOrderService orderService;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public OrderDetailsService(IOrderService orderService, IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.orderService = orderService;
			this.orderRepository = orderRepository;
			this.orderDetailsRepository = orderDetailsRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}


		// task 4.5
		public async Task<bool> CompleteOrder(int? accountId, int? orderId, OrderDetailsModel model)
		{			
			var currentOrder = await orderService.GetCurrentOrderAsync(accountId);
			
			if(currentOrder != null && currentOrder.DateCompleted == null)
			{
				OrderDetails orderDetails = new OrderDetails();
				orderDetails.OrderId = model.OrderId;
				orderDetails.FirstName = model.FirstName;
				orderDetails.LastName = model.LastName;
				orderDetails.Phone = model.Phone;
				orderDetails.Email = model.Email;
				orderDetails.PaymentTypeId = model.PaymentTypeId;
				orderDetails.Comments = model.Comments;

				await orderDetailsRepository.AddAsync(orderDetails);
				currentOrder.DateCompleted = DateTime.Now;
				await orderRepository.UpdateAsync(currentOrder);

				return true;
			}

			return false;
		}

	}
}
