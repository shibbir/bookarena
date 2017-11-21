(function(app) {
    "use strict";

    app.directive("categoryEditForm", function() {
        return {
            restrict: "A",
            templateUrl: "Scripts/books/views/edit.category.view.html",
            link: function() {
                $(document).foundation();
            }
        };
    });
})(angular.module("books"));