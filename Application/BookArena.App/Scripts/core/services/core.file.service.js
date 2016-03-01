(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", "identityService", function($upload, identityService) {

            var isFileReaderSupported = window.FileReader != null && (window.FileAPI == null || FileAPI.html5 != false);

            var postMultipartForm = function(config) {
                return $upload.upload({
                    url: config.url,
                    method: config.method || "POST",
                    headers: identityService.getSecurityHeaders(),
                    data: config.data
                });
            };

            return {
                isFileReaderSupported: isFileReaderSupported,
                postMultipartForm: postMultipartForm
            };
        }
    ]);
})(angular.module("core"));