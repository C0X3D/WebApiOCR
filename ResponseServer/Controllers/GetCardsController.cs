using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ResponseServer.Controllers
{
    public class GetCardsController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost]
        //[Route("api/create")]
        public async Task<string> GetCards()
        {
            var filesProvider = await Request.Content.ReadAsFormDataAsync();

            var username = filesProvider[0];
            var x = Queries.User(username)._bizCards();

            List<bizCard> bcs = new List<bizCard>();
            foreach(var card in x)
            {
                bcs.Add(card);
            }
            var toret = JsonConvert.SerializeObject(bcs);
            return toret;
        }
    }
}
