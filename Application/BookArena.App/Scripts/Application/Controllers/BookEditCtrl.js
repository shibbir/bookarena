(function(app) {
    "use strict";

    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, apiService, notifierService, identityService) {
            $scope.init = function() {
                $scope.book = {};
                $scope.categories = [];

                apiService.get("/api/books/" + $routeParams.id).success(function(result) {
                    if (result) {
                        $scope.book = result;

                        apiService.get("/api/categories/").success(function(categories) {
                            $scope.categories = categories;
                        });
                    } else {
                        $location.path("/").replace();
                    }
                });
            }();

            $scope.update = function(book) {
                $scope.BookEditForm.submitted = true;
                if (identityService.isLoggedIn() && $scope.BookEditForm.$valid) {
                    $scope.editingBook = true;
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    }

                    apiService.put("/api/books/", book, config).success(function() {
                        notifierService.notifySuccess("Book updated successfully!");
                        $scope.editingBook = false;
                    }).error(function(errorResponse) {
                        $scope.editingBook = false;
                        $scope.displayErrors(errorResponse);
                    });
                }
            };
        }
    ]);
})(_app);