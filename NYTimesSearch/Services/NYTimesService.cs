using Newtonsoft.Json;
using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NYTimesSearch.Services
{
    internal class NYTimesService
    {
        private const string APIKEY = "zxZ1fZAhKIikmEedwq2Z38h29VeQudNo";
        private const string URL = "http://api.nytimes.com/svc/search/v2/articlesearch.json";
        private readonly HttpClient client;

        public NYTimesService()
        {
            client = new HttpClient();
        }
        internal async Task<SearchResults> SearchNYT(string keywords, string page)
        {
            SearchResults result = new SearchResults();
            try
            {
                var builder = new UriBuilder(URL);
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["api-key"] = APIKEY;
                query["page"] = page;
                query["q"] = keywords;
                builder.Query = query.ToString();
                string url = builder.ToString();

                string responseBody = await client.GetStringAsync(url);

                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                SearchResultItem item;
                int itemsFound = jsonResponse.response.meta.hits > 10 ? 10 : jsonResponse.response.meta.hits;
                for (int i = 1; i < itemsFound; i++)
                {
                    item = new SearchResultItem() { };
                    item.ArticleName = jsonResponse.response.docs[i].headline.main;
                    item.ArticleLink = jsonResponse.response.docs[i].web_url;
                    item.ArticleSource = jsonResponse.response.docs[i].source;

                    result.SearchResultsList.Add(item);
                }


                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}