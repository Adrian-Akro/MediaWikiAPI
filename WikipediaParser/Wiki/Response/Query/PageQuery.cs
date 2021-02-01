using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query {
    public class PageQuery<TPage, TContinue> : IQuery<TContinue>
        where TPage : IPage
        where TContinue : IContinueParameters {
        public bool BatchComplete { get; set; }
        public Dictionary<string, WarningObject> Warnings { get; set; }
        public TContinue Continue { get; set; }
        public PageQueryResult<TPage> Query { get; set; }
    }
}