namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    [KnownType(typeof(BadRequestError))]
    public class BadRequestError : ErrorResponse, ISuperZapatosResponse
    {
        public BadRequestError()
        {
            this.ErrorCode = "400";
            this.ErrorMessage = "Bad Request";
            this.Sucess = false;
        }
    }
}