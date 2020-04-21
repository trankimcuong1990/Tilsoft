tilsoftApp.controller('_PlanningPurchasingPriceForm', ['$scope', '$timeout', '$cookieStore', 'dataService', '$routeParams', function ($scope, $timeout, $cookieStore, dataService, $routeParams) {
    //
    //property
    //
    $scope.originRouter = $routeParams.originRouter;
    $scope.offerSeasonDetailID = $routeParams.offerSeasonDetailID;
    $scope.offerDetailItem = null;
    $scope.planningPurchasingPriceDTOs = [];
    $scope.factoryPendingPriceDTOs = [];
    $scope.offerDetailItemCopy = null;

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
        //load data
        //
        if ($scope.data !== null) {
            $scope.offerDetailItem = $scope.data.offerSeasonDetailDTOs.filter(o => parseInt(o.offerSeasonDetailID) === parseInt($scope.offerSeasonDetailID))[0];
            $scope.offerDetailItemCopy = angular.copy($scope.offerDetailItem);

            //get properties
            var properties = {
                clientID: $scope.data.offerSeasonDTO.clientID,
                modelID: $scope.offerDetailItem.modelID,
                materialID: $scope.offerDetailItem.materialID,
                materialTypeID: $scope.offerDetailItem.materialTypeID,
                materialColorID: $scope.offerDetailItem.materialColorID,
                frameMaterialID: $scope.offerDetailItem.frameMaterialID,
                frameMaterialColorID: $scope.offerDetailItem.frameMaterialColorID,
                subMaterialID: $scope.offerDetailItem.subMaterialID,
                subMaterialColorID: $scope.offerDetailItem.subMaterialColorID,
                seatCushionID: $scope.offerDetailItem.seatCushionID,
                backCushionID: $scope.offerDetailItem.backCushionID,
                cushionColorID: $scope.offerDetailItem.cushionColorID,
                fscTypeID: $scope.offerDetailItem.fscTypeID,
                fscPercentID: $scope.offerDetailItem.fscPercentID,

                packagingMethodID: $scope.offerDetailItem.packagingMethodID,
                clientSpecialPackagingMethodID: $scope.offerDetailItem.clientSpecialPackagingMethodID,
                season: $scope.data.offerSeasonDTO.season,
                offerSeasonDetailID: $scope.offerDetailItem.offerSeasonDetailID
            };

            dataService.getPlanningPurchasingPrice(
                properties,
                function (data) {
                    jQuery('#content').show();
                    $scope.planningPurchasingPriceDTOs = data.data.planningPurchasingPriceDTOs;
                    $scope.factoryPendingPriceDTOs = data.data.factoryPendingPriceDTOs;
                },
                function (error) {
                }
            );            
        }           
    };

    $scope.selectPlaningPurchasingPrice = function (item) {
        if (!item) {
            if ($scope.offerDetailItemCopy.planingPurchasingPriceRemark === null || $scope.offerDetailItemCopy.planingPurchasingPriceRemark === '') {
                bootbox.alert("Please write your explanation as clear as possible");
                return;
            }
            item = {};
            //manually key in
            item.isManuallyKeyIn = true;
            item.planingPurchasingPriceSourceID = 3;
            item.purchasingPrice = $scope.offerDetailItemCopy.estimatedPurchasingPrice;
            item.planingPurchasingPriceRemark = $scope.offerDetailItemCopy.planingPurchasingPriceRemark;
            item.factoryID = $scope.offerDetailItemCopy.estimatedPurchasingPriceFromFactoryID;
            item.factoryUD = $scope.data.factoryDTOs.filter(o => o.factoryID === $scope.offerDetailItemCopy.estimatedPurchasingPriceFromFactoryID)[0].factoryUD;

            //remark file
            item.planingPurchasingPriceFileFriendlyName = $scope.offerDetailItemCopy.planingPurchasingPriceFileFriendlyName;
            item.planingPurchasingPriceFileLocation = $scope.offerDetailItemCopy.planingPurchasingPriceFileLocation;
            item.planingPurchasingPriceFileNewFile = $scope.offerDetailItemCopy.planingPurchasingPriceFileNewFile;
            item.planingPurchasingPriceFileHasChange = $scope.offerDetailItemCopy.planingPurchasingPriceFileHasChange;
        }

        //update purchasing price
        if ($scope.offerDetailItem) {
            if (item.isManuallyKeyIn) {
                $scope.offerDetailItem.isManuallyKeyIn = item.isManuallyKeyIn;
                $scope.offerDetailItem.estimatedPurchasingPrice = item.purchasingPrice;
                $scope.offerDetailItem.estimatedPurchasingPriceFromFactoryID = item.factoryID;
                $scope.offerDetailItem.planingPurchasingPriceRemark = item.planingPurchasingPriceRemark;
            }
            $scope.offerDetailItem.isPlaningPurchasingPriceSelected = true;
            $scope.offerDetailItem.planingPurchasingPriceUpdatorName = 'yourself';
            $scope.offerDetailItem.planingPurchasingPriceSelectedDate = 'just now';
            $scope.offerDetailItem.planingPurchasingPriceFactoryUD = item.factoryUD;
            $scope.offerDetailItem.planingPurchasingPrice = item.purchasingPrice;
            $scope.offerDetailItem.planingPurchasingPriceSourceID = item.planingPurchasingPriceSourceID;
            $scope.offerDetailItem.planingPurchasingPriceFactoryID = item.factoryID;

            //remark file
            $scope.offerDetailItem.planingPurchasingPriceFileFriendlyName = item.planingPurchasingPriceFileFriendlyName;
            $scope.offerDetailItem.planingPurchasingPriceFileLocation = item.planingPurchasingPriceFileLocation;
            $scope.offerDetailItem.planingPurchasingPriceFileNewFile = item.planingPurchasingPriceFileNewFile;
            $scope.offerDetailItem.planingPurchasingPriceFileHasChange = item.planingPurchasingPriceFileHasChange;

            //update purchasing price
            if ($scope.offerDetailItem.planingPurchasingPrice !== null && $scope.offerDetailItem.planingPurchasingPrice !== '') {
                //if ($scope.offerDetailItem.itemStatus === 6) {
                //    $scope.offerDetailItem.itemStatus = 4; //NEED APPROVE
                //}
                //else if ($scope.offerDetailItem.itemStatus === 1 || $scope.offerDetailItem.itemStatus === 2) {
                //    $scope.offerDetailItem.itemStatus = 3;
                //}
                if ($scope.offerDetailItem.itemStatus === 1 || $scope.offerDetailItem.itemStatus === 2) {
                    $scope.offerDetailItem.itemStatus = 3;
                }

                //update to databse
                $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, $scope.offerSeasonDetailID, $scope.offerDetailItem);
            }
        }        
    };

    $scope.selectPendingFactoryPrice = function (pItem) {
        $scope.offerDetailItemCopy.estimatedPurchasingPrice = pItem.salePrice;
        $scope.offerDetailItemCopy.estimatedPurchasingPriceFromFactoryID = pItem.factoryID;   
        $scope.selectPlaningPurchasingPrice(null);
    };


    $scope.changeFile = function () {
        fileUploader2.callback = function () {
            scope.$apply(function () {
                $scope.offerDetailItemCopy.planingPurchasingPriceFileFriendlyName = fileUploader2.selectedFriendlyName;
                $scope.offerDetailItemCopy.planingPurchasingPriceFileLocation = fileUploader2.selectedFileUrl;
                $scope.offerDetailItemCopy.planingPurchasingPriceFileNewFile = fileUploader2.selectedFileName;
                $scope.offerDetailItemCopy.planingPurchasingPriceFileHasChange = true;
            });
        };
        fileUploader2.open();
    };

    $scope.removeFile = function () {
        $scope.offerDetailItemCopy.planingPurchasingPriceFileFriendlyName = '';
        $scope.offerDetailItemCopy.planingPurchasingPriceFileLocation = '';
        $scope.offerDetailItemCopy.planingPurchasingPriceFileNewFile = '';
        $scope.offerDetailItemCopy.planingPurchasingPriceFileHasChange = true;
    };

    $scope.downloadFile = function () {
        if ($scope.offerDetailItemCopy.planingPurchasingPriceFileLocation) {
            window.open($scope.offerDetailItemCopy.planingPurchasingPriceFileLocation);
        }
    };

    //
    //init
    //
    $scope.initController();
}]);
