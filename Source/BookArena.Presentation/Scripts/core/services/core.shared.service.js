﻿(function(app) {
    "use strict";

    app.factory("sharedService", [
        "notifierService", function(notifierService) {
            var bookQuantities = function() {
                return [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20];
            };

            var programs = function() {
                return ["BBA", "CSE", "CSI", "CEN", "ECO", "EEE", "ENG"];
            };

            var batches = function() {
                return ["25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50"];
            };

            var displayErrors = function(errorResponse) {
                if (errorResponse.modelState) {
                    var errors = _.flatten(_.map(errorResponse.modelState, function(items) {
                        return items;
                    }));
                    notifierService.notifyError(errors);
                } else {
                    notifierService.notifyError(errorResponse.message);
                }
            };

            return {
                bookQuantities: bookQuantities,
                programs: programs,
                batches: batches,
                displayErrors: displayErrors
            };
        }
    ]);
})(angular.module("core"));