using MediaWikiApi.Requests;
using MediaWikiApi.Requests.Exceptions;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Handler.Interfaces;
using MediaWikiApi.Wiki.Parser;
using MediaWikiApi.Wiki.Response.OpenSearch;
using System;

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
            OpenSearch os;
            try {
                os = Parser.Parse(RequestHandler.Make());
            } catch (Exception ex) {
                throw new CouldNotParseException(ex);
            }
            RequestHandler.RemoveArgument("search");
            return os;
        }
    }
}
