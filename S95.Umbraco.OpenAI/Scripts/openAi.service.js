const openAiEndpoint = '/umbraco/backoffice/api/openai/generate';

angular.module('umbraco.services').factory('openAiService', [

    '$http',

    function ($http) {

        return {
            generate: function (generateRequest, cb, cbError) {
                $http({
                    method: 'POST',
                    url: openAiEndpoint,
                    data: generateRequest,
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (response) {
                    if (cb) {
                        cb(response.data);
                    }
                }, function (reason) {
                    if (cbError) {
                        cbError(reason.data);
                    }
                });
            }
        };
    }
]);