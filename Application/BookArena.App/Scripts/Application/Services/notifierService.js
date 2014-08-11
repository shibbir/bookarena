(function (app) {
    "use strict";

    app.factory("notifierService", function() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        var notifySuccess = function(message) {
            toastr.success(message);
        };

        var notifyError = function(error) {
            if (Array.isArray(error)) {
                error.forEach(function(err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(error);
            }
        };

        var notifyWarning = function(message) {
            toastr.warning(message);
        };

        var notifyInfo = function(message) {
            toastr.info(message);
        };

        return {
            notifySuccess: notifySuccess,
            notifyError: notifyError,
            notifyWarning: notifyWarning,
            notifyInfo: notifyInfo
        };
    });
})(_app);