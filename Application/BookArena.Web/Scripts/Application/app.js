"use strict";

(function() {
    var bookArenaApp = angular.module("bookArenaApp", ["ngRoute"]);

    bookArenaApp.config(["$routeProvider", function($routeProvider) {
        $routeProvider.when("/", {
            templateUrl: "Template/Book/Index.html",
            controller: "BookCtrl"
        }).when("/Student", {
            templateUrl: "Template/Student/Index.html",
            controller: "StudentCtrl"
        }).otherwise({
            redirectTo: "/"
        });
    }]);
})();