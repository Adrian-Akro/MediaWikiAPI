namespace MediaWikiApi.Wiki.Response.Query.Images {

    /// <summary>
    /// A image file object inside the wiki. File objects are usually given in a "FileType: FileName.Extension" format
    /// <item><inheritdoc/></item>
    /// </summary>
    public interface IImage {

        int NS { get; set; }
        string Title { get; set; }
    }
}