using NYTimesSearch.Models;
using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NYTimesSearch.Services;

namespace NYTimesSearch.Controllers
{

    [Authorize]
    public class UserSearchController : Controller
    {
        NYTimesService nytService;
        public UserSearchController()
        {
            nytService = new NYTimesService();
        }
        // GET: UserSearch
        public ActionResult Index()
        {            
            User thisUser = new User() { UserName = this.User.Identity.Name };
            return View("UserSearchForm", new SearchResults());
        }

        public async Task<ActionResult> SearchNews(SearchResults itemToSearch)
        {
            //SearchResults result = await SearchNYT(itemToSearch.SearchItem);
            SearchResults res = await nytService.SearchNYT(itemToSearch.SearchItem, itemToSearch.Page);
            // return Content("Searching item: " + res);
            return View("SearchResults", res);
        }

        
    }
}