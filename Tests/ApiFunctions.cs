using bizCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class ApiFunctions
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> UploadImage(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Statics.WebLink + "/api/upload");
            var content = new MultipartFormDataContent();

            byte[] byteArray = File.ReadAllBytes(path);
            content.Add(new ByteArrayContent(byteArray), "file", path);
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task<string> Login(string v1, string v2)
        {
            
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            pairs.Add(new KeyValuePair<string, string>("username", v1));
            pairs.Add(new KeyValuePair<string, string>("password", v2));
            //pairs.Add(new KeyValuePair<string, string>("email", email));

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            var responseString = "";

            using (var client = new HttpClient())
            {
                //client.PostAsync
                HttpResponseMessage response = client.PostAsync(Statics.WebLink + "/api/Login", content).GetAwaiter().GetResult();  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {

                    await response.Content.ReadAsStringAsync().ContinueWith((task) =>
                    {
                        responseString = JsonConvert.DeserializeObject<string>(task.Result);
                        //deserialized = JsonConvert.DeserializeObject<string>(customerJsonString);

                    });
                }
            }
            //For you can use the name or return an auth key or anything you like 
            //i`ll use just the unique name here
            if(responseString == "Login succesfull.")
            {
                Statics.AuthName = v1;
            }
            return responseString;
        }
        public static async Task<string> GetBizCards()
        {
            var request = (HttpWebRequest)WebRequest.Create(Statics.WebLink + "/api/users");

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            pairs.Add(new KeyValuePair<string, string>("username", Statics.AuthName));
            //YOU can set a request authentication string etc here...
            //pairs.Add(new KeyValuePair<string, string>("password", pw));
            //pairs.Add(new KeyValuePair<string, string>("email", email));

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            var responseString = "";

            using (var client = new HttpClient())
            {
                //client.PostAsync
                HttpResponseMessage response = client.PostAsync(Statics.WebLink + "/api/GetCards", content).GetAwaiter().GetResult();  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {

                    await response.Content.ReadAsStringAsync().ContinueWith((task) =>
                    {
                        responseString = task.Result;
                        var deserialized = JsonConvert.DeserializeObject<string>(responseString);
                        //TODO Deserialize to bizCard list
                        var bizList = JsonConvert.DeserializeObject<List<bizCard>>(deserialized);
                    });
                }
            }           
            return responseString;
        }
        public static async Task<string> CreateUser(string name,string pw,string email, bool autologin = false)
        {
            var request = (HttpWebRequest)WebRequest.Create(Statics.WebLink + "/api/users");

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

            pairs.Add(new KeyValuePair<string, string>("username", name));
            pairs.Add(new KeyValuePair<string, string>("password", pw));
            pairs.Add(new KeyValuePair<string, string>("email", email));

            FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
            var responseString = "";

            using (var client = new HttpClient())
            {
                //client.PostAsync
                HttpResponseMessage response =  client.PostAsync(Statics.WebLink + "/api/CreateUser",content).GetAwaiter().GetResult();  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {
                    
                    await response.Content.ReadAsStringAsync().ContinueWith((task) =>
                    {
                        responseString = task.Result;
                        //deserialized = JsonConvert.DeserializeObject<string>(customerJsonString);

                    });
                }
            }
            if (responseString == "Account Created" && autologin)
            {
                await Login(name, pw);
            }
            return responseString;
        }
        public static async Task<string> GetUser(int id)
        {
            var deserialized = "Error in Request";
            string url = Statics.WebLink + "/api/users?id="+id;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call!  
                if (response.IsSuccessStatusCode)
                {                    
                    var customerJsonString = "";
                    await response.Content.ReadAsStringAsync().ContinueWith((task) =>
                    {
                        customerJsonString = task.Result;
                        deserialized = JsonConvert.DeserializeObject<string>(customerJsonString);
                       
                    });
                }
            }
            return deserialized;
        }
    }
}
