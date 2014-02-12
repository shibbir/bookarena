"use strict";

(function() {
    describe("StudentListCtrl Test Block", function() {
        var scope, controller;
        beforeEach(function() {
            module("bookArenaApp");

            inject(function($rootScope, $controller) {
                scope = $rootScope.$new();
                controller = $controller("StudentListCtrl", { $scope: scope });
            });
        });

        it("should define a controller a named StudentListCtrl", function() {
            expect(controller).toBeDefined();
        });

        it("should have a list of students", function () {
            expect(scope.students).toBeDefined();
        });
    });
})();