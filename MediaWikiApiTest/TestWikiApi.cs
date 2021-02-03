using MediaWikiApi.Requests.Exceptions;
using MediaWikiApi.Wiki;
using MediaWikiApi.Wiki.Handler.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaWikiApiTest {

    [TestClass]
    public class TestWikiApi {

        [TestMethod]
        public void TestInstantiation() {
            Assert.IsTrue(WikiApi.IsValidWikiUrl("https://www.wikipedia.org"));
            Assert.IsFalse(WikiApi.IsValidWikiUrl("255.255.255.255"));
            Assert.IsFalse(WikiApi.IsValidWikiUrl("www.google.com"));
        }
    }
}
