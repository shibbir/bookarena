(function(app) {
    "use strict";

    app.controller("BookEditCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "fileService",
        function($scope, $rootScope, $routeParams, $location, apiService, notifierService, identityService, fileService) {
            $scope.init = function() {
                $scope.book = {};
                $scope.categories = [];

                apiService.get("/api/books/" + $routeParams.id).success(function(result) {
                    $scope.book = result;

                    apiService.get("/api/categories/").success(function(categories) {
                        $scope.categories = categories;
                    });
                }).error(function() {
                    $location.path("/").replace();
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

            $rootScope.$on("Broadcast::RatingAvailable", function(event, score) {
                $scope.book.rating = score;
            });

            $scope.uploadPhoto = function(file) {
                var errorMessages = [];

                if (file.size > 1024 * 1024 * 2) {
                    errorMessages.push("File size is too large. Max upload size is 2MB.");
                }

                if (errorMessages.length) {
                    notifierService.notifyInfo(errorMessages);
                } else {
                    if ($scope.fileReaderSupported && file.type.indexOf("image") > -1) {
                        $scope.editingBook = true;

                        var uploadConfig = {
                            url: "/api/books/upload",
                            file: file,
                            data: { id: $routeParams.id }
                        };
                        fileService.upload(uploadConfig).progress(function(evt) {
                            console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                        }).success(function(result) {
                            $scope.book = result;
                            notifierService.notifySuccess("Cover updated successfully!");
                            $scope.editingBook = false;
                        }).error(function(errorResponse) {
                            $scope.editingBook = false;
                            $scope.displayErrors(errorResponse);
                        });
                    }
                }
            };
        }
    ]);
})(angular.module("bookArena"));