using Ilex.Server.Services;
using Ilex.Shared.Enums;
using Ilex.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ilex.Server.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {


        private readonly EmailService _emailService;

        public TestController(EmailService emailService)
        {
            _emailService = emailService;
        }

        // GET: api/<TestController>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            return Ok(new ApiResponse<List<int>>
            {
                
                Content = new List<int> { rand.Next(1,10), rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10), rand.Next(1, 10) },
                Message =  "Get is Success" ,
                ResponseStatus = ResponseStatus.Success,
                Success = true
            }) ;
        }

        // GET api/test/get/5
        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(int id)
        {

            return Ok(new ApiResponse<string>
            {
                Content = "Tina",
                Message = "Get Id is Success",
                ResponseStatus = ResponseStatus.Success,
                Success = true
            });
        }
            


         // POST api/test/post
         [HttpPost]
         [Route("Post")]
        public IActionResult Post([FromBody] string value)
        {
            return Ok(new ApiResponse
            {
                Message = "Post value is Success" ,
                ResponseStatus = ResponseStatus.Success,
                Success = true
            });
        }

        // PUT api/test/put
        [HttpPut]
        [Route("Put")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(new ApiResponse
            {
                Message = "Put Id with value is Success",
                ResponseStatus = ResponseStatus.Success,
                Success = true
            });
        }

        // DELETE api/test/delete/id
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(new ApiResponse
            {
                Message = "Delete Id with value is Success" ,
                ResponseStatus = ResponseStatus.Success,
                Success = true
            });
        }

        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> SendEmail()
        {
            var result = await _emailService.SendEmailAsync("mustac.marijan@gmail.com","<h1>This is message from the controller - Test -</h1>","Test Message");

            return result?
                Ok(new ApiResponse
            {
                Message = "Mail Sent",
                    ResponseStatus = ResponseStatus.EmailSent,
                    Success = true
            }):
            BadRequest(new ApiResponse
            {
                Message = "Mail could not be sent",
                ResponseStatus = ResponseStatus.EmailSendingError,
                Success = true
            });

        }
    }
}
