using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Repositories
{
	public class CardRepository : BaseRepository<Card>, ICardRepository
	{
		public CardRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
	}

	public interface ICardRepository : IBaseRepository<Card>
	{

	}
}
