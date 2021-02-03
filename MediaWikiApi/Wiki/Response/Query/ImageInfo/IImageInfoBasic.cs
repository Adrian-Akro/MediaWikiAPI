namespace MediaWikiApi.Wiki.Response.Query.ImageInfo {
    public interface IImageInfoBasic : IImageInfo {
        string TimeStamp { get; set; }
        string User { get; set; }
    }
}
