namespace WebApiSuperZapatos.Responses
{
    using System.Runtime.Serialization;

    [KnownType(typeof(RecordNotFoundError))]
    public class RecordNotFoundError : ErrorResponse, ISuperZapatosResponse
    {
        public RecordNotFoundError()
        {
            this.ErrorCode = "404";
            this.ErrorMessage = "Record not found";
            this.Sucess = false;
        }
    }
}