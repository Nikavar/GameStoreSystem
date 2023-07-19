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

		// task 4.5, 4.6, 4.7
		[HttpPost("Game/{gameId}/CompleteOrder")]
		public async Task<ActionResult> CompleteOrder(int? account, int? orderId, OrderDetailsModel model)
		{
			var isComplete = await orderDetailsService.CompleteOrder(account, orderId, model);

			if(isComplete)
				return Ok("Order is Confirmed!");

			return BadRequest("Something went wrong!");
		}
	}

}
