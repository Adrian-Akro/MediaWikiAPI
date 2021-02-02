namespace MediaWikiApi.Wiki.Response.Query.ImageInfo {
    public class ImageInfoUrl : IImageInfoUrl, IImageInfoBasic {
        public string Url { get; set; }
        public string DescriptionUrl { get; set; }
        public string DescriptionShortUrl { get; set; }
        public string TimeStamp { get; set; }
        public string User { get; set; }
    }
}