angular.module('tilsoftApp').controller('ModelNoFactoryController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    
    //
    // property
    //
    //$scope.modelNoFactory = null;
    $scope.modelNoFactory = {};
    $scope.isLoaded = false;

    //
    // grid handler
    //
    $scope.modelNoFactory.gridHandler = {
        loadMore: function () {
            $scope.modelNoFactory.event.search();
            //console.log('load more');
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.modelNoFactory.filters.orderBy = sortedBy;
            $scope.modelNoFactory.filters.orderDirection = sortedDirection;
            $scope.event.reload();
        },
        isTriggered: false
    };

    //
    // events
    //
    $scope.modelNoFactory.event = {
        init: function () {
            $scope.modelNoFactory.method.getModelNoFactory(
                function (data) {
                    console.log("abc");
                    console.log(data);
                    $scope.modelNoFactory.data = data.data;
                    $scope.$apply();
                    $scope.isLoaded = true;
                },
                function (error) {

                });
        },
    };


    $scope.modelNoFactory.method = {
        getModelNoFactory: function (successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + modelNoFactoryContext.token
                },
                type: "POST",
                dataType: 'json',
                url: modelNoFactoryContext.serviceUrl + 'get-admin-dashboard',
                beforeSend: function () {
                    //jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    //jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.isLoaded = true;
        $scope.modelNoFactory.event.init();
    }, 0);
}]);