using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using GameStore.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            this.accountService = accountService;   
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAccount([FromBody] AccountModel model)
        {
            var result = await accountService.RegisterAccountAsync(model);
            
            if(result != null)
                return Ok(result);

            return BadRequest(Warnings.AccountAlreadyExists<Account>());
        }

        [HttpPost]
        public async Task<ActionResult> LoginAccount(string username, string password)
        {
            var result = await accountService.LoginAccountAsync(username, password);

            if (result != null)
            {
                var token = Helper.TokenGeneration(result, configuration);
                HttpContext.Response.Cookies.Append("Token", token);
                return Ok(result);
            }
            return BadRequest("your username or/and password is wrong!"); 
        }
    }
}
