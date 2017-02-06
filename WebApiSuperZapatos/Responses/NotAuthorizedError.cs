namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    [KnownType(typeof(NotAuthorizedError))]
    public class NotAuthorizedError : ErrorResponse, ISuperZapatosResponse
    {
        public NotAuthorizedError()
        {
            this.ErrorCode = "401";
            this.ErrorMessage = "Not authorized";
            this.Sucess = false;
        }
    }
}