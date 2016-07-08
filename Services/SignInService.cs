using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoMvc.Models;

namespace TodoMvc.Services
{
    public class SignInService : ISignInService
    {
        private SignInManager<TodoUser> _signInManager;
        private UserManager<TodoUser> _userManager;
        private ILogger _logger;

        public SignInService(
            SignInManager<TodoUser> signInManager,
            UserManager<TodoUser> userManager,
            ILogger<SignInService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> SignIn(TodoUserModel loginUser)
        {
            await EnsureDefaultUserAsync("default@todo.demo", "default", "P@ssw0rd!");
            await EnsureDefaultUserAsync("test@todo.demo", "test", "p@ssW0rd-");

            var signInResult = await _signInManager.PasswordSignInAsync(
                loginUser.Name, loginUser.Password, true, false);

            if (signInResult.Succeeded)
            {
                _logger.LogInformation($"Login user name: {loginUser.Name}");
                return true;
            }

            _logger.LogWarning($"Login failed: {loginUser.Name}");
            return false;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task EnsureDefaultUserAsync(string email, string name, string password)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var newUser = new TodoUser()
                {
                    UserName = name,
                    Email = email
                };

                await _userManager.CreateAsync(newUser, password);
            }
        }
    }
}
