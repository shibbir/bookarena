"use strict";

(function(app) {
    app.factory("apiService", [
        "$rootScope", "$http", "$q", function($rootScope, $http, $q) {
            var call = function(url, data, method) {
                var deferred = $q.defer();
                $http({
                    url: url,
                    method: method ? method : "GET",
                    data: data,
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                    }
                }).success(function(result) {
                    deferred.resolve(result);
                }).error(function(result, status) {
                    deferred.reject(status);
                });
                return deferred.promise;
            };
            return {
                call: call
            };
        }
    ]);
})(angular.module("bookArenaApp"));