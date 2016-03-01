(function(app) {
    "use strict";

    app.directive("rating", [function() {
        return {
            restrict: "A",
            link: function($scope, $element, $attrs) {
                $element.raty({
                    score: $attrs.rating,
                    halfShow: true,
                    readOnly: true,
                    noRatedMsg: "Not rated yet!",
                    starHalf: "/Content/Images/star-half.png",
                    starOff: "/Content/Images/star-off.png",
                    starOn: "/Content/Images/star-on.png",
                    hints: ["Poor", "Average", "Good", "Very Good", "Excellent"]
                });
            }
        };
    }]);
})(angular.module("books"));