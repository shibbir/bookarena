"use strict";

(function(app) {
    app.controller("BookLoanCtrl", [
        "$scope", "$rootScope", "$location", "$routeParams", "apiService", function($scope, $rootScope, $location, $routeParams, service) {
            $scope.data = {};
            $scope.searchedStudent = {};
            $scope.searchStudent = function() {
                if ($rootScope.authenticatedUser.IsAuthenticated && $scope.data.idCardNumber) {
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
            $scope.borrowBook = function (studentId, bookId) {
                if ($rootScope.authenticatedUser.IsAuthenticated) {
                    $scope.searchStudentInProgress = true;
                    service.call("/books/borrow?studentId=" + studentId + "&bookId=" + bookId, null, "POST").then(function (result) {
                        if (result.Data) {
                        } else {
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("bookArenaApp"));