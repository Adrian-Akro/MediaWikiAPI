namespace MediaWikiApi.Wiki.Response.Query {
    public interface IPage {

        /// <summary>
        /// PageIDs start at 1 for pages that return a valid value and -1 if there's no page matching the query
        /// <item><inheritdoc/></item>
        /// </summary>
        int PageID { get; set; }
        int NS { get; set; }
        string Title { get; set; }
        bool Missing { get; set; }
    }
}