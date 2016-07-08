# AngularJS TodoMVC Example
<img src="https://travis-ci.org/HIROSN/todomvc.svg" alt="Travis CI Badge"></img>

> HTML is great for declaring static documents, but it falters when we try to use it for declaring dynamic views in web-applications. AngularJS lets you extend HTML vocabulary for your application. The resulting environment is extraordinarily expressive, readable, and quick to develop.

> _[AngularJS - angularjs.org](http://angularjs.org)_


## Learning AngularJS
The [AngularJS website](http://angularjs.org) is a great resource for getting started.

Here are some links you may find helpful:

* [Tutorial](http://docs.angularjs.org/tutorial)
* [API Reference](http://docs.angularjs.org/api)
* [Developer Guide](http://docs.angularjs.org/guide)
* [Applications built with AngularJS](https://www.madewithangular.com/)
* [Blog](http://blog.angularjs.org)
* [FAQ](http://docs.angularjs.org/misc/faq)
* [AngularJS Meetups](http://www.youtube.com/angularjs)

Articles and guides from the community:

* [Code School AngularJS course](https://www.codeschool.com/courses/shaping-up-with-angular-js)
* [5 Awesome AngularJS Features](http://net.tutsplus.com/tutorials/javascript-ajax/5-awesome-angularjs-features)
* [Using Yeoman with AngularJS](http://briantford.com/blog/angular-yeoman.html)
* [me&ngular - an introduction to MVW](http://stephenplusplus.github.io/meangular)

Get help from other AngularJS users:

* [Walkthroughs and Tutorials on YouTube](http://www.youtube.com/playlist?list=PL1w1q3fL4pmgqpzb-XhG7Clgi67d_OHXz)
* [Google Groups mailing list](https://groups.google.com/forum/?fromgroups#!forum/angular)
* [angularjs on Stack Overflow](http://stackoverflow.com/questions/tagged/angularjs)
* [AngularJS on Twitter](https://twitter.com/angularjs)
* [AngularjS on Google +](https://plus.google.com/+AngularJS/posts)

_If you have other helpful links to share, or find any of the links above no longer work, please [let us know](https://github.com/tastejs/todomvc/issues)._

## Testsuite

The app uses [Karma](http://karma-runner.github.io/0.12/index.html) to run the tests located in the `test/` folder. To run the tests:

```
$ npm install
$ npm test
```
---
## ASP.NET Core
This project uses ASP.NET Core to host the TodoMVC application.

* [Getting Started with ASP.NET Core](https://github.com/aspnet/Home/)
* [Building a Web App with ASP.NET 5, MVC 6, EF7, and AngularJS](http://www.pluralsight.com/courses/aspdotnet-5-ef7-bootstrap-angular-web-app)
* [Create an ASP.NET 5 API app in Visual Studio Code](https://azure.microsoft.com/en-us/documentation/articles/app-service-create-aspnet-api-app-using-vscode/)

## .NET CLI Installation
See the instructions at [.NET Core getting started page](https://www.microsoft.com/net/core).

## Run application locally
```sh
npm install -g bower
npm install
bower install
npm start
```
and navigate to [http://localhost:5000](http://localhost:5000).

To use SQL backend, edit connection string in [config.json](config.json):
```JavaScript
{
  "Data": {
    "UseSqlBackend": "true",
    "TodoContextConnection": "Server=(localdb)\\ProjectsV12;Database=TodoDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```
and create a database using the following command:
```sh
dotnet ef migrations add InitialDatabase
```
