using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
//using Entities;
using Entities.Models;
using System.Diagnostics.Metrics;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public ActionResult<User> GetUserById(int id)
        //{
        //    User result = _userService.GetUserById(id);
        //    if (result.UserId != null)
        //    {
        //        return Ok(_userService.GetUserById(id));
        //    }
        //    return BadRequest();
        //}

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            //int passwordScore = _userService.CheckPassword(user.Password);
            Task<User> result = _userService.AddUser(user);

            if (result != null ) 
            {
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Password")]
        public ActionResult<int> CheckPassword([FromBody] string password)
        {
            int result = _userService.CheckPassword(password);
            if (result < 0)
            {
                return BadRequest();
            }
            return result;
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("Login")]
        public ActionResult<User> Login([FromQuery] string userName, [FromQuery] string password)
        {
            User result = _userService.Login(userName,password);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User userToUpdate)
        {
            Task<User> result = _userService.UpdateUser(id,userToUpdate);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
