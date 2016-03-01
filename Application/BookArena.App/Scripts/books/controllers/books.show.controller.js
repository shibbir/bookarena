(function(app) {
    "use strict";

    app.controller("BookDetailsCtrl", [
        "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService",
        function($rootScope, $routeParams, $location, service, notifier, identityService) {

            var vm = this;

            vm.init = function() {
                $(document).foundation();
                vm.book = {};
                vm.data = {};
                vm.searchedStudent = {};

                service.get("/api/books/" + $routeParams.id).success(function(result) {
                    vm.book = result;
                });
            }();

            vm.loginToEditBook = function() {
                $rootScope.globalContainer = {
                    redirectTo: "/books/edit/" + $routeParams.id
                };
                $location.path("/account/login/");
            };

            vm.searchStudent = function() {
                if (identityService.isLoggedIn() && vm.data.idCardNumber) {
                    vm.searchStudentInProgress = true;

                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };

                    service.get("/api/students/search/" + vm.data.idCardNumber, config).success(function(result) {
                        vm.searchStudentInProgress = false;
                        if (result) {
                            vm.searchedStudent = result;
                        } else {
                            vm.searchedStudent = {};
                        }
                    }).error(function() {
                        vm.searchStudentInProgress = false;
                    });
                }
            };

            vm.borrowBook = function(studentId, bookId) {
                if (identityService.isLoggedIn()) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };

                    service.post("/api/books/borrow?studentId=" + studentId + "&bookId=" + bookId, null, config).success(function(result) {
                        notifier.notifySuccess(result.message);
                        if (result.data) {
                            vm.book.AvailableQuantity = result.data;
                        }
                    }).error(function(error) {
                        notifier.notifyError(error.message);
                    });
                }
            };

            vm.displayBorrowButton = function() {
                if (identityService.isLoggedIn() && vm.book.availableQuantity) {
                    return true;
                }
                return false;
            };

            vm.resetBorrowSection = function() {
                vm.data.idCardNumber = "";
                vm.searchedStudent = {};
            };
        }
    ]);
})(angular.module("books"));