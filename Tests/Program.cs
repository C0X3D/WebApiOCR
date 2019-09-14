using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using bizCommon;
using Newtonsoft.Json;

namespace Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //string x = await ApiFunctions.GetUser(1);
            //Console.WriteLine(x);
            //string c = await ApiFunctions.CreateUser("acc","pw","email");
            //Console.WriteLine(c);
            await ApiFunctions.Login("brt94", "cosminR32");
            var cards = await ApiFunctions.GetBizCards();
            Console.WriteLine(cards);




            Console.Read();

        }


    }
}

