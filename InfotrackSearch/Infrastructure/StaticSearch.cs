using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infrastructure
{
    public class StaticSearch
    {
        private readonly IConfiguration _configuration;
        public StaticSearch(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Search(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Infotrach search");
            return webClient.DownloadString(url);
        }
    }
}
