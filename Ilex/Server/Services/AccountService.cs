using Ilex.Server.Data;
using Ilex.Server.Services.Contracts;
using Ilex.Shared.Enums;
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
        /// Generating Account Confirmation Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token as string or null</returns>
        public async Task<string> GenerateAccountConfirmationToken(User user)
        {
            string token = GenerateRandomToken();

            var tokenRegistrated = await _db.UserAccountTokens.Where(x => x.UserId == user.Id).Where(x => x.TokenUsedFor == Shared.Enums.TokenusedFor.UserConfirmation).ToListAsync();

            if (tokenRegistrated.Count>1)
                _db.UserAccountTokens.RemoveRange(tokenRegistrated);

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

        public async Task<bool> VerifyingAccountConfirmationToken(User user, string token)
        {
            var result = await _db.UserAccountTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync(x => x.TokenUsedFor == TokenusedFor.UserConfirmation);

            if (result == null)
            {
                return false;
            }

            var verifyToken = result.Code == token;

            if (!verifyToken)
                return false;
                
            user.EmailVerified = true;
            
            _db.UserAccountTokens.Remove(result);
            _db.Users.Update(user);

            return await _db.SaveChangesAsync() > 0;

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
