(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", "identityService", function($upload, identityService) {

            var fileReaderSupported = window.FileReader != null && (window.FileAPI == null || FileAPI.html5 != false);

            var upload = function(config) {
                return $upload.upload({
                    url: config.url,
                    method: config.method || "POST",
                    headers: identityService.getSecurityHeaders(),
                    file: config.file,
                    data: config.data
                });
            };

            return {
                upload: upload,
                fileReaderSupported: fileReaderSupported
            };
        }
    ]);
})(angular.module("bookArena"));