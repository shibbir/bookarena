"use strict";

(function () {
    describe("StudentListCtrl Test Block", function () {
        var scope, controller, identity;
        beforeEach(function () {
            module("bookArenaApp");

            inject(function ($rootScope, $controller, identityService) {
                scope = $rootScope.$new();
                controller = $controller("StudentListCtrl", { $scope: scope });
                identity = identityService;
            });
        });

        it("should define a controller a named StudentListCtrl", function () {
            expect(controller).toBeDefined();
        });

        it("should not have a student collection if the user don't have permission", function () {
            expect(scope.students).toBeUndefined();
        });

        it("should have a student collection if the user is logged in", function () {
            identity.setAuthorization({
                "Name": "Shibbir Ahmed",
                "Email": "shibbir@shibbir.net",
                "Address": "Dhaka, Bangladesh",
                "Website": "http://shibbir.net/",
                "ImageFileName": "profile.jpg"
            });
            expect(scope.students).toBeDefined();
        })
    });
})();