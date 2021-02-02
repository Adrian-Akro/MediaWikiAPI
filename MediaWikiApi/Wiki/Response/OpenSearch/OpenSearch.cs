using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.OpenSearch {
    public class OpenSearch : IOpenSearch {
        public string LookupTerm { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Urls { get; set; }
    }
}
