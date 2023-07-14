using GameStore.Model.Models;
using GameStore.Service;
using GameStore.Service.Interfaces;
using GameStore.Service.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GameStore.Service;


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
                var token = Helper.TokenGeneration(result, configuration);
                HttpContext.Response.Cookies.Append("Token", token);

                if (rememberMe)
                {
                    result.RememberMe = true;
                    await accountService.UpdateAccountAsync(result);
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


        // task2.6 To_Do

        [HttpGet("Profile")]
        public async Task<ActionResult> GetProfileInfo([FromQuery]string username, string password)
        {
            //ანუ აქ ქუქიშ ისეამოწმე არის თუ არა ავტორიზებული, თუ არაა - დააბრუნე Not Authorized კოდი, 
            //თუ არის -var model = await accountService.GetAccountByIdAsync(result.Id);
            //return Ok(model);
            //ლოგიკა ატრიბუტში გადაიტანე
            //და ეგ ატრიბუტი დაადე
            //კომენტარის დამატებას, logout, ს ამას და ა.შ.

            var result = await accountService.LoginAccountAsync(username, password);

            if (result != null)
            {
                var model = await accountService.GetAccountByIdAsync(result.Id);
                return Ok(model);
            }

            return NotFound();
        }


        [HttpPut("LogOut")]
        public async Task<ActionResult> LogOut([FromBody] AccountModel model)
        {
            HttpContext.Response.Cookies.Delete("Token");
            model.RememberMe = false;
            await accountService.UpdateAccountAsync(model.Adapt<Account>()); 

            return Ok();
        }

        // task 2.7

        [HttpPut("AddAvatarToAccount")]
        public async Task<ActionResult> AddAvatarToAccount([FromBody] AccountModel model)
        {
            if(model.AvatarImage != null)
            {
                await accountService.AddAvatarToAccount(model);
                return Ok();
            }

            return BadRequest("You must to attach an Avatar Picture");
        }
    }
}
