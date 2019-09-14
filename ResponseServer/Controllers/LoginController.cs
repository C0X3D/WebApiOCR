using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResponseServer.Controllers
{
    public class LoginController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        //[Route("api/create")]
        public async Task<string> Login()
        {
            var filesProvider = await Request.Content.ReadAsFormDataAsync();

            var username = filesProvider[0];
            var password = filesProvider[1];
            //var email = filesProvider[2];
            return Queries.LogIn(username, password);
        }
    }
}
