using Application.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infrastructure
{
    public class StaticSearchService: IStaticSearchService
    {
        private readonly IConfiguration _configuration;
        public StaticSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Search(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Infotrack search");
            return webClient.DownloadString(url);
        }
    }
}
