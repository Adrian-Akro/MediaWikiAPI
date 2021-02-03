using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query {
    public interface IQuery<T> where T : IContinueParameters {
        /// <summary>
        /// True if the query is complete, false if it can continue
        /// <item><inheritdoc/></item>
        /// </summary>
        bool BatchComplete { get; set; }
        Dictionary<string, WarningObject> Warnings { get; set; }

        /// <summary>
        /// Contains the parameters necessary to continue the request if it can
        /// <item><inheritdoc/></item>
        /// </summary>
        T Continue { get; set; }
    }
}
