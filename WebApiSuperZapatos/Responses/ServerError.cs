namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    [KnownType(typeof(ServerError))]
    public class ServerError : ErrorResponse, ISuperZapatosResponse
    {
        public ServerError(string errorMessage)
        {
            this.ErrorCode = "500";
            this.ErrorMessage = "Server error: " + errorMessage;
            this.Sucess = false;
        }
    }
}