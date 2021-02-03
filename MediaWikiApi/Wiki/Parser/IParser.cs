namespace MediaWikiApi.Wiki.Parser {
    public interface IParser<T> {
        public T Parse(string requestResult);
    }
}
