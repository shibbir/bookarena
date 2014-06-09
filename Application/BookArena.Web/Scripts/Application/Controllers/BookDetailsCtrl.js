"use strict";

(function(app) {
    app.controller("BookDetailsCtrl", [
        "$scope", "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", function($scope, $rootScope, $routeParams, $location, service, notifier, identityService) {
            $scope.init = function() {
                $(document).foundation();
                $scope.book = {};
                $scope.data = {};
                $scope.searchedStudent = {};

                service.get("/books/book/" + $routeParams.id).success(function(result) {
                    $scope.book = result.data;
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
                    service.get("/students/studentbyidcard?idCard=" + $scope.data.idCardNumber).success(function(result) {
                        $scope.searchStudentInProgress = false;
                        if (result.data) {
                            $scope.searchedStudent = result.data;
                        } else {
                            $scope.searchedStudent = {};
                        }
                    });
                }
            };
            $scope.borrowBook = function(studentId, bookId) {
                if (identityService.isAuthenticated()) {
                    service.post("/books/borrow?studentId=" + studentId + "&bookId=" + bookId, null).success(function(result) {
                        notifier.notify(result.response);
                        if (result.data) {
                            $scope.book.AvailableQuantity = result.data;
                        }
                    });
                }
            };
            $scope.displayBorrowButton = function() {
                if (identityService.isAuthenticated() && $scope.book.availableQuantity) {
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