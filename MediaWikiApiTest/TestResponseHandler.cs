using MediaWikiApi.Wiki.Handler;
using MediaWikiApi.Wiki.Response.Query.Categories;
using MediaWikiApi.Wiki.Response.Query.Extracts;
using MediaWikiApi.Wiki.Response.Query.ImageInfo;
using MediaWikiApi.Wiki.Response.Query.Images;
using MediaWikiApi.Wiki.Response.Query.PageImages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MediaWikiApiTest {

    [TestClass]
    public class TestResponseHandler {

        [TestMethod]
        public void TestOpenSearchResponseHandler() {
            var osrh = new OpenSearchResponseHandler("https://www.wikipedia.org");
            var openSearch = osrh.RequestSingle("Google");
            Assert.IsNotNull(openSearch);
            Assert.IsNotNull(openSearch.LookupTerm);
            Assert.IsNotNull(openSearch.Titles);
            Assert.IsNotNull(openSearch.Urls);
        }


        [TestMethod]
        public void TestImagesResponseHandler() {
            var irh = new ImagesResponseHandler<Image>("https://www.wikipedia.org");
            var ir = irh.RequestSingle("Google");
            Assert.IsNotNull(ir);
            Assert.IsInstanceOfType(ir, typeof(IEnumerable<IImage>));
            Assert.IsTrue(ir.Count > 0);
            Assert.IsNotNull(ir.First().Title);
            var irl = irh.RequestMany("Google", "Google Drive");
            Assert.IsNotNull(irl);
            Assert.IsInstanceOfType(irl, typeof(IEnumerable<IEnumerable<IImage>>));
        }

        [TestMethod]
        public void TestImageInfoResponseHandler() {
            string[] imageTitles = new string[] { "File:Commons-logo.svg", "File:Edit-clear.svg" };
            var iirh = new ImageInfoResponseHandler<ImageInfoUrl>("https://www.wikipedia.org");
            var iir = iirh.RequestSingle(imageTitles[0]);
            Assert.IsNotNull(iir);
            Assert.IsInstanceOfType(iir, typeof(IImageInfo));
            var iirl = iirh.RequestMany(imageTitles);
            Assert.IsNotNull(iirl);
            Assert.IsInstanceOfType(iirl, typeof(IEnumerable<IImageInfo>));
        }

        [TestMethod]
        public void TestCategoriesResponseHandler() {
            var crh = new CategoriesResponseHandler<Category>("https://www.wikipedia.org");
            var cr = crh.RequestSingle("Google");
            Assert.IsNotNull(cr);
            Assert.IsTrue(cr.Count > 0);
            Assert.IsNotNull(cr.First().Title);
            Assert.IsInstanceOfType(cr, typeof(IEnumerable<ICategory>));
            var crl = crh.RequestMany("Google", "Google Drive");
            Assert.IsNotNull(crl);
            Assert.IsInstanceOfType(crl, typeof(IEnumerable<IEnumerable<ICategory>>));
        }

        [TestMethod]
        public void TestExtractResponseHandler() {
            var erh = new ExtractResponseHandler<HTMLExtractPage>("https://www.wikipedia.org");
            var er = erh.RequestSingle("Google");
            Assert.IsNotNull(er);
            Assert.IsInstanceOfType(er, typeof(IExtractPage));
            Assert.IsNotNull(er.Extract);
            Assert.IsNotNull(er.ExtractInterpreter);
            Assert.IsNotNull(er.ExtractInterpreter.Sections);
            Assert.IsTrue(er.ExtractInterpreter.Sections.Count > 0);
            var erl = erh.RequestMany("Google", "Google Drive");
            Assert.IsNotNull(erl);
            Assert.IsInstanceOfType(erl, typeof(IEnumerable<IExtractPage>));
        }

        [TestMethod]
        public void TestSourcePageImageResponseHandler() {
            var pirh = new PageImageResponseHandler<SourcePageImagePage>("https://www.wikipedia.org");
            pirh.AddQueryStringArgument("piprop", "original");
            var pir = pirh.RequestSingle("Google");
            Assert.IsNotNull(pir);
            Assert.IsInstanceOfType(pir, typeof(IPageImage));
            Assert.IsNotNull(pir.Source);
            var pirl = pirh.RequestMany("Google", "Google Drive");
            Assert.IsNotNull(pirl);
            Assert.IsInstanceOfType(pirl, typeof(IEnumerable<IPageImage>));
        }


    }
}
