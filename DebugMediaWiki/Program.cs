using System;
using MediaWikiApi.Wiki;
using MediaWikiApi.Wiki.Response.OpenSearch;
using MediaWikiApi.Wiki.Response.Query.PageImages;

namespace DebugMediaWiki {
    class Program {
        static void Main(string[] args) {
            WikiApi api = new WikiApi("www.wikipedia.es");
            IOpenSearch os = api.Search("Google");
            IPageImage pageImage = api.GetPageImage(os.Titles[0]);
        }
    }
}
