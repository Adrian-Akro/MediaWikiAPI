using System;

namespace MediaWikiApi.Wiki.Handler.Exceptions {
    [Serializable]
    public class PageNotFoundException : Exception {
        public PageNotFoundException() {
        }

        public PageNotFoundException(string pageTitle) : base($"No page found for {pageTitle}") {
        }

    }
}