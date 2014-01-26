"use strict";

(function(app) {
    app.factory("apiService", [
        "$rootScope", "$http", "$q", function($rootScope, $http, $q) {
            var call = function(url, data, method) {
                $rootScope.fetchInProgress = true;
                var deferred = $q.defer();
                $http({
                    url: url,
                    method: method ? method : "GET",
                    data: data,
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                    }
                }).success(function(result) {
                    $rootScope.fetchInProgress = false;
                    deferred.resolve(result);
                }).error(function (result, status) {
                    $rootScope.fetchInProgress = false;
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