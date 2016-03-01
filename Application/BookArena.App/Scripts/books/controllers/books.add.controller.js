(function(app) {
    "use strict";

    app.controller("BookAddCtrl", [
        "$rootScope", "$location", "apiService", "identityService", "notifierService", "sharedService", "$timeout", "fileService",
        function($rootScope, $location, apiService, identityService, notifierService, sharedService, $timeout, fileService) {

            var vm = this;

            vm.init = function() {
                $(document).foundation();

                vm.book = {};
                vm.categories = [];
                vm.bookQuantities = sharedService.bookQuantities();

                apiService.get("/api/categories/").success(function(result) {
                    vm.categories = result;
                });
            }();

            vm.attachPhoto = function(file) {
                if (!file) {
                    return;
                }

                vm.file = file;

                var errorMessages = [];

                if (vm.file.size > 1024 * 1024 * 2) {
                    errorMessages.push("File size is too large. Max upload size is 2MB.");
                }

                if (errorMessages.length) {
                    notifierService.notifyInfo(errorMessages);
                } else {
                    if (fileService.fileReaderSupported && vm.file.type.indexOf("image") > -1) {
                        var fileReader = new FileReader();

                        fileReader.readAsDataURL(vm.file);

                        var loadFile = function(fileReader) {
                            fileReader.onload = function(e) {
                                $timeout(function() {
                                    vm.filePreview = e.target.result;
                                });
                            };
                        }(fileReader);
                    }
                }
            };

            vm.add = function() {
                vm.BookAddForm.submitted = true;

                if (vm.BookAddForm.$valid) {
                    vm.addingBook = true;

                    var uploadConfig = {
                        url: "/api/books/",
                        file: vm.file,
                        data: vm.book
                    };

                    fileService.upload(uploadConfig).progress(function(evt) {
                        console.log("percent: " + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data) {
                        notifierService.notifySuccess("Book uploaded successfully!");
                        vm.resetForm();
                        $location.path("/books/" + data.id);
                    }).error(function(errorResponse) {
                        vm.addingBook = false;
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };

            $rootScope.$on("Broadcast::RatingAvailable", function(event, score) {
                vm.book.rating = score;
            });

            vm.resetForm = function() {
                vm.book.title = "";
                vm.book.author = "";
                vm.book.category = "";
                vm.book.quantity = "";
                vm.book.shortDescription = "";

                vm.addingBook = false;
            };
        }
    ]);
})(angular.module("books"));