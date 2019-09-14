using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Business_Cards
{
    public class UserObject
    {
       public static int userID = 0;
       public static user Instance
        {
            get
            {
                if (userID != 0)
                    return Queries.User(userID);
                else
                    return null;                
            }
        }
        public static void SetInstance(user _user)
        {
            userID = _user.Id;
        }
    }
}
