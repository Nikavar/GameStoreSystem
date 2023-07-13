using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Account> RegisterAccountAsync(AccountModel model)
        {
            // Because email must be unique, I use it to check if the same account is already in DB or not
            var account = await accountRepository.GetManyAsync(X => X.Email.Equals(model.Email));

            // if not, I create a new one!
            if(account.FirstOrDefault() == null)
            {
                var entity = mapper.Map<Account>(model);
                entity.Password = SHA256.Create(entity.Password).ToString();
                var result = await accountRepository.RegisterAccountAsync(entity);

                return result;
            }

            throw new Exception("This account is already exists!");
        }
    }

    public interface IAccountService
    {
        Task<Account> RegisterAccountAsync(AccountModel model);

    }
}
