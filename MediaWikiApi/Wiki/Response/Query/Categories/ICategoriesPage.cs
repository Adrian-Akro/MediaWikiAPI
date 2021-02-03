using System.Collections.Generic;

namespace MediaWikiApi.Wiki.Response.Query.Categories {
    public interface ICategoriesPage<T> : IPage where T : ICategory {
        List<T> Categories { get; set; }
    }
}
