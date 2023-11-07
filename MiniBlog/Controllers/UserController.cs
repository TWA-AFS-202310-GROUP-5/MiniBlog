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
        private readonly UserStore userStore = null!;

        public UserController(ArticleService articleService, UserStore userStore)
        {
            this.articleService = articleService;
            this.userStore = userStore;
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (this.userStore == null)
            {
                return StatusCode(500);
            }

            if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            {
                userStore.Users.Add(user);
            }

            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, GetByName(user.Name));
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userStore.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
            if (foundUser != null)
            {
                foundUser.Email = user.Email;
            }

            return foundUser;
        }

        [HttpDelete]
        public async Task<User> DeleteAsync(string name)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                await articleService.DeleteAllByUserName(foundUser.Name);
            }

            return foundUser;
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
        }
    }
}
