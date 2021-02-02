using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Interfaces;
using MediaWikiApi.Wiki.Parser;
using MediaWikiApi.Wiki.Response.OpenSearch;

namespace MediaWikiApi.Wiki.Handler {
    public class OpenSearchResponseHandler : ResponseHandler, ISingleTermResponseHandler<OpenSearch> {
        protected IParser<OpenSearch> Parser { get; set; }

        public OpenSearchResponseHandler(string wikiBaseUrl) {
            RequestHandler = new RequestHandler
                .Builder(wikiBaseUrl)
                .WithEndpoint("w/api.php")
                .WithQueryStringParam("action", "opensearch")
                .WithQueryStringParam("formatversion", "2")
                .Build();
            Parser = new OpenSearchParser();
        }

        public OpenSearch RequestSingle(string term) {
            RequestHandler.AddArgument("search", term);
            OpenSearch os = Parser.Parse(RequestHandler.Make());
            RequestHandler.RemoveArgument("search");
            return os;
        }
    }
}
