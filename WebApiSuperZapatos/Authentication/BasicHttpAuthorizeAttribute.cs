namespace WebApiSuperZapatos.Authentication
{
    using System;
    using System.Web.Http;
    using System.Net.Http.Headers;
    using System.Web.Http.Controllers;

    using System.Text;

    public class BasicHttpAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                AuthenticationHeaderValue auth = actionContext.Request.Headers.Authorization;
                if (string.Compare(auth.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                    int separatorIndex = credentials.IndexOf(':');
                    if (separatorIndex >= 0)
                    {
                        string userName = credentials.Substring(0, separatorIndex);
                        string password = credentials.Substring(separatorIndex + 1);
                        if (userName == "my_user" && password == "my_password")
                        {
                            Authorized.IsAuthorized = true;
                            return true;
                        }

                        Authorized.IsAuthorized = false;
                        Authorized.Reason = "Wrong username or password";
                        return true;
                    }
                }

                Authorized.IsAuthorized = false;
                Authorized.Reason = "No username or password were suplied";

                return true;
            }
            catch(Exception ex)
            {
                // To handle the custom message
                Authorized.IsAuthorized = false;
                Authorized.Reason = ex.Message;
                return true;
            }
        }
    }
}