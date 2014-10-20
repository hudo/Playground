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

var context = new Context();

ProcessBuilder.Wireup()
    .Pipe<BeginPipe, NullStream, BlockingCollection<string>>()
        .Output(() => context.First)
        .FinishPipe
    .Pipe<ProcessPipe, BlockingCollection<string>, BlockingCollection<string>>()
        .Input(() => context.First)
        .Output(() => context.Second)
        .FinishPipe
    .Pipe<SavePipe, BlockingCollection<string>, NullStream>()
        .Input(() => context.Second)
        .FinishPipe
    .Go();
```

