(function () {
    "use strict";

    describe("BookListCtrl Test Block", function() {
        var scope, controller;
        beforeEach(function() {
            module("bookArena");

            inject(function($rootScope, $controller) {
                scope = $rootScope.$new();
                controller = $controller("BookListCtrl", { $scope: scope });
            });
        });

        it("should define a controller a named BookListCtrl", function() {
            expect(controller).toBeDefined();
        });

        it("should define a category collection", function() {
            expect(scope.categories).toBeDefined();
        });
    });
})();