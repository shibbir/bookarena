(function(app) {
    "use strict";

    app.controller("StudentAddCtrl", [
        "$rootScope", "$location", "apiService", "notifierService", "identityService", "sharedService", function($rootScope, $location, apiService, notifierService, identityService, sharedService) {

            var vm = this;

            var config = {
                headers: identityService.getSecurityHeaders()
            };

            vm.programs = sharedService.programs();
            vm.batches = sharedService.batches();

            vm.register = function() {
                vm.StudentRegisterForm.submitted = true;

                if (vm.StudentRegisterForm.$valid) {
                    vm.addingStudent = true;

                    apiService.post("/api/students/", vm.student, config).success(function(data) {
                        notifierService.notifySuccess("Student registered successfully.");
                        vm.student.firstName = "";
                        vm.student.lastName = "";
                        vm.student.program = "";
                        vm.student.batch = "";
                        vm.student.idCardNumber = "";

                        vm.addingStudent = false;
                        $location.path("/students/" + data.id);
                    }).error(function(errorResponse) {
                        vm.addingStudent = false;
                        sharedService.displayErrors(errorResponse);
                    });
                }
            };
        }
    ]);
})(angular.module("students"));