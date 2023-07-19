using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GameStore.Model.Models;
using Mapster;

namespace GameStore.Controllers
{
	[Route("api/")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService orderService;
		private readonly IOrderDetailsService orderDetailsService;
		private readonly IGameService gameService;

        public OrderController(IOrderDetailsService orderDetailsService, IOrderService orderService, IGameService gameService)
        {
			this.orderDetailsService = orderDetailsService;			
			this.orderService = orderService;
			this.gameService = gameService;
        }


		// task 4.1

		[HttpPost("Game/{gameId}/AddToOrder")]
		public async Task<ActionResult> AddToOrder([FromRoute] int? gameId, [FromQuery] int? orderId)
		{
			try
			{
				var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
				int accountId = Convert.ToInt32(userId);

				var result = await orderService.AddToCardAsync(gameId,orderId,accountId);

				return StatusCode(201, result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// task 4.2

		[HttpPost("Game/{gameId}/MyCard")]
		public async Task<ActionResult> GetCardItems()
		{
			var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			int accountId = Convert.ToInt32(userId);

			var order = await orderService.GetCurrentOrderAsync(accountId);

			if (order != null)
				return Ok(order);

			return BadRequest("Something went wrong");

		}

		// task 4.3

		[HttpPut("Game/{gameId}/UpdateOrder/{sign}")]
		public async Task<ActionResult> UpdateOrder([FromRoute] int? gameId, [FromQuery] int? orderId, [FromRoute] SignEnum sign)
		{
			var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
			int accountId = Convert.ToInt32(userId);

			var result = await orderService.UpdateOrderAsync(gameId, orderId, accountId, sign);

			return Ok(result);
		}

		// task 4.4

		[HttpGet("Game/{gameId}/MyCard/")]
		public async Task<ActionResult> GetCurrentOrder(int? accountId)
		{
			var order = await orderService.GetCurrentOrderAsync(accountId);
			return Ok(order);
		}
	}
}
