"use strict";

(function() {
    describe("StudentListCtrl Test Block", function() {
        var scope, controller, identity;
        beforeEach(function() {
            module("bookArenaApp");

            inject(function($rootScope, $controller, identityService) {
                scope = $rootScope.$new();
                controller = $controller("StudentListCtrl", { $scope: scope });
                identity = identityService;
            });
        });

        it("should define a controller a named StudentListCtrl", function() {
            expect(controller).toBeDefined();
        });

        it("should have a student collection only if the user has permission", function () {
            console.log(identity.isAuthenticated());
            if (identity.isAuthenticated()) {
                expect(scope.students).toBeDefined();
            } else {
                expect(scope.students).toBeUndefined();
            }
        });
    });
})();