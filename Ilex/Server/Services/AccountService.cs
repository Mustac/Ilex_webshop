using Ilex.Server.Data;
using Ilex.Server.Services.Contracts;
using Ilex.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilex.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;
        public AccountService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckIfUserExistByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return false;

            return true;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;

            return user;
        }
    }
}
