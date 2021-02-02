using MediaWikiApi.Wiki.Response.Query;
using Newtonsoft.Json;

namespace MediaWikiApi.Wiki.Parser {
    public class QueryParser<TPage, TContinue> : IParser<PageQuery<TPage, TContinue>>
        where TPage : IPage
        where TContinue : IContinueParameters {
        public PageQuery<TPage, TContinue> Parse(string requestResult) {
            return JsonConvert.DeserializeObject<PageQuery<TPage, TContinue>>(requestResult);
        }
    }
}
