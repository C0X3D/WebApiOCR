using System;
using System.IO;
using System.Text;

namespace LogEngines
{
    public static class LogEngine
    {
        public static void Write(string message)
        {
            //GET MESSAGE TEMPLATE 
            var sb = "["+DateTime.Now+"]" + message + ";";
            var x = File.AppendText(Settings.SavePath);
             x.WriteLine(sb);
            x.Close();
        }
        public static void Write(string message,string user)
        {
            Write(" | " + user + " | " + message);
        }
    }
}
