(function(app) {
    "use strict";

    app.directive("cdBookStatus", function() {
        return {
            restrict: "A",
            link: function($scope, $element, $attrs) {
                var qty = $attrs.cdBookStatus,
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
    }).directive("topBar", function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Templates/Common/Topbar.html"
        };
    }).directive("sideNavigation", function() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "/Templates/Common/SideNavigation.html"
        };
    }).directive("cdCategoryAddForm", function() {
        return {
            restrict: "A",
            templateUrl: "/Templates/Category/CategoryAddForm.html",
            link: function() {
                $(document).foundation();
            }
        };
    }).directive("cdCategoryEditForm", function() {
        return {
            restrict: "A",
            templateUrl: "/Templates/Category/CategoryEditForm.html",
            link: function() {
                $(document).foundation();
            }
        };
    }).directive("cdRating", function() {
        return {
            restrict: "A",
            link: function($scope, $element, $attrs) {
                $element.raty({
                    score: $attrs.cdRating,
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
    }).directive("cdRatingInput", [
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
    ]).directive("offCanvasMenu", [
        function() {
            return {
                restrict: "E",
                replace: true,
                templateUrl: "/Templates/Common/OffCanvasMenu.html"
            };
        }
    ]);
})(angular.module("bookArena"));