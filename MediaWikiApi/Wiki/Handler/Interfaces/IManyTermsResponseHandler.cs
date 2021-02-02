using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Handler.Interfaces {
    public interface IManyTermsResponseHandler<T> {
        public IReadOnlyList<T> RequestMany(params string[] terms);
    }
}
