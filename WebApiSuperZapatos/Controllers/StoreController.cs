namespace WebApiSuperZapatos.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Authentication;
    using DataAccess;
    using Responses;

    public class StoreController : ApiController
    {
        // GET: services/Store
        [Authentication.BasicHttpAuthorize]
        [Route("services/stores")]
        public ISuperZapatosResponse Get()
        {
            try
            {
                if (!Authorized.IsAuthorized)
                {
                    NotAuthorizedError notAuthorized = new NotAuthorizedError();
                    return notAuthorized;
                }

                IEnumerable<StoreAccess> stores = StoreAccess.GetAllStores();
                int count = stores.Count();

                if (count == 1)
                {
                    StoreResponse storeResponse = new StoreResponse();
                    storeResponse.Store = stores.First();
                    storeResponse.Sucess = true;
                    storeResponse.TotalElements = count;
                    return storeResponse;
                }

                StoresResponse response = new StoresResponse();
                response.Stores = stores;
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

        [Authentication.BasicHttpAuthorize]
        [HttpGet]
        [Route("services/stores/create/{name}/{address}")]
        public ISuperZapatosResponse Create(string name, string address)
        {
            try
            {
                if (!Authorized.IsAuthorized)
                {
                    NotAuthorizedError notAuthorized = new NotAuthorizedError();
                    return notAuthorized;
                }

                StoreAccess store = new StoreAccess(name, address);
                bool exitoso = StoreAccess.CreateStore(store);

                if (exitoso)
                {
                    StoreResponse storeResponde = new StoreResponse();
                    storeResponde.Store = store;
                    storeResponde.Sucess = true;
                    storeResponde.TotalElements = 1;
                    return storeResponde;
                }

                ServerError error = new ServerError("Unexpected error inserting the store.");
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