using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
//using Entities;
using Entities.Models;
using System.Diagnostics.Metrics;
using AutoMapper;
using DTO;
using Microsoft.Identity.Client;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;

        public UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

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
        public async Task<IActionResult> Post([FromBody] FullUserDTO userToAdd)
        {
            User user = _mapper.Map<FullUserDTO, User>(userToAdd);
            //int passwordScore = _userService.CheckPassword(user.Password);
            User newUser = await _userService.AddUser(user);
            ReturnUserDTO usersDTO = _mapper.Map<User, ReturnUserDTO>(newUser);

            if (usersDTO != null ) 
            {
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, usersDTO);
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
        public ActionResult<User> Login([FromBody] LoginUserDTO loginUser )
        {
            User user = _userService.Login(loginUser);
            ReturnUserDTO usersDTO = _mapper.Map<User, ReturnUserDTO>(user);
            if (usersDTO != null)
            {
                return Ok(usersDTO);
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] FullUserDTO userToUpdate)
        {
            User u = _mapper.Map<FullUserDTO, User>(userToUpdate);
            u.UserId = id;

            User user = await _userService.UpdateUser(id, u);
            ReturnUserDTO usersDTO = _mapper.Map<User, ReturnUserDTO>(user);

            if (usersDTO != null)
            {
                return Ok(usersDTO);
            }
            return BadRequest();
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, [FromBody] User userToUpdate)
        //{
        //    User result = await _userService.UpdateUser(id, userToUpdate);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest();
        //}

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
