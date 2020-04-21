(function () {
    'use strict';

    angular.module('widgets').directive('avsShowDialogLink', function () {
        return {
            restrict: 'E',
            templateUrl: '/Angular/app/widgets/showDialogLink/show-dialog-link.html',
            scope: {
                isShow: '=',
                navigateUrl: '=',
                displayText: '='
            }
        };
    });
})();