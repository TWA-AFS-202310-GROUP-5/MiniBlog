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
    private readonly UserStore userStore = null!;

    public ArticleService(UserStore userStore, IArticleRepository articleRepository)
    {
        this.articleRepository = articleRepository;
        this.userStore = userStore;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        userStore.AddUser(article.UserName);
        return await articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetById(string id)
    {
        return await articleRepository.GetById(id);
    }
}
