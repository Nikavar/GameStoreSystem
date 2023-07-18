﻿using AutoMapper;
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
		public async Task<ActionResult> AddOrder([FromRoute] int? accountId, [FromBody] OrderModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var result = await orderService.AddOrderAsync(accountId, model);
					return StatusCode(201, result);
				}

				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}				
			}

			return BadRequest(400);
		}
	}
}
