using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
	public class PaymentTypeService : IPaymentTypeService
	{
		private readonly IPaymentTypeRepository paymentTypeRepository;
		private readonly IDbFactory dbFactory;
		private readonly IUnitOfWork unitOfWork;

        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository, IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
		    this.paymentTypeRepository = paymentTypeRepository;
			this.dbFactory = dbFactory;
			this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypesAsync()
		{
			return await paymentTypeRepository.GetAllAsync();
		}
	}
}
