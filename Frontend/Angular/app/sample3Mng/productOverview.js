//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getProductOverviewData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data;
                        $('#content').show();

                        //
                        // add image to cache area - help loading images faster
                        //
                        $('#pnlCacheImg').append('<img src="' + $scope.data.finishedImageFileLocation + '" />');
                        angular.forEach($scope.data.progressDTOs, function (item) {
                            angular.forEach(item.progressImageDTOs, function (image) {
                                $('#pnlCacheImg').append('<img src="' + image.fileLocation + '" />');
                            });
                        });
                        angular.forEach($scope.data.referenceImageDTOs, function (item) {
                            $('#pnlCacheImg').append('<img src="' + item.fileLocation + '" />');
                        });
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        }
    };

    $scope.method = {
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);