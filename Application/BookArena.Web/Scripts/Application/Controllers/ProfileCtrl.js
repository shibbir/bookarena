"use strict";

(function(app) {
    app.controller("ProfileCtrl", [
        "$scope", "$rootScope", function($scope, $rootScope) {
            $rootScope.checkForPermisssionAfter();
        }
    ]);
})(angular.module("bookArenaApp"));