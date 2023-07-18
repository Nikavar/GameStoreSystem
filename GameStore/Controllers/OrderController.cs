﻿using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.Controllers
{
	[Route("api/")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService orderService;
		private readonly IGameService gameService;

        public OrderController(IOrderService orderService, IGameService gameService)
        {				
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

	}
}
