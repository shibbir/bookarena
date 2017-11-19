"use strict";

var ApplicationConfiguration = (function() {
    var applicationModuleName = "bookArena";
    var applicationModuleVendorDependencies = ["ngRoute", "ngMessages", "ngFileUpload"];

    var registerModule = function(moduleName, dependencies) {
        angular.module(moduleName, dependencies || []);

        angular.module(applicationModuleName).requires.push(moduleName);
    };

    return {
        applicationModuleName: applicationModuleName,
        applicationModuleVendorDependencies: applicationModuleVendorDependencies,
        registerModule: registerModule
    };
})();
