using AutoMapper;
using Ilex.Server.Data;
using Ilex.Server.Services.Contracts;
using Ilex.Server.Statics;
using Ilex.Shared.Enums;
using Ilex.Shared.Helpers;
using Ilex.Shared.ModelDTOs.Account;
using Ilex.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
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
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        public AccountController(AppDbContext db, IAccountService accountService, IMapper mapper, IConfiguration config, IEmailService emailService)
        {
            _db = db;
            _accountService = accountService;
            _mapper = mapper;
            _config = config;
            _emailService = emailService;
        }


        [HttpGet]
        [Route("get/{id?}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            try
            {
                var user = await _db.Users.FindAsync(Id);

                if (user == null)
                {
                    return BadRequest(new ApiResponse<UserDTO>
                    {
                        Error = "Greška, Pokušajte se odjaviti i prijaviti",
                        ResponseStatus = ResponseStatus.Error,
                        Success = false
                    });
                }

                var userDTO = _mapper.Map<UserDTO>(user);

                return Ok(new ApiResponse<UserDTO>
                {
                    Content = userDTO,
                    Message = $"Uspjeh",
                    ResponseStatus = ResponseStatus.Success,
                    Success = true
                }); ;
            }
            catch
            {
                return BadRequest(new ApiResponse<UserDTO>
                {
                    Error = $"Greška na serveru",
                    ResponseStatus = ResponseStatus.ServerError,
                    Success = false
                });
            }




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
                        ResponseStatus = ResponseStatus.EmailIsUnavailable,
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

                var savedToDbSuccess = await _db.SaveChangesAsync() > 0;

                user = await _accountService.GetUserByEmailAsync(user.Email);

                var code = await _accountService.GenerateAccountConfirmationToken(user);

                string email = "<span style='border: 1px solid darkblue; padding: 10px 25px; border - radius: 10px; '> " + code + " </span>";

                if (savedToDbSuccess)
                {
                    await _emailService.SendEmailAsync(user.Email, email, "Confirmation Mail");
                }

                return savedToDbSuccess ?
                     Ok(new ApiResponse
                     {
                         Message = $"Korisnik {userModel.FirstName} je sada registiran",
                         ResponseStatus = ResponseStatus.Success,
                         Success = true
                     }) :
                     BadRequest(new ApiResponse
                     {
                         Error = $"Korisnik nije mogao biti registriran",
                         ResponseStatus = ResponseStatus.Error,
                         Success = false
                     });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Error = $"Greška na serveru",
                    ResponseStatus = ResponseStatus.ServerError,
                    Success = false
                });
            }



        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginDTO userModel)
        {
            try
            {
                var user = await _accountService.GetUserByEmailAsync(userModel.Email);

                if (user == null)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Error = "Korisnik sa email ne postoji",
                        ResponseStatus = ResponseStatus.EmailOrPasswordAreWrong,
                        Success = false
                    });
                }

                if (user.EmailVerified == false)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Error = "Račun nije aktiviran",
                        ResponseStatus = ResponseStatus.AccountNotActivated,
                        Success = false
                    });
                }

                var correctPassword = BCrypt.Net.BCrypt.Verify(userModel.Password, user.HashedPassword);

                if (!correctPassword)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Error = "Email ili password nisu točni",
                        ResponseStatus = ResponseStatus.EmailOrPasswordAreWrong,
                        Success = false
                    });
                }

                var token = JwtToken.CreateJwtToken(user, _config["JwtSecurityKey"], _config["JwtIssuer"], _config["JwtAudience"]);

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Error = "Greška na serveru",
                        ResponseStatus = ResponseStatus.ServerError,
                        Success = false
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Message = "Uspjeh",
                    ResponseStatus = ResponseStatus.Success,
                    Success = true,
                    Content = token
                });
            }
            catch
            {

                return BadRequest(new ApiResponse<string>
                {
                    Error = "Greška na serveru",
                    ResponseStatus = ResponseStatus.ServerError,
                    Success = false
                });

            }

        }

        [HttpGet]
        [Route("sendemailconfirmation/{email}")]
        public async Task<IActionResult> EmailConfirmationAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Error = "Pogrešan Email, Račun ne postoji",
                    ResponseStatus = ResponseStatus.Error,
                    Success = false
                });
            }

            if (user.EmailVerified)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Error = "Račun je već aktiviran",
                    ResponseStatus = ResponseStatus.Error,
                    Success = false
                });
            }

            var token = await _accountService.GenerateAccountConfirmationToken(user);

            string emailToSend = "<span style='border: 1px solid darkblue; padding: 10px 25px; border - radius: 10px; '> " + token + " </span>";

            if (token != null)
            {
                var response = await _emailService.SendEmailAsync(user.Email, emailToSend, "Confirmation Mail");
                if (response)
                {
                    return Ok(new ApiResponse
                    {
                        Message = "Email sa je uspješno poslan",
                        ResponseStatus = ResponseStatus.Success,
                        Success = true,

                    });
                }
            }

            return BadRequest(new ApiResponse<string>
            {
                Error = "Greška putem slanja Emaila",
                ResponseStatus = ResponseStatus.EmailSendingError,
                Success = false
            });


        }



    }
}
