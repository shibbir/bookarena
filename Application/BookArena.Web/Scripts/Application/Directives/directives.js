(function(app) {
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
    }).directive("cdTopbar", function() {
        return {
            restrict: "A",
            templateUrl: "/Template/Common/Topbar.html",
            link: function() {
                $(document).foundation();
            }
        };
    }).directive("cdSideNavigation", function() {
        return {
            restrict: "A",
            templateUrl: "/Template/Common/SideNavigation.html"
        };
    }).directive("cdCategoryAddForm", function() {
        return {
            restrict: "A",
            templateUrl: "/Template/Category/CategoryAddForm.html",
            link: function() {
                $(document).foundation();
            }
        };
    }).directive("cdCategoryEditForm", function() {
        return {
            restrict: "A",
            templateUrl: "/Template/Category/CategoryEditForm.html",
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
    }).directive("cdRatingInput", function() {
        return {
            restrict: "A",
            link: function($scope, $element) {
                $element.raty({
                    half: true,
                    halfShow: true,
                    starHalf: "/Content/Images/star-half-big.png",
                    starOff: "/Content/Images/star-off-big.png",
                    starOn: "/Content/Images/star-on-big.png",
                    hints: ["Poor", "Average", "Good", "Very Good", "Excellent"],
                    size: 24,
                    target: "#Rating",
                    targetKeep: true,
                    targetText: "Not Rated yet!",
                    scoreName: "Rating"
                });
            }
        };
    });
})(angular.module("bookArenaApp"));