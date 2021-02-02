namespace MediaWikiApi.Wiki.Response.Query.PageImages {
    public class SourcePageImagePage : IPage, IPageImageContainer {
        public int PageID { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public bool Missing { get; set; }
        public PageImage Original { get; set; }
        public string PageImage { get; set; }

        public IPageImage GetPageImage() => Original;
    }
}
