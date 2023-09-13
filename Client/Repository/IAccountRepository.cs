using Client.Model;
using Microsoft.AspNetCore.Identity;

namespace Client.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);

        Task<string> SigninAsync(SignInModel signInModel);
    }
}