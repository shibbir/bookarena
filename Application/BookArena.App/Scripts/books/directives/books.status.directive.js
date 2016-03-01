(function(app) {
    "use strict";

    app.directive("bookStatus", [
        function() {
            return {
                restrict: "A",
                link: function($scope, $element, $attrs) {
                    var qty = $attrs.bookStatus,
                        text;
                    if (parseInt(qty) > 0) {
                        text = "Available <i class='fi-check'></i>";
                        $element.addClass("green");
                    } else {
                        text = "Not Available <i class='fi-x'></i>";
                        $element.addClass("red");
                    }
                    $element.html(text);
                }
            };
        }
    ]);
})(angular.module("books"));