# PROJECT SPECIFICATION #

## High level architecture ##

WARNING: intentionally over-engineered!

Even though this is really simple app, not even CRUD since it contains just 'R' part of it, 
more complete project structure is implemented, just to show architecture level opinions of the Author.

[Onion architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) was used as a guideline, 
or at least some parts which fits this simple task. 


## Projects overview ##

### Domain ###

Represents core domain from Onion architecture. Contains domain model and core types which are used throughout the whole application.

### Persistency.Inmemory ###

Just a simple in-memory collection of planets, to avoid complications with database and ORMs. 

### Web ###

OWIN-based web application, hosted in web project by System.Web. Contains middlewares:   

 - LogginConsoleMiddleware, just a simple custom middleware that logs request URLs into debug window of VS  
 - StaticFiles, to server static assets like HTML and JS files  
 - WebAPI, maybe overkill, but configuration and usage are simple enough, servers are cheap. And time is money:)

To keep simplicity of used 3th party libs, just jquery is used, and not some other client-side MVC/binding/templating framework like Knockout, 
despite pure desire to build client SPA app, and it would in fact bring a value by reducing number of lines of code. 

**Client app location**: Scripts/App.js    
**CSS framework**: Bootstrap  
**Ajax calls**: load of all planets, load info about selected planet.  
**REST API**: GET api/planets/(id)  
**Start page**: /  

### Tests.Integration ###

Few simple integration tests of REST API endpoints, with OWIN-based testing library.   
Test framework used: NUnit