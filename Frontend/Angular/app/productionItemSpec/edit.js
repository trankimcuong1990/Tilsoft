//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService','$timeout', function ($scope, dataService,$timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;

     $scope.productionItemSpecTypes = [
        { 'productionItemSpecTypeID': 1, 'productionItemSpecTypeNM': 'Color' },
        { 'productionItemSpecTypeID': 2, 'productionItemSpecTypeNM': 'Dimension' },
        { 'productionItemSpecTypeID': 2, 'productionItemSpecTypeNM': 'Other Description' }
    ];

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;

                    //console.log(data);

                    jQuery('#content').show();
                     // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    }); 

                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);

                    $scope.data = null;
                }
            );
        },
        update: function () {
            if($scope.data.productionItemSpecUD.length !== 4){
                jsHelper.showMessage('warning', 'Code must be in 4 characters format and unique!');
                return;
            }
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.productionItemSpecID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },

    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);