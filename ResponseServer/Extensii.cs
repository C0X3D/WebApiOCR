using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ResponseServer
{
    public static class Extensii
    {
        public static bool ContainsName(this string str)
        {            
            var v = str.Split(' ');
            string nume = File.ReadAllText(@"C:\Users\brati\source\repos\Server Business Cards\ResponseServer\namesDB.txt");
            foreach (var v1 in v)
            {
                if (nume.Contains(v1))
                {
                    return true;
                }
            }
            return false;
        }
        public static string isPhoneNumber(this string str)
        {
            bool isPN = false;
            if(str.ToLower().Contains("tel"))
            {
                isPN = true;
            }
            var pn = new String(str.Where(Char.IsDigit).ToArray());
            if(pn.Length >= 3 || isPN)
            {
                return pn;
            }
            return "error";
        }
    }
}