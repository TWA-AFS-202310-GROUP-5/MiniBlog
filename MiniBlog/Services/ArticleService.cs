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
    private readonly UserStore userStore = null!;

    public ArticleService(IArticleRepository articleRepository, UserStore userStore)
    {
        this.articleRepository = articleRepository;
        this.userStore = userStore;
    }

    public async Task<Article> CreateArticle(Article article)
    {
        var temp = await articleRepository.CreateArticle(article);
        return temp;
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await articleRepository.GetArticleById(id.ToString());
    }
}
