namespace MediaWikiApi.Wiki.Response.Query.PageImages {
    public interface IPageImage {
        string Source { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}
