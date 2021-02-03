using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.Extracts {
    public interface IExtractInterpreter {
        List<Section> Sections { get; set; }
    }
}
