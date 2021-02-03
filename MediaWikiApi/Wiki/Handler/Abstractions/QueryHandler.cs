using MediaWikiApi.Requests;
using MediaWikiApi.Requests.Exceptions;
using MediaWikiApi.Wiki.Handler.Exceptions;
using MediaWikiApi.Wiki.Handler.Interfaces;
using MediaWikiApi.Wiki.Parser;
using MediaWikiApi.Wiki.Response.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaWikiApi.Wiki.Handler.Abstractions {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    /// <typeparam name="TPage">Must inherit IPage and be have a parameterless constructor so that newtonsoft can create an instance</typeparam>
    /// <typeparam name="TContinue">Must inherit IContinueParameters and be have a parameterless constructor so that newtonsoft can create an instance</typeparam>
    public abstract class QueryHandler<TReturnType, TPage, TContinue> : ResponseHandler, ICompositeTermResponseHandler<TReturnType>
        where TPage : IPage, new()
        where TContinue : IContinueParameters, new() {
        private QueryParser<TPage, TContinue> Parser { get; set; } = new QueryParser<TPage, TContinue>();

        public QueryHandler(string wikiUrl) {
            RequestHandler = GetQueryRequestHandler(wikiUrl);
        }

        public virtual RequestHandler GetQueryRequestHandler(string wikiUrl) {
            return new RequestHandler
                .Builder(wikiUrl)
                .WithEndpoint("w/api.php")
                .WithQueryStringParam("action", "query")
                .WithQueryStringParam("format", "json")
                .WithQueryStringParam("formatversion", "2")
                .WithQueryStringParam("utf8", "")
                .Build();
        }
        /// <summary>
        /// Checks wether the query can continue and outputs a request handler with the necessary continue parameters attached
        /// <item><inheritdoc/></item>
        /// </summary>
        /// <param name="requestResult">The result of the current search</param>
        /// <param name="parametrizedRequestHandler">The request handler used to get the current search result</param>
        /// <param name="continueRequestHandler">The resulting request handler with the parameters required to continue attached</param>
        /// <returns>True if the request can continue or false if it can't</returns>
        protected virtual bool IsContinue(IQuery<TContinue> parsedResponse, RequestHandler parametrizedRequestHandler, out RequestHandler continueRequestHandler) {
            continueRequestHandler = null;
            return false;
        }


        public virtual TReturnType RequestSingle(string term) {
            return RequestMany(term).First();
        }


        public virtual IReadOnlyList<TReturnType> RequestMany(params string[] terms) {
            RequestHandler parametrizedRh = GetParametrizedRequestHandler(terms);
            Dictionary<int, TReturnType> data = new Dictionary<int, TReturnType>();
            string response;
            PageQuery<TPage, TContinue> parsedResponse;
            do {
                try {
                    response = parametrizedRh.Make();
                    parsedResponse = Parser.Parse(response);
                } catch (Exception ex) {
                    throw new CouldNotParseException(ex);
                }
                for (int i = 0; i < parsedResponse.Query.Pages.Count; i++) {
                    TPage page = parsedResponse.Query.Pages[i];
                    TReturnType requestedData = GetRequestedFromResponse(page);
                    if (data.ContainsKey(i)) {
                        data[i] = ComputeContinuedBehavior(requestedData, data[i]);
                    } else {
                        data.Add(i, requestedData);
                    }
                }
            } while (IsContinue(parsedResponse, parametrizedRh, out parametrizedRh));
            return data.Values.ToList();
        }

        /// <summary>
        /// Selects the data that is to be returned for a given response page
        /// <item><inheritdoc/></item>
        /// </summary>
        protected abstract TReturnType GetRequestedFromResponse(TPage parsedResponse);

        /// <summary>
        ///  Execute the necessary merge operations for the existing data (if any)
        ///  when a continue operation is executed and new data is found.
        /// <item><inheritdoc/></item>
        /// </summary>
        /// <param name="newData">The data found after the continue operation was computed</param>
        /// <param name="existingData">The data that had been processed for the previous request</param>
        /// <returns>The result of the merging operations</returns>
        protected virtual TReturnType ComputeContinuedBehavior(TReturnType newData, TReturnType existingData) {
            return newData;
        }

        private RequestHandler GetParametrizedRequestHandler(params string[] terms) {
            return RequestHandler
                .From(RequestHandler)
                .WithQueryStringParam("titles", string.Join('|', terms))
                .Build();
        }
    }
}
