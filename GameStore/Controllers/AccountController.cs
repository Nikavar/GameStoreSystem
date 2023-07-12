using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using GameStore.Statics;
using Mapster;
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

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAccount([FromBody] AccountModel model)
        {
            var result = await accountService.RegisterAccountAsync(model);
            
            if(result != null)
                return Ok(result);

            return BadRequest(Warnings.AccountAlreadyExists<Account>());
        }

        [HttpGet("Login")]
        public async Task<ActionResult> LoginAccount([FromQuery]string username, string password, bool rememberMe)
        {
            var result = await accountService.LoginAccountAsync(username, password);

            if (result != null)
            {
                if (rememberMe)
                {
                    var token = Helper.TokenGeneration(result, configuration);
                    HttpContext.Response.Cookies.Append("Token", token);
                    result.RememberMe = true;
                    await accountService.UpdateAccountAsync(result.Adapt<AccountModel>());
                }

                // task 2.6 _ First name and Last name are displayed on Site

                // I return the logged account in "result", so it is an easy way 
                // to get FirstName & LastName from this object, for instance:

                var firstName = result.FirstName;
                var lastName = result.LastName;

                return Ok(result);
            }
            return BadRequest("your username or/and password is wrong!"); 
        }

        [HttpPut]
        public async Task<ActionResult> LogOut([FromBody] AccountModel model)
        {
            HttpContext.Response.Cookies.Delete("Token");
            model.RememberMe = false;
            await accountService.UpdateAccountAsync(model); 

            return Ok();
        }
    }
}
