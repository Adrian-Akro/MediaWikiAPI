using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Response.Query;
using MediaWikiApi.Wiki.Response.Query.Categories;
using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Handler {
    public class CategoriesResponseHandler<T> : QueryHandler<List<T>, CategoriesPage<T>, CategoriesContinueParams>
        where T : ICategory, new() {
        public CategoriesResponseHandler(string wikiUrl) : base(wikiUrl) { }

        public override RequestHandler GetQueryRequestHandler(string wikiBaseUrl) {
            return base
                .GetQueryRequestHandler(wikiBaseUrl)
                .AddArgument("prop", "categories");
        }


        protected override List<T> GetRequestedFromResponse(CategoriesPage<T> parsedResponse) {
            if (parsedResponse.Missing) throw new PageNotFoundException(parsedResponse.Title);
            return parsedResponse.Categories;
        }


        protected override List<T> ComputeContinuedBehavior(List<T> newData, List<T> existingData) {
            existingData.AddRange(newData);
            return existingData;
        }

        protected override bool IsContinue(IQuery<CategoriesContinueParams> parsedResponse, RequestHandler parametrizedRequestHandler, out RequestHandler continueRequestHandler) {
            if (parsedResponse.Continue == null) {
                continueRequestHandler = null;
                return false;
            }
            continueRequestHandler = parametrizedRequestHandler
                .AddArgument("continue", parsedResponse.Continue.Continue)
                .AddArgument("clcontinue", parsedResponse.Continue.ClContinue);
            return !(parsedResponse.BatchComplete);
        }
    }
}

