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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateAccountConfirmationToken(User user)
        {
            string token = GenerateRandomToken();

            var tokenRegistrated = _db.UserAccountTokens.Where(x => x.UserId == user.Id).FirstOrDefault(x => x.TokenUsedFor == Shared.Enums.TokenusedFor.UserConfirmation);

            if (tokenRegistrated != null)
                _db.UserAccountTokens.Remove(tokenRegistrated);

            UserAccountToken userToken = new UserAccountToken()
            {
                Code = token,
                DateCreated = DateTime.Now,
                TokenUsedFor = Shared.Enums.TokenusedFor.UserConfirmation,
                UserId = user.Id,

            };

            await _db.UserAccountTokens.AddAsync(userToken);

            return await _db.SaveChangesAsync() > 0?userToken.Code:null;

        }

        private string GenerateRandomToken()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            string code = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                code += rand.Next(1, 9).ToString();
            };

            return code;

        }

        //public async Task<bool> VerifyAccountConfirmationToken()
    }
}
