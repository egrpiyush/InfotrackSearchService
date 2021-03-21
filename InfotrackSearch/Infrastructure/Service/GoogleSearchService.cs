using Application.Interface;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Infrastructure
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IConfiguration _configuration;
        private readonly string _url;
        public GoogleSearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = _configuration["Google_Search_Url"] + "?q={0}&count=100&start={1}";
        }
        public string Search(string searchFor, int pageNumber)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Infotrack search");
            return webClient.DownloadString(string.Format(_url, searchFor, pageNumber));
        }
    }
}
