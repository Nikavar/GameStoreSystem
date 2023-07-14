using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

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

            if (result != null)
                return Ok(result);

            return BadRequest(Warnings.AccountAlreadyExists<Account>());
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAccount([Required] string username, [Required] string password)
        {
            try
            {
                var result = await accountService.LoginAccountAsync(username, password);

                if (result != null)
                {
                    var token = Helper.TokenGeneration(result, configuration);
                    HttpContext.Response.Cookies.Append("Token", token);
                    return Ok(result);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("your username or/and password is wrong!");
        }
    }
}
