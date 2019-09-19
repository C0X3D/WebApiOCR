using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResponseServer.Controllers
{
    public class CreateUserController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        //[Route("api/create")]
        public async Task<string> CreateUser()
        {
            var filesProvider = await Request.Content.ReadAsFormDataAsync();

            var username = filesProvider[0];
            var password = filesProvider[1];
            var email = filesProvider[2];
            string msg = "Communication Error with Database.";
            if (!Queries.UserExist(username, email))
            {
                // string jsonDetails = "";
                msg = Queries.CreateUserAsync(username, password, email);
            }
            else
            {
                msg = "Cannot create user. Reason email or username in use.";
            }
            return msg;
        }
    }
}
