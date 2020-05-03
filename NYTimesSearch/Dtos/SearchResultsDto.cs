using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYTimesSearch.Dtos
{
    public class SearchResultsDto
    {
        public List<SearchResultItem> SearchResultsList { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Page { get; set; }
        [Required]
        public string SearchItem { get; set; }
        public SearchResultsDto()
        {
            SearchResultsList = new List<SearchResultItem>();
            Page = "1";
        }
    }
}