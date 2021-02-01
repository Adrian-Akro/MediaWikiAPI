using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.Extracts {
    public class Section {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Section> Subsections { get; set; } = new List<Section>();
    }
}
