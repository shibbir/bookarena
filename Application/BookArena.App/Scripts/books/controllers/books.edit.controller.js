(function(app) {
    "use strict";

    app.controller("BookEditCtrl", [
        "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "fileService", "sharedService",
        function($rootScope, $routeParams, $location, apiService, notifierService, identityService, fileService, sharedService) {

            var vm = this;

            vm.init = function() {
                vm.book = {};
                vm.categories = [];

                apiService.get("/api/books/" + $routeParams.id).success(function(result) {
                    vm.book = result;

                    apiService.get("/api/categories/").success(function(categories) {
                        vm.categories = categories;
                    });
                }).error(function() {
                    $location.path("/").replace();
                });
            }();

            vm.update = function() {
                vm.BookEditForm.submitted = true;
                if (identityService.isLoggedIn() && vm.BookEditForm.$valid) {
                    vm.editingBook = true;

                    var config = {
                        headers: identityService.getSecurityHeaders()
                    }

                    apiService.put("/api/books/", vm.book, config).success(function() {
                        notifierService.notifySuccess("Book updated successfully!");
                        vm.editingBook = false;
                    }).error(function(errorResponse) {
                        vm.editingBook = false;
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };

            $rootScope.$on("Broadcast::RatingAvailable", function(event, score) {
                vm.book.rating = score;
            });

            vm.uploadPhoto = function(file) {
                if (!file) {
                    return;
                }

                var errorMessages = [];

                if (file.size > 1024 * 1024 * 2) {
                    errorMessages.push("File size is too large. Max upload size is 2MB.");
                }

                if (errorMessages.length) {
                    notifierService.notifyInfo(errorMessages);
                } else {
                    if (fileService.fileReaderSupported && file.type.indexOf("image") > -1) {
                        vm.editingBook = true;

                        var uploadConfig = {
                            url: "/api/books/upload",
                            file: file,
                            data: { id: $routeParams.id }
                        };
                        fileService.upload(uploadConfig).progress(function(evt) {
                            console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                        }).success(function(result) {
                            vm.book = result;
                            notifierService.notifySuccess("Cover updated successfully!");
                            vm.editingBook = false;
                        }).error(function(errorResponse) {
                            vm.editingBook = false;
                            sharedService.displayErrors(errorResponse);
                        });
                    }
                }
            };
        }
    ]);
})(angular.module("books"));