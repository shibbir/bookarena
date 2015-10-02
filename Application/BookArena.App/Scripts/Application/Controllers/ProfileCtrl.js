(function(app) {
    "use strict";

    app.controller("ProfileCtrl", [
        function() {

            var vm = this;

            vm.projectInfo = {
                author: {
                    name: "Shibbir Ahmed",
                    website: "http://shibbir.net/",
                    twitter: "shibbir_net",
                    address: "Dhaka, Bangladesh",
                    gravatar: "profile.jpg"
                },
                tools: [
                    { title: "ASP.NET Web API", url: "http://www.asp.net/web-api" },
                    { title: "ASP.NET Identity", url: "http://www.asp.net/identity" },
                    { title: "Entity Framework", url: "http://www.asp.net/entity-framework" },
                    { title: "AngularJS", url: "http://angularjs.org/" },
                    { title: "AutoMapper", url: "http://automapper.org/" },
                    { title: "Toastr", url: "http://codeseven.github.io/toastr/" },
                    { title: "Foundation", url: "http://foundation.zurb.com/" },
                    { title: "MorrisJS", url: "http://www.oesmith.co.uk/morris.js/" }
                ]
            };
        }
    ]);
})(angular.module("bookArena"));