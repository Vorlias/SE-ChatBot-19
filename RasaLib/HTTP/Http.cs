﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RasaLib.HTTP
{
    /// <summary>
    /// Http Request handler
    /// </summary>
    internal class Http
    {
        public static void PostJson<T>(string uri, T data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JObject.FromObject(data).ToString();

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
