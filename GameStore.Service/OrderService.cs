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
		private readonly IAccountRepository accountRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public OrderService(IAccountRepository accountRepository, IOrderRepository orderRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
			this.gameRepository = gameRepository;
            this.orderRepository = orderRepository;
			this.accountRepository = accountRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
        }


		// task 4.5
		public async Task<Order> AddOrderAsync(int? accountId, OrderModel model)
		{
			var account = await accountRepository.GetByIdAsync(accountId);

			model.FirstName = account.FirstName;
			model.LastName = account.LastName;
			model.Email = account.Email;
			model.AccountId = accountId;

			var order = await orderRepository.AddAsync(mapper.Map<Order>(model));
			return order;
		}
	}
}
