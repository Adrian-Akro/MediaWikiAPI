# (Unofficial) MediaWiki C# API
This program provides a way to interact with any website that implements the MediaWiki API using an **HttpClient**.

## Basic use
To make use of this library you need to create an instance of the class **WikiApi** by providing a valid url for a website that uses the aforementioned API.
```
WikiApi api = new WikiApi("https://www.wikipedia.org/");
```
You can then use the different methods in this class to extract resources from a page by providing a valid page title.

### Obtaining a valid page title
To obtain a valid page title it's necessary to first perform an **OpenSearch**, by doing so the API will return a list of titles and URLs that match the search argument.

```
IOpenSearch openSearchResponse = api.Search("Google");
openSearchResponse.Titles
> [
> 	"Google",
> 	"Google Maps",
> 	"Google Drive",
> 	...
> ]
```

### Using a page title to perform queries

#### Get the main image from a page

```
IPageImage pageImage = api.GetPageImage("Google");
pageImage.Source
> "https://upload.wikimedia.org/[...].jpg"
```

#### Get all the images from page

```
IReadOnlyList<IImageInfoUrl> images = api.GetImageUrls("Google");
images[0].Url
> "https://upload.wikimedia.org/[...].jpg"
```

#### Get all the categories a page belongs to

```
IReadOnlyList<ICategory> categories = api.GetCategories("Google");
categories[0].Title
> "Category:1998 establishments in California"
```

#### Get the sections from page
A section has a title, its content and a list of subsections. 
The title of the first section is missing and it corresponds with the title of the page that was searched.
The depth of the subsections depend on how the wiki page itself is formed.
```
IReadOnlyList<Section> sections = api.GetSections("Google");
```


## Using the classes provided by the library to perform other queries
The following classes are designed to work on any queries derived from their base implementation, meaning that for example, a *ImageInfoResponseHandler* can be used to request any *ImageInfo* data if it's provided a class that implements *IImageInfo* and the right arguments.
```
> ImageInfoResponseHandler
> ExtractResponseHandler
> PageImageResponseHandler
> CategoriesResponseHandler
> ImagesResponseHandler
```

Example:
```
var imageInfoResponseHandler = new ImageInfoResponseHandler<FooImageInfoType>();
imageInfoResponseHandler
	.AddQueryStringArgument("iiprop", "myProp")
	.AddQueryStringArgument("exampleProp", "myValue");
FooImageInfoType fooImageInfo = imageInfoResponseHandler.RequestSingle("myTitle")
```

## Creating a new query
To create a new query you can extend the abstract class QueryHandler. Note that you may need to override some virtual methods since the default implementations may not work for your use case.
