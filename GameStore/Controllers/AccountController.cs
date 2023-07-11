using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Models;
using GameStore.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;   
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAccount([FromBody] AccountModel model)
        {
            var result = await accountService.RegisterAccountAsync(model);
            
            if(result != null)
                return Ok(result);

            return BadRequest(Warnings.AccountAlreadyExists<Account>());
        }
    }
}
