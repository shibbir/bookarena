(function(app) {
    "use strict";

    app.factory("identityService", [
        "$rootScope", "$location", "$http", "apiService", function($rootScope, $location, $http, apiService) {

            var setAuthorizedUserData = function(data) {
                $rootScope.authenticatedUser = data;
            };

            var clearAuthorizedUserData = function() {
                $rootScope.authenticatedUser = {};
            };

            var getAuthorizedHeaders = function(accessToken) {
                return { "Authorization": "Bearer " + accessToken };
            };

            var getSecurityHeaders = function() {
                var accessToken = sessionStorage["accessToken"] || localStorage["accessToken"];

                if (accessToken) {
                    return getAuthorizedHeaders(accessToken);
                }
                return {};
            };

            var getUserInfo = function(accessToken) {
                var headers;

                if (accessToken) {
                    headers = getAuthorizedHeaders(accessToken);
                } else {
                    headers = getSecurityHeaders();
                }
                var config = {
                    headers: headers
                };
                return apiService.get("/api/Account/UserInfo", config);
            };

            var logout = function() {
                var config = {
                    headers: getSecurityHeaders()
                };
                return apiService.post("/api/Account/Logout", {}, config);
            };

            var createAccessDeniedResponse = function(path) {
                $rootScope.globalContainer = {
                    redirectTo: path.originalPath,
                    message: "Access Denied! You need to login first.",
                    notifyType: "error"
                };
            };

            var login = function(user) {
                var data = {
                    UserName: user.email,
                    Password: user.password,
                    grant_type: "password"
                };
                return $http({
                    method: "POST",
                    url: "/token",
                    data: data,
                    transformRequest: function(obj) {
                        var str = [];
                        for (var p in obj) {
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        }
                        return str.join("&");
                    }
                });
            };

            var setAccessToken = function(accessToken, persistent) {
                if (persistent) {
                    localStorage["accessToken"] = accessToken;
                } else {
                    sessionStorage["accessToken"] = accessToken;
                }
            };

            var getAccessToken = function() {
                return sessionStorage.getItem("accessToken") || localStorage.getItem("accessToken");
            };

            var clearAccessToken = function() {
                localStorage.removeItem("accessToken");
                sessionStorage.removeItem("accessToken");
            };

            return {
                setAuthorizedUserData: setAuthorizedUserData,
                clearAuthorizedUserData: clearAuthorizedUserData,
                createAccessDeniedResponse: createAccessDeniedResponse,
                getUserInfo: getUserInfo,
                login: login,
                logout: logout,
                isLoggedIn: getAccessToken,
                setAccessToken: setAccessToken,
                getAccessToken: getAccessToken,
                clearAccessToken: clearAccessToken,
                getSecurityHeaders: getSecurityHeaders
            };
        }
    ]);
})(_app);