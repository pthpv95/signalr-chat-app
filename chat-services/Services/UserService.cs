using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using realtime_app.Contracts;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
  public class UserService : IUserService
  {
    private readonly ChatDbContext _context;

    public UserService(ChatDbContext context)
    {
      _context = context;
    }

    public Task<IList<UserContract>> GetSuggestedFriend(string userId)
    {
      throw new System.NotImplementedException();
    }

    public async Task<UserContract> GetUserDetails(Guid id)
    {
      var user = await _context.Set<User>().SingleAsync(u => u.Id == id);
      return new UserContract
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        AbbreviatedName = user.FirstName[0].ToString().ToUpper() + user.LastName[0].ToString().ToUpper()
      };
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