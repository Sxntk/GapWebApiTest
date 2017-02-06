namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    using DataAccess;

    [KnownType(typeof(StoreResponse))]
    public class StoreResponse : ISuperZapatosResponse
    {
        public StoreAccess Store { get; set; }

        public bool Sucess { get; set; }

        public int TotalElements { get; set; }
    }
}