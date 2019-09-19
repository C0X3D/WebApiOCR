using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ResponseServer
{
    public static class Queries
    {
        public static List<bizCard> GetBizCards(int userID)
        {
            using (var db = new bcservEntities())
            {
                var q = (List<bizCard>)from p in db.users where p.Id == userID select p.bizCards.ToList();
                return q;
            }
        }
        public static ICollection<bizCard> _bizCards(this user user)
        {
            using (var db = new bcservEntities())
            {
                var q = (from p in db.users where p.Id == user.Id select p.bizCards).FirstOrDefault(); //as List<bizCard>;
                return q;
            }
        }

        internal static bool UserExist(string username, string email)
        {
            using (var db = new bcservEntities())
            {
                var q = (from p in db.users where p.username == username || p.email == email select p).ToList();
                if (q != null)
                {
                    if (q.Count < 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static string CreateUserAsync(string user,string password,string email)
        {
            string resp = "Error";
            using (var db = new bcservEntities())
            {
                try
                {
                    user x = new user();
                    x.bizCards = null;
                    x.AuthKeys = null;
                    x.email = email;
                    x.username = user;
                    x.password = password;
                    resp = "Here It Crashes.";
                    db.users.Add(x);//  return q;                   
                    int q = db.SaveChanges();
                    if (q > 0)
                    {
                        resp = "Account Created";
                        return resp;
                        //Creating AUTH KEY
                        //var use = (from b in db.users where b.username == user && b.password == password select b).FirstOrDefault();
                        //No auth key needed for this thing 
                        //use.AuthKeys.Add(new AuthKey() {authstring = GenerateAuth(user), expiration = DateTime.Now.AddDays(30)});
                        //db.SaveChanges();
                    }
                    else
                    {
                        resp = "Error cannot create user reason DB ERROR";
                        return resp;
                    }
                }
                catch
                {
                  //  resp = "Error ON LINE 81";

                }

            }
           
            return resp;
        }

        private static string GenerateAuth(string user)
        {
            return "";
            //HMAC256 ETC...
            //use your own based on username...
        }

        public static void AddCard(this user _user,bizCard card)
        {

        }
        public static string LogIn(string id, string pw)
        {
            user toRet = new user();
            bool response = false;
            if (id.Contains("@"))
            {
                using (var db = new bcservEntities())
                {

                    toRet = (from p in db.users where p.email == id && p.password == pw select p).FirstOrDefault();
                    if ((from p in db.users where p.email == id && p.password == pw select p).Count() > 0)
                    {
                        response = true;
                    }
                    // return q;
                }
            }
            else
            {
                using (var db = new bcservEntities())
                {
                    
                    toRet = (from p in db.users where p.username == id && p.password == pw select p).FirstOrDefault();
                    if ((from p in db.users where p.username == id && p.password == pw select p).Count() > 0)
                    {
                        response = true;
                    }
                    // return q;
                }
            }
            if(response)
            return "Login succesfull.";
            else
                return "User Not Found.";

        }
        
        public static user User(int userID)
        {
            using (var db = new bcservEntities())
            {
                
                var q = (from p in db.users where p.Id == userID select p).FirstOrDefault();                
                return q;
            }
        }
        public static user User(string username)
        {
            using (var db = new bcservEntities())
            {

                var q = (from p in db.users where p.username == username select p).FirstOrDefault();
                return q;
            }
        }
    }
}
