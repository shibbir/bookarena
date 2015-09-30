(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", "identityService", function ($upload, identityService) {

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
                upload: upload
            };
        }
    ]);
})(angular.module("bookArena"));