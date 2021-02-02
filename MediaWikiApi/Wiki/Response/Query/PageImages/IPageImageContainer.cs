namespace MediaWikiApi.Wiki.Response.Query.PageImages {
    public interface IPageImageContainer {
        public IPageImage GetPageImage();
        public string PageImage { get; set; }
    }
}
