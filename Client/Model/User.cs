using Microsoft.AspNetCore.Identity;

namespace Client.Model
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }
    }
}
