namespace MediaWikiApi.Wiki.Handler.Interfaces {
    /// <summary>
    /// A response handler that combines de single and many terms response handler
    /// </summary>
    public interface ICompositeTermResponseHandler<T> : ISingleTermResponseHandler<T>, IManyTermsResponseHandler<T> {
    }
}
