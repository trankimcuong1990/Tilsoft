tilsoftApp.controller('_EditFobSparepartForm', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
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

    //
    //init
    //    
    $timeout(function () {
        $scope.initController();
    });
}]);
