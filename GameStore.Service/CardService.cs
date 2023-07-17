﻿using AutoMapper;
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
        public async Task<Card> AddCardAsync(CardModel model)
		{
			var entity = mapper.Map<Card>(model);
			entity.OrderCount++;

			return await cardRepository.AddAsync(entity);
		}
		
	}
}
