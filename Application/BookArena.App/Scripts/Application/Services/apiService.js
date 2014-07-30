"use strict";

(function(app) {
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

            var patch = function(url, data) {
                return $http({
                    url: url,
                    method: "PATCH",
                    data: data,
                    headers: {
                        "Content-Type": "application/json;charset=utf-8"
                    }
                });
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
})(_app);