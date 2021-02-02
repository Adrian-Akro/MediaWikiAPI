using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Response.Query;
using MediaWikiApi.Wiki.Response.Query.Extracts;

namespace MediaWikiApi.Wiki.Handler {
    public class ExtractResponseHandler<T> : QueryHandler<T, T, ExtractContinueParams>
        where T : IExtractPage, IPage, new() {

        public ExtractResponseHandler(string wikiUrl) : base(wikiUrl) { }

        public override RequestHandler GetQueryRequestHandler(string wikiBaseUrl) {
            return base
                .GetQueryRequestHandler(wikiBaseUrl)
                .AddArgument("prop", "extracts");
        }

        protected override T ComputeContinuedBehavior(T newData, T existingData) {
            existingData.Extract += newData.Extract;
            return existingData;
        }

        protected override T GetRequestedFromResponse(T parsedResponse) {
            if (parsedResponse.Missing) throw new PageNotFoundException(parsedResponse.Title);
            return parsedResponse;
        }

        protected override bool IsContinue(IQuery<ExtractContinueParams> parsedResponse, RequestHandler parametrizedRequestHandler, out RequestHandler continueRequestHandler) {
            if (parsedResponse.Continue == null) {
                continueRequestHandler = null;
                return false;
            }
            // Since the arguments are of type single they are overwritten
            continueRequestHandler = parametrizedRequestHandler
                .AddArgument("continue", parsedResponse.Continue.Continue)
                .AddArgument("excontinue", parsedResponse.Continue.ExContinue);
            return !(parsedResponse.BatchComplete);
        }
    }
}
