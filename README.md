#Sandbox for fooling around with some ideas

What's currently in there: 

##ScraperTryout 

Wanted to see how selenium webdrived is working, what's good for and diff between HttpClient. 

##Pipes

Simple implementation for [pipelined processing](http://msdn.microsoft.com/en-us/library/ff963548.aspx) of data. 
Similar solution used in one web scraper project: scrape links from one page, visit them, 
find additional links on those pages, then scrape some data (with HtmlAgilityPack) and save it to db. 
Configuration of that process can be nicely modeled with this pipeline solution.  

Example:

```cs  

var buffers = new Buffers();

var tasks = ProcessBuilder.Create()
    .Pipe<BeginPipe, Nothing, BlockingCollection<string>>()
        .Output(() => buffers.First)
        .Wire
    .Pipe<ProcessPipe, BlockingCollection<string>, BlockingCollection<string>>()
        .Input(() => buffers.First)
        .Output(() => buffers.Second)
        .Wire
    .Pipe<SavePipe, BlockingCollection<string>, Nothing>()
        .Input(() => buffers.Second)
        .Wire
    .Build();

Task.WaitAll(tasks.ToArray());