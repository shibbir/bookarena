(function(app) {
    app.factory("notifierService", function() {
        return {
            notify: function(data) {
                var responseType = data.ResponseType.toLowerCase(),
                    response = data.Message;

                toastr.options = {
                    "debug": false,
                    "positionClass": "toast-bottom-right",
                    "onclick": null,
                    "fadeIn": 300,
                    "fadeOut": 1000,
                    "timeOut": 5000,
                    "extendedTimeOut": 1000,
                    "closeButton": true
                };

                switch (responseType) {
                case "success":
                    toastr.success(response);
                    break;
                case "error":
                    toastr.error(response);
                    break;
                case "warning":
                    toastr.warning(response);
                    break;
                case "info":
                    toastr.info(response);
                    break;
                }
            }
        };
    });
})(angular.module("bookArenaApp"));