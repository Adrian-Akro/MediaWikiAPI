namespace MediaWikiApi.Wiki.Response.Query.Extracts {
    public class HTMLExtractPage : IPage, IExtractPage {

        private string _extract;
        public string Extract {
            get => _extract;
            set {
                _extract = value;
                ExtractInterpreter = new HTMLExtractInterpreter(value);
            }
        }
        public int PageID { get; set; }
        public int NS { get; set; }
        public string Title { get; set; }
        public bool Missing { get; set; }

        public IExtractInterpreter ExtractInterpreter { get; set; }

    }
}
