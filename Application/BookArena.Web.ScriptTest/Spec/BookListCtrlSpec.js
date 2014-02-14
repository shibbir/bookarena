"use strict";

(function() {
    describe("BookListCtrl Test Block", function() {
        var scope, controller;
        beforeEach(function() {
            module("bookArenaApp");

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