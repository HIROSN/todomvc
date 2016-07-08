using System.Threading.Tasks;
using TodoMvc.Models;

namespace TodoMvc.Services
{
    public interface ISignInService
    {
        Task<bool> SignIn(TodoUserModel loginUser);
        Task SignOut();
    }
}
