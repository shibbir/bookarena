(function(app) {
    "use strict";

    app.directive("categoryEditForm", function() {
        return {
            restrict: "A",
            templateUrl: "Scripts/books/views/category.edit.view.html",
            link: function() {
                $(document).foundation();
            }
        };
    });
})(angular.module("books"));