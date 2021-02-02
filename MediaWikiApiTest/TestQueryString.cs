using MediaWikiApi.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MediaWikiApiTest {
    [TestClass]
    public class TestQueryString {

        [TestMethod]
        public void TestSingleArguments() {
            QueryString qs = new QueryString();
            qs.Add("arg1", "value");
            qs.Add("arg1", "value2");
            Assert.AreEqual("?arg1=value2", qs.ToString());
            qs.Add("arg2", "value");
            Assert.AreEqual(true, qs.ToString().Contains("arg2=value"));
        }

        [TestMethod]
        public void TestListArguments() {
            QueryString qs = new QueryString();
            qs.AddList("arg1", "value");
            qs.AddList("arg1", "value2");
            Assert.AreEqual("?arg1=value&arg1=value2", qs.ToString());
            qs.AddList("arg2", "value");
            Assert.AreEqual(true, qs.ToString().Contains("arg2=value"));
        }

        [TestMethod]
        public void TestRemoveSingleArguments() {
            QueryString qs = new QueryString();
            qs.Add("arg1", "value");
            qs.RemoveKey("arg1");
            qs.Add("arg2", "value");
            Assert.AreEqual("?arg2=value", qs.ToString());
        }

        [TestMethod]
        public void TestRemoveListArguments() {
            QueryString qs = new QueryString();
            qs.AddList("arg1", "value");
            qs.AddList("arg1", "value2");
            qs.RemoveKey("arg1");
            qs.AddList("arg2", "value");
            Assert.AreEqual("?arg2=value", qs.ToString());
        }

        [TestMethod]
        public void TestGetArguments() {
            QueryString qs = new QueryString();
            qs.Add("arg1", "value");
            qs.Add("arg2", "value");
            bool result = qs.TryGet("arg1", out string value);
            Assert.AreEqual("value", value);
            Assert.IsTrue(result);
            result = qs.TryGet("arg3", out string valueNull);
            Assert.IsNull(valueNull);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestGetListArguments() {
            QueryString qs = new QueryString();
            qs.AddList("arg1", "value");
            qs.AddList("arg1", "value2");
            qs.AddList("arg2", "value");
            bool result = qs.TryGetList("arg1", out string[] values);
            Assert.IsTrue(values.SequenceEqual(new string[] { "value", "value2" }));
            Assert.IsTrue(result);
            result = qs.TryGetList("arg3", out string[] valuesNull);
            Assert.IsNull(valuesNull);
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestEnumerator() {
            QueryString qs = new QueryString();
            qs.AddList("arg1", "value");
            qs.AddList("arg1", "value2");
            qs.Add("arg2", "value2");
            int count = 0;
            foreach (var item in qs) {
                count++;
                Assert.IsInstanceOfType(item.Key, typeof(string));
                Assert.IsInstanceOfType(item.Value.Item1, typeof(ParamType));
                Assert.IsInstanceOfType(item.Value.Item2, typeof(string));
                if (item.Key.Equals("arg1")) {
                    Assert.AreEqual(ParamType.List, item.Value.Item1);
                } else if (item.Key.Equals("arg2")) {
                    Assert.AreEqual(ParamType.Single, item.Value.Item1);
                }
            }
            Assert.AreEqual(3, count);
        }
    }
}
