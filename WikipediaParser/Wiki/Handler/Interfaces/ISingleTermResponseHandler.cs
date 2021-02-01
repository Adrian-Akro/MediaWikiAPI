namespace MediaWikiApi.Wiki.Handler.Interfaces {
    public interface ISingleTermResponseHandler<T> {
        public T RequestSingle(string term);
    }
}
