using AutoMapper;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model.Models;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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

        public async Task<Account> LoginAccountAsync(string username, string password)
        {
            if(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                throw new Exception("username or/and password is null or empty!");
            }  
                
            return await accountRepository.LoginAccountAsync(username, password);   

        }

        public async Task<Account> RegisterAccountAsync(AccountModel model)
        {
            // Because email must be unique, I use it to check if the same account is already in DB or not
            var account = await accountRepository.GetManyAsync(X => X.Email.Equals(model.Email));

            // if not, I create a new one!
            if(account.FirstOrDefault() == null)
            {
                var entity = mapper.Map<Account>(model);
                //var result = await accountRepository.RegisterAccountAsync(entity);

                return result;
            }

            throw new Exception("This account is already exists!");
        }

        public Task<Account> RegisterAccountAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
