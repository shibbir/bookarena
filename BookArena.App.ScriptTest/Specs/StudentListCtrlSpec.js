"use strict";

(function() {
    describe("StudentListCtrl", function() {
        var scope, controller, identity, rootScope;
        beforeEach(function() {
            module("bookArena");

            inject(function($rootScope, $controller, identityService) {
                rootScope = $rootScope;
                scope = $rootScope.$new();
                controller = $controller("StudentListCtrl", { $scope: scope });
                identity = identityService;
            });
        });

        it("should define a controller a named StudentListCtrl", function() {
            expect(controller).toBeDefined();
        });

        it("should not have a student collection if the user don't have permission", function() {
            expect(scope.students).toBeUndefined();
        });

        it("should have a student collection if the user is logged in", function() {
            identity.setAuthorizationData({
                "Name": "Shibbir Ahmed",
                "Email": "shibbir@shibbir.net"
            });
            expect(scope.students).toBeDefined();
        });
    });
})();