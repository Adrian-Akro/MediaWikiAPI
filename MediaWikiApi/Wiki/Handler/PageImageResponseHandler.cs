using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Response.Query;
using MediaWikiApi.Wiki.Response.Query.PageImages;

namespace MediaWikiApi.Wiki.Handler {
    public class PageImageResponseHandler<T> : QueryHandler<IPageImage, T, PageImageContinueParams>
        where T : IPageImageContainer, IPage, new() {
        public PageImageResponseHandler(string wikiUrl) : base(wikiUrl) { }

        public override RequestHandler GetQueryRequestHandler(string wikiBaseUrl) {
            return base
                .GetQueryRequestHandler(wikiBaseUrl)
                .AddArgument("prop", "pageimages");
        }


        protected override IPageImage GetRequestedFromResponse(T parsedResponse) {
            if (parsedResponse.Missing) throw new PageNotFoundException(parsedResponse.Title);
            return parsedResponse.GetPageImage();
        }
    }
}
