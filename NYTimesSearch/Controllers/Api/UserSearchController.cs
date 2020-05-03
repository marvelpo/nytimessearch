using NYTimesSearch.Dtos;
using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NYTimesSearch.Controllers;
using NYTimesSearch.Services;
using System.Threading.Tasks;

namespace NYTimesSearch.Controllers.Api
{
    public class UserSearchController : ApiController
    {
        NYTimesService nytService;
        public UserSearchController()
        {
            nytService = new NYTimesService();
        }
        //GET /api/usersearch
        public async Task<SearchResultsDto> GetSearchResults(SearchResultsDto search)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            SearchResults response = await nytService.SearchNYT(search.SearchItem, search.Page);
            SearchResultsDto result = new SearchResultsDto();
            result.SearchResultsList = response.SearchResultsList;
            result.Page = search.Page;
            return result;
        }
    }
}
