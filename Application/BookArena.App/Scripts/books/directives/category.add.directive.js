(function(app) {
    "use strict";

    app.directive("categoryAddForm", function() {
        return {
            restrict: "A",
            templateUrl: "Scripts/books/views/category.add.view.html",
            link: function() {
                $(document).foundation();
            }
        };
    });
})(angular.module("books"));