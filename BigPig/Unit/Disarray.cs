using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigPig.Unit
{
    public class Disarray
    {
        public static string HttpGetFrom(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";//链接类型
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream rspStream = response.GetResponseStream();
            string htmlAll = string.Empty;
            using (StreamReader reader = new StreamReader(rspStream, Encoding.UTF8))
            {
                htmlAll = reader.ReadToEnd();
                rspStream.Close();
            }
            response.Close();
            return htmlAll;
        }
    }
}
