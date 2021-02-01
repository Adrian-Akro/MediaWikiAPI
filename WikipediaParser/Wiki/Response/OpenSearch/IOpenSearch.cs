using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.OpenSearch {
    public interface IOpenSearch {
        public string LookupTerm { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Urls { get; set; }
    }
}
