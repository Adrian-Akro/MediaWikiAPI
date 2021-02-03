using MediaWikiApi.Requests;
using MediaWikiApi.Requests.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaWikiApiTest {

    [TestClass]
    public class TestRequestHandler {

        [TestMethod]
        public void TestInvalidRequest() {
            RequestHandler rh = new RequestHandler
                .Builder("255.255.255.255")
                .WithEndpoint("/")
                .WithQueryStringParam("param1", "value")
                .WithQueryStringParam("param2", "value", ParamType.List)
                .Build();
            Assert.ThrowsException<UnreachableUrlException>(rh.Make);
        }

        [TestMethod]
        public void TestValidRequest() {
            RequestHandler rh = new RequestHandler
                .Builder("www.google.com")
                .WithEndpoint("/")
                .WithQueryStringParam("param1", "value")
                .WithQueryStringParam("param2", "value", ParamType.List)
                .Build();
            Assert.IsInstanceOfType(rh.Make(), typeof(string));
        }

        [TestMethod]
        public void TestCopyRequestHandler() {
            RequestHandler rh = new RequestHandler
                .Builder("www.google.com")
                .WithEndpoint("/")
                .WithQueryStringParam("param1", "value")
                .Build();
            RequestHandler rhCopy = RequestHandler.From(rh).Build();
            var enum1 = rh.QueryStringArguments;
            var enum2 = rhCopy.QueryStringArguments;
            Assert.AreNotEqual(rh, rhCopy);
            Assert.IsTrue(enum1.MoveNext());
            Assert.IsTrue(enum2.MoveNext());
            Assert.AreEqual(enum1.Current.Value.Item2, enum2.Current.Value.Item2);
        }

    }
}
