using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSuperZapatos.Authentication
{
    public static class Authorized
    {
        public static bool IsAuthorized { get; set; }

        public static string Reason { get; set; }
    }
}