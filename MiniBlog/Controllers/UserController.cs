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
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly UserService userService = null!;

        public UserController(ArticleStore articleStore, UserStore userStore, UserService userService)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<User> Register(User user)
        {
            User result = null;
            var exit = await userService.IsUserAlreadyExit(user.Name);
            if (!exit)
            {
                result = await userService.CreateUserAsync(user);
            }

            return result;
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await userService.GetAllAsync();
        }

        [HttpPut]
        public User Update(User user)
        {
            if (user != null)
            {
                userService.Update(user);
                return user;
            }

            return null;
        }

        [HttpDelete]
        public User Delete(string name)
        {
            var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
            if (foundUser != null)
            {
                userStore.Users.Remove(foundUser);
                articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
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
