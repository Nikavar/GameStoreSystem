using GameStore.Model.Models;
using GameStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.Interfaces
{
    public interface ICardService
	{
		Task<IEnumerable<Card>> GetAllCardsAsync();
		Task<IEnumerable<Card>> GetManyCardsAsync(Expression<Func<Card, bool>> filter);
		Task<Card> AddCardAsync(int? gameId, CardModel entity);
		Task UpdateCardAsync(int cardId, CardModel entity);
		Task<CardModel> GetCardById(int? cardId);

	}
}
