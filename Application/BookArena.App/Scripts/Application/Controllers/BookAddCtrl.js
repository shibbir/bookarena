"use strict";

(function(app) {
    app.controller("BookAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "identityService", "notifierService", "sharedService", function($scope, $rootScope, $location, apiService, identityService, notifierService, sharedService) {
            $(document).foundation();
            if (identityService.isLoggedIn()) {

                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                $scope.book = {};
                $scope.book.isRequired = true;
                $scope.categories = [];
                $scope.bookQuantities = sharedService.bookQuantities();

                apiService.get("/api/categories/").success(function(result) {
                    $scope.categories = result;
                });
            } else {
                identityService.createAccessDeniedResponse();
                $location.path("/account/login").replace();
            }
            $scope.add = function(book) {
                $scope.BookAddForm.submitted = true;
                if ($scope.BookAddForm.$valid) {
                    $scope.addingBook = true;
                    apiService.post("/api/books/", book, config).success(function(result) {
                        notifierService.notifySuccess(result.message);

                        $scope.book.title = "";
                        $scope.book.author = "";
                        $scope.book.categoryId = "";
                        $scope.book.quantity = "";
                        $scope.book.shortDescription = "";

                        $scope.addingBook = false;
                    }).error(function(error) {
                        notifierService.notifyError(error.message);
                        $scope.addingBook = false;
                    });
                }
            };
        }
    ]);
})(_app);