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

        var notifySuccess = function (message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function (msg) {
                    toastr.success(msg);
                });
            } else {
                toastr.success(message);
            }
        };

        var notifyError = function (message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function(err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(message);
            }
        };

        var notifyWarning = function (message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function (msg) {
                    toastr.warning(msg);
                });
            } else {
                toastr.warning(message);
            }
        };

        var notifyInfo = function (message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function (msg) {
                    toastr.info(msg);
                });
            } else {
                toastr.info(message);
            }
        };

        return {
            notifySuccess: notifySuccess,
            notifyError: notifyError,
            notifyWarning: notifyWarning,
            notifyInfo: notifyInfo
        };
    });
})(angular.module("bookArena"));