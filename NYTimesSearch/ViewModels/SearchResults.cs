using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYTimesSearch.ViewModels
{
    public class SearchResultItem
    {
        public string ArticleName { get; set; }
        public string ArticleLink { get; set; }
        public string ArticleSource { get; set; }
    }
    public class SearchResults
    {
        public List<SearchResultItem> SearchResultsList { get; set; }
        public string Page { get; set; }

        [Required]
        [Display(Name = "Searh item")]
        public string SearchItem { get; set; }
        public SearchResults()
        {
            SearchResultsList = new List<SearchResultItem>();
            Page = "1";
        }
    }
}