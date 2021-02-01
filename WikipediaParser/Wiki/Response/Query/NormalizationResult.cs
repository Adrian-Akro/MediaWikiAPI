namespace MediaWikiApi.Wiki.Response.Query {
    /// <summary>
    /// Contains the normalized query title
    /// </summary>
    public class NormalizationResult {
        string From { get; set; }
        string To { get; set; }
        bool FromEncoded { get; set; }
    }
}