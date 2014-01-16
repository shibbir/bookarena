(function(app) {
    app.factory("apiService", [
        "$http", "$q", function($http, $q) {
            var call = function(url, params, method) {
                var deferred = $q.defer();
                $http({
                    url: url,
                    method: method ? method : "GET",
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
                call: call
            };
        }
    ]);
})(angular.module("bookArenaApp"));