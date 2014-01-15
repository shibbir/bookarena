(function(app) {
    app.factory("DataService", [
        "$http", "$q", function($http, $q) {
            var get = function(url, params) {
                var deferred = $q.defer();
                $http({
                    url: url,
                    method: "GET",
                    data: params,
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                    }
                }).success(function(data) {
                    deferred.resolve(data);
                }).error(function(data, status) {
                    deferred.reject(status);
                });
                return deferred.promise;
            };
            var post = function(url, params) {
                var deferred = $q.defer();
                $http({
                    url: url,
                    method: "POST",
                    data: params,
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
                    }
                }).success(function(data) {
                    deferred.resolve(data);
                }).error(function(data, status) {
                    deferred.reject(status);
                });
                return deferred.promise;
            };
            return {
                get: get,
                post: post
            };
        }
    ]);
})(angular.module("bookArenaApp"));