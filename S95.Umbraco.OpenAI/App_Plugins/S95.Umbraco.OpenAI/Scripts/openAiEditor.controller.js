angular.module('umbraco').controller('openAiEditorController', [

    '$scope',
    'notificationsService',
    'openAiService',

    function ($scope, notificationsService, openAiService) {

        $scope.loading = false;

        const behaviorModel = [
            'Conservative',
            'Cautious',
            'Balanced',
            'Creative',
            'Adventurous'
        ];

        const maxCharacters = $scope.model.umbracoProperty.property.config.maxChars
            ? $scope.model.umbracoProperty.property.config.maxChars
            : ($scope.model.umbracoProperty.property.editor == 'Umbraco.TextBox' ? 512 : 1000);

        let labels = [];
        for (let i = 0; i < maxCharacters; i += maxCharacters / 5) {
            labels.push(i == 0 ? 10 : Math.floor(i));
        }
        labels.push(maxCharacters);

        $scope.maximumLengthOptions = {
            start: maxCharacters / 2,
            step: 10,
            range: {
                min: 10,
                max: maxCharacters
            },
            tooltips: [true],
            format: {
                to: (value) => Math.ceil(value),
                from: (value) => Math.ceil(value)
            },
            pips: {
                mode: 'values',
                density: 10,
                values: labels
            }
        };

        $scope.behaviorModelOptions = {
            start: 2,
            step: 1,
            range: {
                min: 0,
                max: 4
            },
            tooltips: [false],
            format: {
                to: (value) => Math.ceil(value),
                from: (value) => Math.ceil(value)
            },
            pips: {
                mode: 'steps',
                density: 100,
                filter: function (value, type) {
                    if (value == 1 || value == 3)
                        return 1;
                    else
                        return 2;
                },
                format: {
                    to: function (value) {
                        return behaviorModel[value];
                    },
                    from: function (value) {
                        return behaviorModel[value];
                    }
                }
            }
        };

        $scope.generateRequest = {
            type: '0',
            text: '',
            maximumLength: [maxCharacters / 2],
            behaviorModel: [2]
        };

        $scope.generateResponse = { text: '' };

        $scope.onMaximumLengthUpdate = function (values) {
            $scope.generateRequest.maximumLength = values;
        };

        $scope.onBehaviorModelUpdate = function (values) {
            $scope.generateRequest.behaviorModel = values;
        };

        $scope.generate = function () {
            $scope.loading = true;

            let tempRequest = {
                type: parseInt($scope.generateRequest.type),
                text: $scope.generateRequest.text,
                maximumLength: $scope.generateRequest.maximumLength[0],
                behaviorModel: $scope.generateRequest.behaviorModel[0]
            };

            openAiService.generate(
                tempRequest,
                function (response) {
                    $scope.generateResponse.text = response;
                    $scope.loading = false;
                },
                function (error) {
                    console.log(error);
                    notificationsService.error('Error', error.ExceptionMessage);
                });
        }

        $scope.submit = function () {
            $scope.model.umbracoProperty.property.value = $scope.generateResponse.text;
            notificationsService.success('Success', $scope.model.umbracoProperty.property.label + ' updated');
            $scope.model.close();
        }
    }
]);