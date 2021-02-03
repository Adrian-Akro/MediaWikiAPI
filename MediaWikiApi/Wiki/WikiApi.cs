using MediaWikiApi.Wiki.Handler;
using MediaWikiApi.Wiki.Response.OpenSearch;
using MediaWikiApi.Wiki.Response.Query.Categories;
using MediaWikiApi.Wiki.Response.Query.Extracts;
using MediaWikiApi.Wiki.Response.Query.ImageInfo;
using MediaWikiApi.Wiki.Response.Query.Images;
using MediaWikiApi.Wiki.Response.Query.PageImages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaWikiApi.Wiki {
    public class WikiApi {

        ImagesResponseHandler<Image> imagesResponseHandler;
        ImageInfoResponseHandler<ImageInfoUrl> imageInfoResponseHandler;
        ExtractResponseHandler<HTMLExtractPage> extractResponseHandler;
        PageImageResponseHandler<SourcePageImagePage> pageImageResponseHandler;
        CategoriesResponseHandler<Category> categoriesResponseHandler;
        OpenSearchResponseHandler openSearchResponseHandler;

        public WikiApi(string wikiUrl) {
            if (!IsWikiUrlValid(wikiUrl)) {
                throw new UriFormatException();
            }
            openSearchResponseHandler = new OpenSearchResponseHandler(wikiUrl);
            imagesResponseHandler = new ImagesResponseHandler<Image>(wikiUrl);
            imageInfoResponseHandler = new ImageInfoResponseHandler<ImageInfoUrl>(wikiUrl);
            imageInfoResponseHandler.AddQueryStringArgument("iiprop", "url");
            extractResponseHandler = new ExtractResponseHandler<HTMLExtractPage>(wikiUrl);
            pageImageResponseHandler = new PageImageResponseHandler<SourcePageImagePage>(wikiUrl);
            pageImageResponseHandler.AddQueryStringArgument("piprop", "original");
            categoriesResponseHandler = new CategoriesResponseHandler<Category>(wikiUrl);
        }

        public static bool IsValidWikiUrl(string wikiUrl) {
            OpenSearchResponseHandler openSearchResponseHandler = new OpenSearchResponseHandler(wikiUrl);
            try {
                openSearchResponseHandler.RequestSingle("a");
            } catch (Exception) {
                return false;
            }
            return true;
        }

        private bool IsWikiUrlValid(string wikiUrl) {
            return Uri.IsWellFormedUriString(wikiUrl, UriKind.RelativeOrAbsolute);
        }

        public IReadOnlyList<IImageInfoUrl> GetImageUrls(string pageTitle) {
            IReadOnlyList<IImage> images = imagesResponseHandler.RequestSingle(pageTitle);
            return imageInfoResponseHandler.RequestMany(images.Select(x => x.Title).ToArray());
        }


        public IReadOnlyList<ICategory> GetCategories(string pageTitle) {
            return categoriesResponseHandler.RequestSingle(pageTitle);
        }

        public IPageImage GetPageImage(string pageTitle) {
            return pageImageResponseHandler.RequestSingle(pageTitle);
        }

        public IReadOnlyList<Section> GetSections(string pageTitle) {
            return extractResponseHandler.RequestSingle(pageTitle).ExtractInterpreter.Sections;
        }

        public IOpenSearch Search(string term) {
            return openSearchResponseHandler.RequestSingle(term);
        }
    }
}
