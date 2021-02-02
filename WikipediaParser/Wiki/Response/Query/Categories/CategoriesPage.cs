
using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.Categories {
    public class CategoriesPage<T> : ICategoriesPage<T> where T : ICategory {
        public List<T> Categories { get; set; } = new List<T>();
        public int PageID { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public bool Missing { get; set; }
    }
}
