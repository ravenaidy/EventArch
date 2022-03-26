using System.Threading.Tasks;
using LoginService.Models;

namespace LoginService.Repositories.Contracts
{
    public interface ILoginRepository
    {
        Task RegisterLogin(Login login);
        Task<bool> UserNameExists(string userName);
    }
}