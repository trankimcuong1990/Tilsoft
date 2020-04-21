tilsoftApp.controller('_InitForm', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
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
        //        
        $scope.event.init();
    };

    //
    // property
    //
    $scope.offerSeasonTypeDTOs = [];
    $scope.param = {
        selectedOfferSeasonTypeID: 2
    };

    //
    // events
    //
    $scope.event = {
        init: function () {
            dataService.getOfferSeasonType(
                function (data) {
                    $scope.offerSeasonTypeDTOs = data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    //
                }
            );
        }              
    };
    //
    //init
    //
    $scope.initController();
}]);
