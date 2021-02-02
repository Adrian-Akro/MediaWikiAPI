namespace MediaWikiApi.Wiki.Response.Query.Extracts {
    public interface IExtractPage {
        string Extract { get; set; }
        IExtractInterpreter ExtractInterpreter { get; set; }
    }
}
