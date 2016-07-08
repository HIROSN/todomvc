'use strict';
/*global angular */

/**
* Services that persists and retrieves todos from localStorage or a backend API
* if available.
*
* They both follow the same API, returning promises for all changes to the
* model.
*/
angular.module('todomvc')
.factory('todoStorage', function($http, $injector) {

  // Detect if an API backend is present. If so, return the API module, else
  // hand off the localStorage adapter
  return $http.get('/api').then(function() {
    return $http.get('/api/login').then(function() {
      // User is already authorized, use the API module
      return $injector.get('api');
    }, function() {
      // Login to API backend
      return $http.post('/api/login', {
        name: 'default',
        password: 'P@ssw0rd!'
      }).then(function() {
        return $injector.get('api');
      }, function() {
        return $injector.get('localStorage');
      });
    });
  }, function() {
    return $injector.get('localStorage');
  });
})

.factory('api', function($resource) {
  var store = {
    todos: [],

    api: $resource('/api/todos/:id', null,
    {
      update: {method: 'PUT'}
    }),

    clearCompleted: function() {
      var originalTodos = store.todos.slice(0);

      var completeTodos = [];
      var incompleteTodos = [];
      store.todos.forEach(function(todo) {
        if (todo.completed) {
          completeTodos.push(todo);
        } else {
          incompleteTodos.push(todo);
        }
      });

      angular.copy(incompleteTodos, store.todos);

      return store.api.delete(function() {
      }, function error() {
        angular.copy(originalTodos, store.todos);
      });
    },

    delete: function(todo) {
      var originalTodos = store.todos.slice(0);

      store.todos.splice(store.todos.indexOf(todo), 1);
      return store.api.delete({id: todo.id},
        function() {
        }, function error() {
          angular.copy(originalTodos, store.todos);
        });
    },

    get: function() {
      return store.api.query(function(resp) {
        angular.copy(resp, store.todos);
      });
    },

    insert: function(todo) {
      var originalTodos = store.todos.slice(0);

      return store.api.save(todo,
        function success(resp) {
          todo.id = resp.id;
          store.todos.push(todo);
        }, function error() {
          angular.copy(originalTodos, store.todos);
        })
      .$promise;
    },

    put: function(todo) {
      return store.api.update({id: todo.id}, todo)
      .$promise;
    }
  };

  return store;
})

.factory('localStorage', function($q) {
  var STORAGE_ID = 'todos-angularjs';

  var store = {
    todos: [],

    _getFromLocalStorage: function() {
      return JSON.parse(localStorage.getItem(STORAGE_ID) || '[]');
    },

    _saveToLocalStorage: function(todos) {
      localStorage.setItem(STORAGE_ID, JSON.stringify(todos));
    },

    clearCompleted: function() {
      var deferred = $q.defer();

      var completeTodos = [];
      var incompleteTodos = [];
      store.todos.forEach(function(todo) {
        if (todo.completed) {
          completeTodos.push(todo);
        } else {
          incompleteTodos.push(todo);
        }
      });

      angular.copy(incompleteTodos, store.todos);

      store._saveToLocalStorage(store.todos);
      deferred.resolve(store.todos);

      return deferred.promise;
    },

    delete: function(todo) {
      var deferred = $q.defer();

      store.todos.splice(store.todos.indexOf(todo), 1);

      store._saveToLocalStorage(store.todos);
      deferred.resolve(store.todos);

      return deferred.promise;
    },

    get: function() {
      var deferred = $q.defer();

      angular.copy(store._getFromLocalStorage(), store.todos);
      deferred.resolve(store.todos);

      return deferred.promise;
    },

    insert: function(todo) {
      var deferred = $q.defer();

      store.todos.push(todo);

      store._saveToLocalStorage(store.todos);
      deferred.resolve(store.todos);

      return deferred.promise;
    },

    put: function(todo, index) {
      var deferred = $q.defer();

      store.todos[index] = todo;

      store._saveToLocalStorage(store.todos);
      deferred.resolve(store.todos);

      return deferred.promise;
    }
  };

  return store;
});
