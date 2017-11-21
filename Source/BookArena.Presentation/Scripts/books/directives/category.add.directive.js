(function(app) {
    "use strict";

    app.directive("categoryAddForm", function() {
        return {
            restrict: "A",
            templateUrl: "Scripts/books/views/add.category.view.html",
            link: function() {
                $(document).foundation();
            }
        };
    });
})(angular.module("books"));