"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$rootScope", "$location", "identityService", function($scope, $rootScope, $location, identityService) {
            if (!identityService.isAuthenticated()) {
                $rootScope.globalContainer = {
                    redirectTo: $location.path(),
                    response: {
                        ResponseType: "error",
                        Message: "Access Denied! You need to login first."
                    }
                };
                $location.path("/account/login").replace();
            }

            $scope.tools = [
                { title: "AngularJS", url: "http://angularjs.org/" },
                { title: "ASP.NET MVC 5", url: "http://www.asp.net/mvc" },
                { title: "AutoMapper", url: "http://automapper.org/" },
                { title: "Entity Framework", url: "http://www.asp.net/entity-framework" },
                { title: "Toastr", url: "http://codeseven.github.io/toastr/" },
                { title: "Foundation", url: "http://foundation.zurb.com/" },
                { title: "MorrisJS", url: "http://www.oesmith.co.uk/morris.js/" }
            ];
        }
    ]);
})(angular.module("bookArenaApp"));