using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
	[Route("api/")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {				
			this.orderService = orderService;
        }

		// task 4.5

        [HttpPost("Account/{accountId}/AddOrder")]
		public async Task<ActionResult> AddOrder([FromQuery] int? orderId, [FromBody] OrderModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await orderService.AddOrderAsync(orderId, model);
					return StatusCode(201, result);
			}

			return BadRequest(400);
		}
	}
}
