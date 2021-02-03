using System;

namespace MediaWikiApi.Wiki.Handler.Exceptions {
    [Serializable]
    public class CouldNotParseException : Exception {
        public CouldNotParseException(Exception ex) : base($"The url provided is not a valid wiki url and the response could not be parsed", ex) {
        }

    }
}

