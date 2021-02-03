using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.Images {
    public class ImagesPage<T> : IPage where T : IImage {
        public List<T> Images { get; set; } = new List<T>();
        public int PageID { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public bool Missing { get; set; }
    }
}
