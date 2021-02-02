using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Response.Query;
using MediaWikiApi.Wiki.Response.Query.Images;
using System.Collections.Generic;
using System.Linq;

namespace MediaWikiApi.Wiki.Handler {
    public class ImagesResponseHandler<T> : QueryHandler<List<T>, ImagesPage<T>, ImagesContinueParams>
        where T : IImage, new() {

        public ImagesResponseHandler(string wikiUrl) : base(wikiUrl) { }

        public override RequestHandler GetQueryRequestHandler(string wikiBaseUrl) {
            return base
                .GetQueryRequestHandler(wikiBaseUrl)
                .AddArgument("prop", "images");
        }



        protected override List<T> ComputeContinuedBehavior(List<T> newData, List<T> existingData) {
            existingData.AddRange(newData);
            return existingData;
        }

        protected override List<T> GetRequestedFromResponse(ImagesPage<T> parsedResponse) {
            if (parsedResponse.Missing) throw new PageNotFoundException(parsedResponse.Title);
            else if (parsedResponse.Images == null) return new List<T>();
            return parsedResponse.Images.Cast<T>().ToList();
        }

        protected override bool IsContinue(IQuery<ImagesContinueParams> parsedResponse, RequestHandler parametrizedRequestHandler, out RequestHandler continueRequestHandler) {
            if (parsedResponse.Continue == null) {
                continueRequestHandler = null;
                return false;
            }
            continueRequestHandler = parametrizedRequestHandler
                .AddArgument("continue", parsedResponse.Continue.Continue)
                .AddArgument("imcontinue", parsedResponse.Continue.ImContinue);
            return !(parsedResponse.BatchComplete);
        }
    }
}