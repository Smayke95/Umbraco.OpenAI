﻿angular.module('umbraco').component('openAiButton', {
    templateUrl: '/App_Plugins/S95.Umbraco.OpenAI/Views/openAiButton.html',
    require: {
        umbProperty: '^^umbProperty'
    },
    controller: [

        '$scope',
        '$element',
        '$timeout',
        'editorService',

        function (
            $scope,
            $element,
            $timeout,
            editorService
        ) {

            const allowedEditors = [
                'Umbraco.TextBox',
                'Umbraco.TextArea',
                'Umbraco.TinyMCE'
            ];

            this.$onInit = () => {
                if (this.umbProperty == null || this.umbProperty.property == null || !allowedEditors.includes(this.umbProperty.property.editor)) {
                    $scope.remove();
                    return;
                }

                $scope.updatePositionInDom();
            }

            $scope.remove = () => {
                $element.remove();
            }

            $scope.updatePositionInDom = () => {
                $timeout(() => {
                    const $controls = $element.prev('.controls');
                    const $propertyEditor = $controls.find('ng-form');

                    if ($propertyEditor.length > 0) {
                        $propertyEditor.append($element);
                        $propertyEditor.css('display', 'flex');
                        $propertyEditor.css('gap', '10px');
                    }
                }, 200);
            }

            $scope.openAiEditor = () => {

                let options = {
                    view: '/App_Plugins/S95.Umbraco.OpenAI/Views/openAiEditor.html',
                    size: 'small',
                    umbracoProperty: this.umbProperty,
                    close: function () {
                        editorService.close();
                    }
                }

                editorService.open(options);
            }
        }
    ]
});