(function(app) {
    "use strict";

    app.controller("BookDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService) {
            $scope.init = function() {
                $(document).foundation();
                $scope.book = {};
                $scope.data = {};
                $scope.searchedStudent = {};

                service.get("/api/books/" + $routeParams.id).success(function(result) {
                    $scope.book = result;
                });
            }();

            $scope.loginToEditBook = function() {
                $rootScope.globalContainer = {
                    redirectTo: "/books/edit/" + $routeParams.id
                };
                $location.path("/account/login/");
            };

            $scope.searchStudent = function() {
                if (identityService.isLoggedIn() && $scope.data.idCardNumber) {
                    $scope.searchStudentInProgress = true;

                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };

                    service.get("/api/students/search/" + $scope.data.idCardNumber, config).success(function(result) {
                        $scope.searchStudentInProgress = false;
                        if (result) {
                            $scope.searchedStudent = result;
                        } else {
                            $scope.searchedStudent = {};
                        }
                    });
                }
            };

            $scope.borrowBook = function(studentId, bookId) {
                if (identityService.isLoggedIn()) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };

                    service.post("/api/books/borrow?studentId=" + studentId + "&bookId=" + bookId, null, config).success(function(result) {
                        notifier.notifySuccess(result.message);
                        if (result.data) {
                            $scope.book.AvailableQuantity = result.data;
                        }
                    }).error(function(error) {
                        notifier.notifyError(error.message);
                    });
                }
            };

            $scope.displayBorrowButton = function() {
                if (identityService.isLoggedIn() && $scope.book.availableQuantity) {
                    return true;
                }
                return false;
            };

            $scope.resetBorrowSection = function() {
                $scope.data.idCardNumber = "";
                $scope.searchedStudent = {};
            };
        }
    ]);
})(angular.module("bookArena"));