using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace bizCommon
{
    public static class bizFE
    {
        public static string Serialize(this user user)
        {
            return JsonConvert.SerializeObject(user);
        }
        public static string Serialize(this bizCard card)
        {
            return JsonConvert.SerializeObject(card);
        }
        public static user CreateUser(string email,string _password,string username)
        {
            user u = new user
            {
                username = username,
                password = md5Hash(_password),
                email = email
            };
            return u;
        }

        private static string md5Hash(string password)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
