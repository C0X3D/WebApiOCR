using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Server_Business_Cards
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
        public static List<bizCard> _bizCards(this user user)
        {
            using (var db = new bcservEntities())
            {
                var q = (List<bizCard>)from p in db.users where p.Id == user.Id select p.bizCards.ToList();
                return q;
            }
        }
        public static bool CreateUser(string jsonDetails)
        {
            user u = new user();
            u = JsonConvert.DeserializeObject<user>(jsonDetails);

            using (var db = new bcservEntities())
            {
                db.users.Add(u);//  return q;
                db.SaveChanges();
            }
            return false;
        }
        public static void AddCard(this user _user,bizCard card)
        {

        }
        public static user User(int userID)
        {
            using (var db = new bcservEntities())
            {
                return (from p in db.users where p.Id == userID select p).FirstOrDefault();
            }
        }
    }
}
