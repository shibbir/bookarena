(function(app) {
    "use strict";

    app.controller("StudentDetailsCtrl", [
        "$rootScope", "$routeParams", "$location", "apiService", "notifierService", "identityService", "sharedService",
        function($rootScope, $routeParams, $location, apiService, notifierService, identityService, sharedService) {

            var vm = this;

            vm.programs = sharedService.programs();
            vm.batches = sharedService.batches();

            var config = {
                headers: identityService.getSecurityHeaders()
            };

            apiService.get("/api/students/" + $routeParams.id, config).success(function(data) {
                if (data.id) {
                    vm.student = angular.copy(data);
                    delete vm.student.transactions;

                    if (data.transactions && data.transactions.length) {
                        vm.transactions = data.transactions;
                    }
                } else {
                    $location.path("/").replace();
                }
            });

            vm.update = function() {
                vm.StudentEditForm.submitted = true;

                if (identityService.isLoggedIn() && vm.StudentEditForm.$valid) {
                    vm.editingStudent = true;

                    apiService.put("/api/students/", vm.student, config).success(function() {
                        notifierService.notifySuccess("Student updated successfully.");

                        vm.editingStudent = false;
                        vm.StudentEditForm.submitted = false;
                    }).error(function(errorResponse) {
                        vm.editingStudent = false;
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };
        }
    ]);
})(angular.module("bookArena"));