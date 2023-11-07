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

    public ArticleService(IArticleRepository articleRepository)
    {
        this.articleRepository = articleRepository;
    }

    public async Task<Article?> CreateArticle(Article article)
    {
        return await articleRepository.CreateArticle(article);
    }

    public async Task<List<Article>> GetAll()
    {
        return await articleRepository.GetArticles();
    }

    public async Task<Article?> GetById(string id)
    {
        //articleStore.Articles.FirstOrDefault(article => article.Id == id.ToString())
        return await articleRepository.GetById(id);
    }
}
