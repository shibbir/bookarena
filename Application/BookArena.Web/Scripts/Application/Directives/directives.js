(function(app) {
    app.directive("cdBookStatus", function() {
        return {
            restrict: "A",
            link: function($scope, $element, $attrs) {
                var statusId = $attrs.cdBookStatus,
                    text;
                if (statusId === "1") {
                    text = "Available <i class='fi-check'></i>";
                    $element.addClass("green");
                } else if (statusId === "2") {
                    text = "Not Available <i class='fi-x'></i>";
                    $element.addClass("red");
                } else {
                    text = "Rented <i class='fi-alert'></i>";
                    $element.addClass("note");
                }
                $element.html(text);
            }
        };
    });
})(angular.module("bookArenaApp"));