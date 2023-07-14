﻿using AutoMapper;
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

        public async Task<Account> LoginAccountAsync(string username, string password)
        {
            if(string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                throw new NullReferenceException();
            }  
                
            return await accountRepository.LoginAccountAsync(username, password);   

        }

        public async Task<Account> RegisterAccountAsync(AccountModel model)
        {
            // Because email must be unique, I use it to check if the same account is already in DB or not
            var account = await accountRepository.GetManyAsync(X => X.Email.Equals(model.Email));

            // if not, I create a new one!
            if (account.FirstOrDefault() == null)
            {
                if (model.Password != null)
                {
                    var hashedPassword = ComputeSha256Hash(model.Password);
                    model.Password = hashedPassword;

                    var entity = new Account
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = hashedPassword,
                        UserName = model.UserName,
                        AvatarImage = model.AvatarImage
                    };

                    var entity2 = mapper.Map<Account>(entity);

                    var result = await accountRepository.AddAsync(entity);

                    return result;
                }

                throw new NullReferenceException("Your password must not be null");

            }

            throw new Exception("This account is already exists!");
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task UpdateAccountAsync(Account entity)
        {     
            await accountRepository.UpdateAsync(entity);
        }
    }
}
