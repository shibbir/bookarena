"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService) {
            $scope.init = function() {
                $(document).foundation();
                $scope.book = {};
                $scope.data = {};
                $scope.searchedStudent = {};

                service.call("/books/book/" + $routeParams.id).then(function(result) {
                    $scope.book = result.Data;
                });
            }();
            $scope.loginToEditBook = function() {
                $rootScope.globalContainer = {
                    redirectTo: "/books/edit/" + $routeParams.id
                };
                $location.path("/account/login/");
            };
            $scope.searchStudent = function() {
                if (identityService.isAuthenticated() && $scope.data.idCardNumber) {
                    $scope.searchStudentInProgress = true;
                    service.call("/students/studentbyidcard?idCard=" + $scope.data.idCardNumber).then(function(result) {
                        $scope.searchStudentInProgress = false;
                        if (result.Data) {
                            $scope.searchedStudent = result.Data;
                        } else {
                            $scope.searchedStudent = {};
                        }
                    });
                }
            };
            $scope.borrowBook = function(studentId, bookId) {
                if (identityService.isAuthenticated()) {
                    service.call("/books/borrow?studentId=" + studentId + "&bookId=" + bookId, null, "POST").then(function(result) {
                        notifier.notify(result.Response);
                        if (result.Data) {
                            $scope.book.AvailableQuantity = result.Data;
                        }
                    });
                }
            };
            $scope.displayBorrowButton = function() {
                if (identityService.isAuthenticated() && $scope.book.AvailableQuantity) {
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
})(angular.module("bookArenaApp"));