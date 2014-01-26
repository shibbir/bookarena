(function(app) {
    app.directive("cdBookStatus", function() {
            return {
                restrict: "A",
                link: function ($scope, $element, $attrs) {
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
        }).directive("cdNavigation", function() {
            return {
                restrict: "A",
                templateUrl: "/Template/Common/Navigation.html",
                link: function() {
                    $(document).foundation();
                }
            };
        }).directive("cdSideNavigation", function() {
            return {
                restrict: "A",
                templateUrl: "/Template/Common/SideNavigation.html"
            };
        })
        .directive("cdRating", function() {
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
                        hints: ["Bad", "Average", "Good", "Very Good", "Awesome"]
                    });
                }
            };
        });
})(angular.module("bookArenaApp"));