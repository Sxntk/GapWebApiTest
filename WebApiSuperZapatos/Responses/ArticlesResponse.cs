namespace WebApiSuperZapatos.Responses
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using DataAccess;

    [KnownType(typeof(ArticlesResponse))]
    public class ArticlesResponse : ISuperZapatosResponse
    {
        public IEnumerable<ArticleAccess> Articles { get; set; }

        public bool Sucess { get; set; }

        public int TotalElements { get; set; }
    }
}