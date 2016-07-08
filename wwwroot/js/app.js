'use strict';
/*global angular */

/**
* The main TodoMVC app module
*
* @type {angular.Module}
*/
angular.module('todomvc', ['ngRoute', 'ngResource'])
.config(function($routeProvider, $httpProvider) {

  var routeConfig = {
    controller: 'TodoCtrl',
    templateUrl: 'todomvc-index.html',
    resolve: {
      store: function(todoStorage) {
        // Get the correct module (API or localStorage).
        return todoStorage.then(function(module) {
          module.get(); // Fetch the todo records in the background.
          return module;
        });
      }
    }
  };

  $routeProvider
  .when('/', routeConfig)
  .when('/:status', routeConfig)
  .otherwise({
    redirectTo: '/'
  });

  if (!$httpProvider.defaults.headers.get) {
    $httpProvider.defaults.headers.common = {};
  }
  $httpProvider.defaults.headers.common['Cache-Control'] = 'no-cache';
  $httpProvider.defaults.headers.common.Pragma = 'no-cache';
  $httpProvider.defaults.headers.common['If-Modified-Since'] = '0';
});
