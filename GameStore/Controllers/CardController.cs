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
	public class CardController : ControllerBase
	{
		private readonly ICardService cardService;

        public CardController(ICardService cardService)
        {				
			this.cardService = cardService;
        }

		// task 4.1

		[HttpPost("Game/{gameId}/AddCard")]
		public async Task<ActionResult> AddGameToCard([FromRoute] int? gameId, [FromBody] CardModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await cardService.AddCardAsync(gameId, model);
				return StatusCode(201, result);
			}
			return BadRequest(400);
		}


		// task 4.2
		[HttpGet]
		public async Task<ActionResult> GetAllOrders()
		{
			var result = await cardService.GetAllCardsAsync();

			if (result != null)
				return StatusCode(201, result);

			return BadRequest(400);
		}

	}
}
