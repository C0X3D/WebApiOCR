using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
//using bizCommon;
//using Server_Business_Cards;

namespace ResponseServer.Controllers
{

    public class UsersController : ApiController
    {

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string users(int id)
        {
            var u = Queries.User(id);

            return JsonConvert.SerializeObject(u);
        }
       
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpGet]
        public string getuserdata()
        {
            var id = 0;
            return JsonConvert.SerializeObject(Queries.User(id));
        }
    }
}
