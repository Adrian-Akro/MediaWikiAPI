using MediaWikiApi.Requests;
using MediaWikiApi.Wiki.Handler.Abstractions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Response.Query.ImageInfo;
using System.Linq;

namespace MediaWikiApi.Wiki.Handler {
    public class ImageInfoResponseHandler<T> : QueryHandler<T, ImageInfoPage<T>, ImageInfoContinueParams>
        where T : class, IImageInfo {
        public ImageInfoResponseHandler(string wikiUrl) : base(wikiUrl) { }

        public override RequestHandler GetQueryRequestHandler(string wikiBaseUrl) {
            return base
                .GetQueryRequestHandler(wikiBaseUrl)
                .AddArgument("prop", "imageinfo");
        }

        protected override T GetRequestedFromResponse(ImageInfoPage<T> parsedResponse) {
            if (parsedResponse.Missing) throw new PageNotFoundException(parsedResponse.Title);
            if (parsedResponse.ImageInfo.Count > 0) {
                return parsedResponse.ImageInfo.First();
            }
            return default(T);
        }
    }
}
