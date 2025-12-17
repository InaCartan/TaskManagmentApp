using DTO;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserBLL user;

        public UserController(UserBLL user)
        {
            this.user = user;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return Ok(user.GetAllUsers());
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var u = user.GetUserById(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<UserDto> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null) return BadRequest();

            userDto.AssignedTasks ??= new List<TaskItemDto>();

            user.AddUser(userDto);

            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null || id != userDto.Id) return BadRequest();

            var existing = user.GetUserById(id);
            if (existing == null)
            {
                // upsert behavior in repo: creates if missing
                user.UpdateUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
            }

            user.UpdateUser(userDto);
            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existing = user.GetUserById(id);
            if (existing == null) return NotFound();

            user.DeleteUser(id);
            return NoContent();
        }
    }
}
