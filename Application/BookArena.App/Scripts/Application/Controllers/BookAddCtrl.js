(function(app) {
    "use strict";

    app.controller("BookAddCtrl", [
        "$scope", "$rootScope", "$location", "apiService", "identityService", "notifierService", "sharedService", "$timeout", "fileService",
        function ($scope, $rootScope, $location, apiService, identityService, notifierService, sharedService, $timeout, fileService) {

            $scope.init = function() {
                $(document).foundation();

                $scope.book = {};
                $scope.book.isRequired = true;
                $scope.categories = [];
                $scope.bookQuantities = sharedService.bookQuantities();

                apiService.get("/api/categories/").success(function(result) {
                    $scope.categories = result;
                });
            }();

            $scope.attachPhoto = function($files) {
                if ($files && $files.length) {
                    $scope.book.file = $files[0];

                    var errorMessages = [];

                    if ($scope.book.file.size > 1024 * 1024 * 2) {
                        errorMessages.push("File size is too large. Max upload size is 2MB.");
                    }

                    if (errorMessages.length) {
                        notifierService.notifyInfo(errorMessages);
                    } else {
                        if ($scope.fileReaderSupported && $scope.book.file.type.indexOf("image") > -1) {
                            var fileReader = new FileReader();

                            fileReader.readAsDataURL($scope.book.file);

                            var loadFile = function (fileReader) {
                                fileReader.onload = function (e) {
                                    $timeout(function () {
                                        $scope.book.filePreview = e.target.result;
                                    });
                                };
                            }(fileReader);
                        }
                    }
                }
            };

            $scope.add = function(book) {
                $scope.BookAddForm.submitted = true;

                if ($scope.BookAddForm.$valid) {
                    $scope.addingBook = true;

                    var uploadConfig = {
                        url: "/api/books/",
                        file: $scope.book.file,
                        data: book
                    };

                    fileService.upload(uploadConfig).progress(function (evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function () {
                        notifierService.notifySuccess("Book uploaded successfully!");
                        $scope.resetBookUploadForm();
                    }).error(function (errorResponse) {
                        $scope.addingBook = false;
                        $scope.displayErrors(errorResponse);
                    });
                }
            };

            $scope.resetBookUploadForm = function() {
                $scope.book.title = "";
                $scope.book.author = "";
                $scope.book.categoryId = "";
                $scope.book.quantity = "";
                $scope.book.shortDescription = "";

                $scope.addingBook = false;
            };
        }
    ]);
})(angular.module("bookArena"));