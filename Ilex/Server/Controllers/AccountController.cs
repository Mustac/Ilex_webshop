using AutoMapper;
using Ilex.Server.Data;
using Ilex.Server.Services.Contracts;
using Ilex.Shared.Helpers;
using Ilex.Shared.ModelDTOs.Account;
using Ilex.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ilex.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(AppDbContext db, IAccountService accountService, IMapper mapper)
        {
            _db = db;
            _accountService = accountService;
            _mapper = mapper;
        }


        // GET: api/<ValuesController>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUserAsync(UserRegistrationDTO userModel)
        {
            try
            {
                if (await _accountService.CheckIfUserExistByEmailAsync(userModel.Email))
                {
                    return BadRequest(new ApiResponse
                    {
                        Error = "Korisnik sa email adresom već postoji",
                        ResponseCode = System.Net.HttpStatusCode.BadRequest,
                        Success = false
                    });
                }

                var user = new User();

                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Email = userModel.Email;
                user.EmailVerified = false;
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.ConfirmPassword);
                user.Phone = userModel.Phone;
                user.Street = userModel.Street;
                user.PostNum = userModel.PostNum;
                user.City = userModel.City;
                user.DateCreated = DateTime.Now;
                user.Role = "User";

                await _db.Users.AddAsync(user);

                return await _db.SaveChangesAsync() > 0 ?
                     Ok(new ApiResponse
                     {
                         Message = $"Korisnik {userModel.FirstName} je sada registiran",
                         ResponseCode = System.Net.HttpStatusCode.OK,
                         Success = true
                     }) :
                     BadRequest(new ApiResponse
                     {
                         Error = $"Korisnik nije mogao biti registriran",
                         ResponseCode = System.Net.HttpStatusCode.BadRequest,
                         Success = false
                     });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Error = $"Greška na serveru",
                    ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false
                });
            }



        }
    }
}
