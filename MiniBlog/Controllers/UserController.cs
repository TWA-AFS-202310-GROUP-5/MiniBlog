using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ArticleService articleService = null!;
        private readonly UserService userService = null!;

        public UserController(ArticleService articleService, UserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            if (this.userService == null)
            {
                return StatusCode(500);
            }

            _ = await userService.CreateUser(user);

            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, GetByName(user.Name));
        }

        [HttpGet]
        public async Task<List<User>> GetAllAsync()
        {
            return await userService.GetAllUsers();
        }

        [HttpPut]
        public async Task<User> Update(User user)
        {
            return await userService.UpdateUser(user);
        }

        [HttpDelete]
        public async Task DeleteAsync(string name)
        {
            await userService.DeleteUserByName(name);
        }

        [HttpGet("{name}")]
        public async Task<User> GetByName(string name)
        {
            return await userService.GetUserByName(name);
        }
    }
}
