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
                ResponseCode = System.Net.HttpStatusCode.OK,
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
                ResponseCode = System.Net.HttpStatusCode.OK,
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
                ResponseCode = System.Net.HttpStatusCode.OK,
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
                ResponseCode = System.Net.HttpStatusCode.OK,
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
                ResponseCode = System.Net.HttpStatusCode.OK,
                Success = true
            });
        }
    }
}
