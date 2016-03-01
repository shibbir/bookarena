(function(app) {
    "use strict";

    app.directive("ratingInput", [
        "$rootScope", function($rootScope) {
            return {
                restrict: "A",
                link: function($scope, $element, $attrs) {
                    $element.raty({
                        score: $attrs.cdRatingInput,
                        half: true,
                        halfShow: true,
                        starHalf: "/Content/Images/star-half-big.png",
                        starOff: "/Content/Images/star-off-big.png",
                        starOn: "/Content/Images/star-on-big.png",
                        hints: ["Poor", "Average", "Good", "Very Good", "Excellent"],
                        target: "#Rating",
                        targetKeep: true,
                        noRatedMsg: "Not Rated yet!",
                        scoreName: "Rating",
                        click: function(score) {
                            $rootScope.$broadcast("Broadcast::RatingAvailable", score);
                        }
                    });
                }
            };
        }
    ]);
})(angular.module("books"));