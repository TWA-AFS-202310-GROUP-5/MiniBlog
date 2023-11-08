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
        //private readonly ArticleStore articleStore = null!;
        //private readonly UserStore userStore = null!;
        private readonly UserService userService = null!;
        /*
        public UserController(ArticleStore articleStore, UserStore userStore, ArticleService articleService)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.articleService = articleService;
        }
        */
       
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            //if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
            //{
            //    userStore.Users.Add(user);
            //}
            var addedUser = await userService.CreateUser(user);
            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, addedUser);
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await userService.GetAll();
        }

        [HttpGet("{name}")]
        public async Task<User?> GetByName(string name)
        {
            return await userService.GetByName(name);
        }

        /*
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
        public User Delete(string name)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleService.DeleteByName(foundUser.Name);
            }

            return foundUser;
        }*/
    }
}