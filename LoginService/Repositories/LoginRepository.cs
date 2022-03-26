using System.Threading.Tasks;
using Infrastructure.Connection.Contracts;
using Infrastructure.Repository;
using LoginService.Models;
using LoginService.Repositories.Contracts;

namespace LoginService.Repositories
{
    public class LoginRepository : DapperBaseRepository, ILoginRepository
    {
        public LoginRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
            
        }

        public async Task RegisterLogin(Login login)
        {
            var parameters = new {UserName = login.Username, login.Password };
            const string spName = "pr_CreateAccount";
            await ExecuteAsync(spName, parameters);
        }
        
        public async Task<bool> UserNameExists(string userName)
        {
            var parameters = new { UserName = userName};
            const string spName = "pr_AccountExists";
            return await QueryFirstOrDefaultAsync<int>(spName, parameters) > 0;
        }
    }
}