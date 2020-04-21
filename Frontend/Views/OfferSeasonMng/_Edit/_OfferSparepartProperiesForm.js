tilsoftApp.controller('_OfferSparepartProperiesForm', ['$scope', '$timeout', '$cookieStore', 'dataService', '$routeParams', function ($scope, $timeout, $cookieStore, dataService, $routeParams) {
    //
    //property
    //
    $scope.offerSeasonDetailID = null;
    $scope.offerDetailItem = null;
    $scope.modelSpareparts = [];

    //
    //init controller
    //

    $scope.initController = function () {
        //
        //tracking position
        //
        $scope.pagePosition.pageYOffset = window.pageYOffset;
        window.scrollTo(0, 0);

        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //get param router
        //
        $scope.offerSeasonDetailID = $routeParams.offerSeasonDetailID;

        //
        //load data
        //
        if ($scope.data !== null) {
            $scope.offerDetailItem = $scope.data.offerSeasonDetailDTOs.filter(o => parseInt(o.offerSeasonDetailID) === parseInt($scope.offerSeasonDetailID))[0];      
            dataService.searchModelSparepart(
                $scope.offerDetailItem.modelID,
                function (data) {
                    jQuery('#content').show();
                    $scope.modelSpareparts = data.data;
                },
                function (error) {
                }
            );            
        }           
    };

    $scope.changeModelSparepart = function (partID) {        
        var part = $scope.modelSpareparts.filter(o => o.modelSparepartID === partID);
        if (part.length > 0) {
            $scope.offerDetailItem.articleCode = part[0].modelUD + '-S-' + part[0].partUD;
            $scope.offerDetailItem.description = 'SPAREPART / ' + part[0].modelNM + ' / ' + part[0].description;                  
        }

    };
      
    //
    //init
    //
    $scope.initController();
}]);
