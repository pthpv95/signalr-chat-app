using System.Collections.Generic;
using System.Threading.Tasks;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;

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
        var user = new User(contract.FirstName, contract.LastName, contract.UserName);

        var contact = new Contact(user.Id, contract.FirstName, contract.LastName);
        await _context.Set<User>().AddAsync(user);
        await _context.Set<Contact>().AddAsync(contact);
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