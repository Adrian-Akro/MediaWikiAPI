using System;
namespace MediaWikiApi.Requests.Exceptions {
    [Serializable]
    public class UnreachableUrlException : Exception {
        public UnreachableUrlException(Exception innerException) : base("The requested url could not be reached", innerException) {
        }

    }
}