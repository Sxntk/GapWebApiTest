namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    using DataAccess;

    [KnownType(typeof(ArticleResponse))]
    public class ArticleResponse : ISuperZapatosResponse
    {
        public ArticleAccess Article { get; set; }

        public bool Sucess { get; set; }

        public int TotalElements { get; set; }
    }
}