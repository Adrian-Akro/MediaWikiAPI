using System;
using System.Collections.Generic;

namespace MediaWikiApi.Requests {
    public class RequestHandler {
        public string BaseUrl { get; private set; }
        public string Endpoint { get; private set; }

        // TODO Allow argument manipulation of QueryString and make private or protected
        private QueryString queryString { get; set; }
        private RequestHandler() { }

        public string Make() {
            return WebClient.Request(
                new Uri($"{BaseUrl}{Endpoint}{queryString}")
                ).Result;
        }
        public RequestHandler AddArgument(string key, string value, ParamType type = ParamType.Single) {
            if (type.Equals(ParamType.Single))
                queryString.Add(key, value);
            else
                queryString.AddList(key, value);
            return this;
        }

        public bool RemoveArgument(string key) {
            return queryString.RemoveKey(key);
        }

        public IEnumerator<KeyValuePair<string, (ParamType, string)>> QueryStringArguments() => queryString.GetEnumerator();

        public static Builder From(RequestHandler request) {
            return new Builder(request);
        }

        public class Builder {
            private RequestHandler request;
            private QueryString queryString;

            public Builder(string baseUrl) {
                if (!baseUrl.StartsWith("http")) baseUrl = "http://" + baseUrl;
                if (!baseUrl.EndsWith("/")) baseUrl += "/";
                request = new RequestHandler();
                queryString = new QueryString();
                request.BaseUrl = baseUrl;
            }

            protected internal Builder(RequestHandler request) {
                this.request = new RequestHandler();
                this.request.BaseUrl = request.BaseUrl;
                this.request.Endpoint = request.Endpoint;
                queryString = new QueryString();
                foreach (KeyValuePair<string, (ParamType Type, string ParamValue)> kvp in request.queryString) {
                    switch (kvp.Value.Type) {
                        case ParamType.List:
                            queryString.AddList(kvp.Key, kvp.Value.ParamValue);
                            break;
                        case ParamType.Single:
                            queryString.Add(kvp.Key, kvp.Value.ParamValue);
                            break;
                        default:
                            break;
                    }
                }
            }

            public Builder WithEndpoint(string endpoint) {
                request.Endpoint = endpoint;
                return this;
            }

            public Builder WithQueryStringParam(string key, string value, ParamType type = ParamType.Single) {
                if (type.Equals(ParamType.Single))
                    queryString.Add(key, value);
                else
                    queryString.AddList(key, value);
                return this;
            }

            public Builder WithoutQueryStringParam(string key) {
                queryString.RemoveKey(key);
                return this;
            }

            public RequestHandler Build() {
                request.queryString = queryString;
                return request;
            }
        }
    }



}
