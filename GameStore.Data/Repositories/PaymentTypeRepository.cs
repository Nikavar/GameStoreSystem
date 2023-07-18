using GameStore.Data.Infrastructure;
using GameStore.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Repositories
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>, IPaymentTypeRepository
	{
        public PaymentTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        
        }
    }

    public interface IPaymentTypeRepository : IBaseRepository<PaymentType>
    {

    }
}