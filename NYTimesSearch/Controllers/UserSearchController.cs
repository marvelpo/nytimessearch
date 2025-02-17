﻿using NYTimesSearch.Models;
using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NYTimesSearch.Controllers
{

    [Authorize]
    public class UserSearchController : Controller
    {
        private const string APIKEY = "zxZ1fZAhKIikmEedwq2Z38h29VeQudNo";
        private const string URL = "http://api.nytimes.com/svc/search/v2/articlesearch.json";
        private readonly HttpClient client = new HttpClient();
        // GET: UserSearch
        public ActionResult Index()
        {            
            User thisUser = new User() { UserName = this.User.Identity.Name };
            return View("UserSearchForm", new SearchResults());
        }

        public async Task<ActionResult> SearchNews(SearchResults itemToSearch)
        {
            //SearchResults result = await SearchNYT(itemToSearch.SearchItem);
            SearchResults res = await SearchNYT(itemToSearch.SearchItem, itemToSearch.Page);
            // return Content("Searching item: " + res);
            return View("SearchResults", res);
        }

        private async Task<SearchResults> SearchNYT(string keywords, string page)
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

                //HttpResponseMessage response = await client.GetAsync(url);
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
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