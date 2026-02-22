using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVrefitLOLP
{
    public abstract class A_RefitRequester
    {
        protected HttpClient cookieClient;
        protected CookieHandler cookieHandler;

        protected A_RefitRequester(HttpClient CCclient, CookieHandler CCcookieHandler)
        {
            this.cookieClient = CCclient;
            this.cookieHandler = CCcookieHandler;
        }

    }
}
