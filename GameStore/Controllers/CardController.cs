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
		public async Task<ActionResult> AddGameToCard([FromQuery] int? gameId, [FromBody] CardModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var result = await cardService.AddCardAsync(gameId, model);
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
