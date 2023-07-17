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
	public class CardService : ICardService
	{
		private readonly ICardRepository cardRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.cardRepository = cardRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
        }

		// task 4.1
		public async Task<Card> AddCardAsync(int? gameId, CardModel model)
		{
			model.GameId = gameId;
			decimal gamePrice = 0;

			var game = await gameRepository.GetByIdAsync(gameId);
			if (game != null)
			{
				gamePrice = game.Price;
			}

			else
			{
				throw new Exception($"this Game is not in Store");
			}

			var entity = mapper.Map<Card>(model);
			var result = await GetManyCardsAsync(x => x.OrderId == model.OrderId && x.GameId == model.GameId);
			var order = result.FirstOrDefault();

			if (order != null)
			{
				order.OrderCount++;
				order.TotalAmount += gamePrice;
				return await cardRepository.AddAsync(order);
			}

			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Card>> GetManyCardsAsync(Expression<Func<Card, bool>> filter)
		{
			return await cardRepository.GetManyAsync(filter);
		}
	}
}
