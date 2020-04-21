tilsoftApp.controller('_EditWarehouseForm', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
    //
    //init controller
    //
    $scope.initController = function () {
        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //load data
        //
        if ($scope.data === null) {
            $scope.load(null);
        }   

        //
        //go to position before
        //
        $scope.goToCurrentScrollPosition();
    };

    $scope.productGrid = {
        loadNextPage: function () {
            //do nothing
        },
        sortData: function (sortedBy, sortedDirection) {
            //do nothing
        },
        getCurrentScrollPosition: function (position) {
            $scope.pagePosition.gridProductTop = position;
        }
    };

    $scope.addItem = function () {
        if (!$scope.data.offerSeasonDTO.offerSeasonID > 0) {
            bootbox.alert("Please save general info before add model");
            return;
        }
    };

    $scope.changeValue = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.isEditing = true;
    };

    $scope.currencies = [{ id: "USD", name: "USD" }, { id: "EUR", name: "EUR" }];

    $scope.getTotalOrderInCont = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null)
            return 0;
        var total = 0;
        angular.forEach($scope.data.offerSeasonDetailDTOs, function (item) {
            if (item.qnt40HC !== null && item.qnt40HC !== 0) {
                total += (item.quantity === null || item.quantity === '' ? 0 : parseFloat(item.quantity)) / item.qnt40HC;
            }            
        });
        return total;
    };
    //
    //init
    //    
    $timeout(function () {
        $scope.initController();
    });
}]);
