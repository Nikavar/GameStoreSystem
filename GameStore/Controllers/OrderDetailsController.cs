using GameStore.Service.Models;
using GameStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameStore.Service.Interfaces;
using AutoMapper;
using GameStore.Data.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using GameStore.Model.Models;
using Mapster;

namespace GameStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDetailsController : ControllerBase
	{
		private readonly IOrderService orderService;
		private readonly IOrderDetailsService orderDetailsService;

		public OrderDetailsController(IOrderDetailsService orderDetailsService, IOrderService orderService)
		{
			this.orderService = orderService;
			this.orderDetailsService = orderDetailsService;
		}

	}

}
