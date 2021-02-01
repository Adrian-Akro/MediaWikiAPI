using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.ImageInfo {
    public class ImageInfoPage<T> : IPage where T : IImageInfo {
        public int PageID { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public bool Missing { get; set; }
        public bool Known { get; set; }
        public string ImageRepository { get; set; }
        public List<T> ImageInfo { get; set; }
    }
}
