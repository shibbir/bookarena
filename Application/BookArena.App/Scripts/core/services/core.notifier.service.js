(function(app) {
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

        var formatErrors = function(messages) {
            var erroMessages = [];

            if (Array.isArray(messages)) {
                erroMessages = _.map(messages, function(error) {
                    return error.errorMessage;
                });
            } else {
                erroMessages.push("Something happened! Please try again.");
            }

            return erroMessages;
        };

        var notifySuccess = function(message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function(msg) {
                    toastr.success(msg);
                });
            } else {
                toastr.success(message);
            }
        };

        var notifyError = function(messages, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            messages = formatErrors(messages);

            messages.forEach(function(err) {
                toastr.error(err);
            });
        };

        var notifyWarning = function(message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function(msg) {
                    toastr.warning(msg);
                });
            } else {
                toastr.warning(message);
            }
        };

        var notifyInfo = function(message, options) {
            if (options && options.timeOut) {
                toastr.options.timeOut = options.timeOut;
            }

            if (Array.isArray(message)) {
                message.forEach(function(msg) {
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
})(angular.module("core"));