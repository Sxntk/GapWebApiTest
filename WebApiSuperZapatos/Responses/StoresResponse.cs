namespace WebApiSuperZapatos.Responses
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using DataAccess;

    [KnownType(typeof(StoresResponse))]
    public class StoresResponse : ISuperZapatosResponse
    {
        public IEnumerable<StoreAccess> Stores { get; set; }

        public bool Sucess { get; set; }

        public int TotalElements { get; set; }
    }
}