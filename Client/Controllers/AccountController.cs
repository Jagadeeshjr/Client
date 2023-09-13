using Client.Model;
using Client.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signUpModel) 
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded) 
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin(SignInModel signInModel)
        {
            var result = await _accountRepository.SigninAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
