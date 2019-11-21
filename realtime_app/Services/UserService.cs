using System.Collections.Generic;
using System.Threading.Tasks;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;
using System.Web.Helpers;

namespace realtime_app.Services
{
  public class UserService : IUserService
  {
    private readonly RealtimeAwesomeDbContext _context;
    public UserService(RealtimeAwesomeDbContext context)
    {
        _context = context;
    }

    public Task<IList<UserContract>> GetSuggestedFriend(string userId)
    {
      throw new System.NotImplementedException();
    }

    public async Task<UserContract> RegisterUserAsync(RegiserUserContract contract)
    {
      var hashPassword = Crypto.HashPassword(contract.Password);
      
      var user = new User(contract.FirstName, contract.LastName, contract.UserName, hashPassword);
      await _context.Set<User>().AddAsync(user);
      await _context.SaveChangesAsync();

      return new UserContract()
      {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          UserName = user.UserName
      };
    }
  }
}