using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query {
    public class PageQueryResult<T> where T : IPage {
        public List<NormalizationResult> Normalized { get; set; }
        public List<T> Pages { get; set; }
    }
}