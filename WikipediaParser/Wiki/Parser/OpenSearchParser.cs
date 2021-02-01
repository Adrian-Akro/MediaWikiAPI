using MediaWikiApi.Wiki.Response.OpenSearch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Parser {
    class OpenSearchParser : IParser<OpenSearch> {

        public OpenSearch Parse(string requestResult) {
            JArray jArray = JsonConvert.DeserializeObject<JArray>(requestResult);
            return new OpenSearch {
                LookupTerm = jArray[0].ToObject<string>(),
                Titles = jArray[1].ToObject<List<string>>(),
                Urls = jArray[3].ToObject<List<string>>()
            };
        }
    }
}
