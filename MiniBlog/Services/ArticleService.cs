using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;

namespace MiniBlog.Services;

public class ArticleService
{
    private readonly IArticleRepository articleRepository = null!;
    private readonly IUserRepository userRepository = null!;

    public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository)
    {
        this.articleRepository = articleRepository;
        this.userRepository = userRepository;
    }

    public async Task<Article> CreateArticle(Article article)
    {
        var existUser = await userRepository.GetUserByName(article.UserName);
        if (existUser == null)
        {
            var newUser = new User
            {
                Name = article.UserName,
            };

            var result = await userRepository.CreateUser(newUser);
        }
        
        var createdArticle = await articleRepository.CreateArticle(article);
        return createdArticle;
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetByIdAsync(string id)
    {
        return await articleRepository.GetArticleById(id.ToString());
    }

    public async Task DeleteAllByUserName(string name)
    {
        await articleRepository.DeleteAllByUserName(name);
    }
}
