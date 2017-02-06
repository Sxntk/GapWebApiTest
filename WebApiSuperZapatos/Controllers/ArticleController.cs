namespace WebApiSuperZapatos.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Authentication;
    using DataAccess;
    using Responses;

    public class ArticleController : ApiController
    {
        // GET: services/Article
        [Authentication.BasicHttpAuthorize]
        [Route("services/articles")]
        public ISuperZapatosResponse Get()
        {
            try
            {
                if (!Authorized.IsAuthorized)
                {
                    NotAuthorizedError notAuthorized = new NotAuthorizedError();
                    return notAuthorized;
                }

                IEnumerable<ArticleAccess> articles = ArticleAccess.GetAllArticles();
                int count = articles.Count();

                if (count == 1)
                {
                    ArticleResponse articleResponse = new ArticleResponse();
                    articleResponse.Article = articles.First();
                    articleResponse.Sucess = true;
                    articleResponse.TotalElements = count;
                    return articleResponse;
                }

                ArticlesResponse response = new ArticlesResponse();
                response.Articles = articles;
                response.Sucess = true;
                response.TotalElements = count;

                return response;
            }
            catch (Exception ex)
            {
                ServerError error = new ServerError(ex.Message);
                return error;
            }
        }

        // GET: services/articles/stores/{GUID}
        [Authentication.BasicHttpAuthorize]
        [Route("services/articles/stores/{storeid}")]
        public ISuperZapatosResponse Get(string storeid)
        {
            try
            {
                if (!Authorized.IsAuthorized)
                {
                    NotAuthorizedError notAuthorized = new NotAuthorizedError();
                    return notAuthorized;
                }

                Guid id;
                if (!Guid.TryParse(storeid, out id))
                {
                    BadRequestError formatError = new BadRequestError();
                    return formatError;
                }

                IEnumerable<ArticleAccess> articles = ArticleAccess.GetArticlesByStoreId(id);
                int count = articles.Count();
                if (count == 0)
                {
                    RecordNotFoundError error = new RecordNotFoundError();
                    return error;
                }

                if (count == 1)
                {
                    ArticleResponse articleResponse = new ArticleResponse();
                    articleResponse.Article = articles.First();
                    articleResponse.Sucess = true;
                    articleResponse.TotalElements = count;
                    return articleResponse;
                }

                ArticlesResponse response = new ArticlesResponse();
                response.Articles = articles;
                response.Sucess = true;
                response.TotalElements = count;

                return response;
            }
            catch (Exception ex)
            {
                ServerError error = new ServerError(ex.Message);
                return error;
            }
        }

        // GET: services/articles/stores/{GUID}
        [Authentication.BasicHttpAuthorize]
        [HttpGet]
        [Route("services/articles/create/{name}/{description}/{price}/{totalinshelf}/{totalinvault}/{storeid}")]
        public ISuperZapatosResponse Create(string name, string description, decimal price, decimal? totalinshelf, decimal? totalinvault, Guid storeid)
        {
            try
            {
                if (!Authorized.IsAuthorized)
                {
                    NotAuthorizedError notAuthorized = new NotAuthorizedError();
                    return notAuthorized;
                }

                ArticleAccess article = new ArticleAccess(name, description, price, totalinshelf, totalinvault, storeid);
                bool exitoso = ArticleAccess.CreateArticle(article);

                if (exitoso)
                {
                    ArticleResponse articleResponse = new ArticleResponse();
                    articleResponse.Article = article;
                    articleResponse.Sucess = true;
                    articleResponse.TotalElements = 1;
                    return articleResponse;
                }

                ServerError error = new ServerError("Unexpected error inserting the article.");
                return error;
            }
            catch (Exception ex)
            {
                ServerError error = new ServerError(ex.Message);
                return error;
            }
        }
    }
}
