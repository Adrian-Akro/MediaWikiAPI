using MediaWikiApi.Requests;

namespace MediaWikiApi.Wiki.Handler.Abstractions {
    public abstract class ResponseHandler {
        protected RequestHandler RequestHandler { get; set; }

        /// <summary>
        /// Adds a query string parameter to the current instance of the response handler.
        /// </summary>
        /// <param name="type">Wether more than a single instance of the same key is allowed (Single, List)</param>
        public ResponseHandler AddQueryStringArgument(string key, string value, ParamType type = ParamType.Single) {
            RequestHandler.AddArgument(key, value, type);
            return this;
        }

        /// <summary>
        /// Removes a query string parameter to the current instance of the response handler.
        /// </summary>
        /// <returns>True if the operation was succesful, otherwise it returns false</returns>
        public bool RemoveQueryStringArgument(string key) {
            return RequestHandler.RemoveArgument(key);
        }
    }
}
