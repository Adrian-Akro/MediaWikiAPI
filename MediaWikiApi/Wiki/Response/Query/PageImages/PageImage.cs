namespace MediaWikiApi.Wiki.Response.Query.PageImages {
    public class PageImage : IPageImage {
        public string Source { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
