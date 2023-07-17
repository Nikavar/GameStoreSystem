using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly ICardService cardService;

        public CardController(ICardService cardService)
        {				
			this.cardService = cardService;
        }


        [HttpPost]
		public async Task<ActionResult> AddGameToCard([FromBody] CardModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await cardService.AddCardAsync(model);
				return StatusCode(201, result);
			}
			return BadRequest(400);
		}

	}
}
