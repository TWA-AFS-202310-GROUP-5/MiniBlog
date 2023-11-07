using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("/article")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private ArticleService articleService = null!;

        public ArticleController(ArticleStore articleStore, UserStore userStore, ArticleService articleService)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<List<Article>> ListAsync()
        {
            return await articleService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Article article)
        {
            if (this.articleStore == null)
            {
                return StatusCode(500);
            }

            Article createdAriticle = await articleService.CreateNewArticleAsync(article);

            return CreatedAtAction(nameof(GetById), new { id = createdAriticle.Id }, createdAriticle);
        }

        [HttpGet("{id}")]
        public Article GetById(string id)
        {
            return articleService.FindArticleById(id);
        }
    }
}
