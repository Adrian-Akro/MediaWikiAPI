namespace MediaWikiApi.Wiki.Response.Query.ImageInfo {
    public interface IImageInfoUrl : IImageInfo {
        public string Url { get; set; }
        public string DescriptionUrl { get; set; }
        public string DescriptionShortUrl { get; set; }
    }
}
