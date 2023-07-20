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

	}
}
