(function(app) {
    "use strict";

    app.factory("apiService", [
        "$http", function($http) {
            var get = function(url, config) {
                return $http.get(url, config);
            };

            var post = function(url, data, config) {
                return $http.post(url, data, config);
            };

            var put = function(url, data, config) {
                return $http.put(url, data, config);
            };

            var patch = function(url, data, config) {
                return $http.patch(url, data, config);
            };

            var remove = function(url, config) {
                return $http.delete(url, config);
            };

            return {
                get: get,
                post: post,
                put: put,
                patch: patch,
                remove: remove
            };
        }
    ]);
})(angular.module("core"));