//$(document).keypress(function (e) {
//    if (e.keyCode === 13) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.filterItem();
//        scope.$apply();
//    }    
//});

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngRoute', 'ngCookies', 'customFilters', 'ui']);

tilsoftApp.filter('filterProduct', function () {
    return function (data, productSearchQuery, itemStatusSearchQuery, isEditingSearchQuery, hasQuantity, isClientSelected, isRepeatItem) {
        if (productSearchQuery === '' && itemStatusSearchQuery === null && isEditingSearchQuery === null && hasQuantity === null && isClientSelected === null && isRepeatItem === null) return data;
        var result = [];
        angular.forEach(data, function (item) {
            var isValid = true;

            //product filter
            if (productSearchQuery !== '') {
                productSearchQuery = productSearchQuery.toUpperCase();
                isValid = isValid && (item.articleCode.toUpperCase().indexOf(productSearchQuery) >= 0 || item.description.toUpperCase().indexOf(productSearchQuery) >= 0);
            }

            //status filter
            if (itemStatusSearchQuery !== null) {
                isValid = isValid && item.itemStatus === itemStatusSearchQuery;
            }

            //edit filter
            if (isEditingSearchQuery !== null) {
                isValid = isValid && item.isEditing === isEditingSearchQuery;
            }

            //quantity filter
            if (hasQuantity !== null) {
                if (hasQuantity === true) {
                    isValid = isValid && item.quantity > 0;
                }
                else {
                    isValid = isValid && (item.quantity === 0 || item.quantity === null);
                }                
            }

            //client selected  filter
            if (isClientSelected !== null) {
                if (isClientSelected === true) {
                    isValid = isValid && item.isClientSelected === true;
                }
                else {
                    isValid = isValid && item.isClientSelected === false;
                }                
            }

            //IsRepeatItem
            if (isRepeatItem !== null) {
                if (isRepeatItem === true) {
                    isValid = isValid && item.isRepeatItem === true;
                }
                else {
                    isValid = isValid && item.isRepeatItem === false;
                }
            }

            //result filter
            if (isValid) {
                result.push(item);
            }
        });
        return result;
    };
});

tilsoftApp.filter('sumFunc', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] === null ? 0 : v[key]);
        });
        return sum;
    };
});

tilsoftApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/OfferSeasonMng/_EditFobProductForm', //url of partial view
            controller: '_EditFobProductForm'
        });

    $routeProvider.when('/init-form',
        {
            templateUrl: '/OfferSeasonMng/_InitForm', //url of partial view
            controller: '_InitForm'
        });

    $routeProvider.when('/edit-fob-product-form',
        {
            templateUrl: '/OfferSeasonMng/_EditFobProductForm', //url of partial view
            controller: '_EditFobProductForm'
        });

    $routeProvider.when('/edit-fob-product-form/:itemStatus/:description/:hasQuantity/:isClientSelected',
        {
            templateUrl: '/OfferSeasonMng/_EditFobProductForm', //url of partial view
            controller: '_EditFobProductForm'
        });

    $routeProvider.when('/edit-fob-sparepart-form',
        {
            templateUrl: '/OfferSeasonMng/_EditFobSparepartForm', //url of partial view
            controller: '_EditFobSparepartForm'
        });

    $routeProvider.when('/edit-warehouse-form',
        {
            templateUrl: '/OfferSeasonMng/_EditWarehouseForm', //url of partial view
            controller: '_EditWarehouseForm'
        });

    $routeProvider.when('/search-model-form',
        {
            templateUrl: '/OfferSeasonMng/_SearchModelForm', //url of partial view
            controller: '_SearchModelForm'
        });  

    $routeProvider.when('/search-product-form',
        {
            templateUrl: '/OfferSeasonMng/_SearchProductForm', //url of partial view
            controller: '_SearchProductForm'
        });

    $routeProvider.when('/search-sparepart-form',
        {
            templateUrl: '/OfferSeasonMng/_SearchSparepartForm', //url of partial view
            controller: '_SearchSparepartForm'
        });

    $routeProvider.when('/offer-item-properties-form/:originRouter/:offerSeasonDetailID',
        {
            templateUrl: '/OfferSeasonMng/_OfferItemProperiesForm', //url of partial view
            controller: '_OfferItemProperiesForm'
        });

    $routeProvider.when('/offer-sparepart-properties-form/:offerSeasonDetailID',
        {
            templateUrl: '/OfferSeasonMng/_OfferSparepartProperiesForm', //url of partial view
            controller: '_OfferSparepartProperiesForm'
        });

    $routeProvider.when('/planning-purchasing-price-form/:originRouter/:offerSeasonDetailID',
        {
            templateUrl: '/OfferSeasonMng/_PlanningPurchasingPriceForm', //url of partial view
            controller: '_PlanningPurchasingPriceForm'
        });

    $routeProvider.when('/edit-fob-sample-form',
        {
            templateUrl: '/OfferSeasonMng/_EditFobSampleForm', //url of partial view
            controller: '_EditFobSampleForm'
        });

    $routeProvider.when('/edit-fob-sample-form/:itemStatus/:description/:hasQuantity/:isClientSelected',
        {
            templateUrl: '/OfferSeasonMng/_EditFobSampleForm', //url of partial view
            controller: '_EditFobSampleForm'
        });
});

function formatAutcompleteClient(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.clientUD,
            id: item.clientID,
            label: item.clientNM,
            value: item.clientNM,

            clientID: item.clientID,
            clientUD: item.clientUD,
            clientNM: item.clientNM,
            paymentTermNM: item.paymentTermNM,
            paymentTypeID: item.paymentTypeID,
            inDays: item.inDays,
            paymentMethod: item.paymentMethod,
            downValue: item.downValue,
            deliveryTermNM: item.deliveryTermNM,
            clientEstimatedAdditionalCostDTOs: item.clientEstimatedAdditionalCostDTOs
        };
    });
}

tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    //property
    //
    $scope.data = null;
    $scope.filter = {
        productSearchQuery: '',
        itemStatusSearchQuery: null,
        isEditingSearchQuery: null,
        hasQuantity: null,
        isClientSelected: true,
        isRepeatItem: null
    };
    $scope.localFilter = {
        productSearchQuery: '',
        itemStatusSearchQuery: null,
        isEditingSearchQuery: null,
        hasQuantity: null,
        isClientSelected: true,
        isRepeatItem: null
    };
    $scope.pagePosition = {
        gridProductTop: 0,
        gridProductLeft: 0,
        pageYOffset
    };
    $scope.sortHandler = {
        orderByColumn: ''
    };


    //data structure:
    //data = {
    //    offerSeasonDTO,
    //    offerSeasonDetailDTOs,
    //    offerSeasonDetailPriceOptionDTOs,
    //    seasons,
    //    exRateDTOs,
    //    vat,
    //    clientEstimatedAdditionalCostDTOs,
    //    planingPurchasingPriceSourceDTOs, 
    //    offerSeasonItemStatusDTOs,
    //    productElementDTOs,
    //    factoryDTOs
    //}

    //
    //load data
    //
    $scope.load = function (finishedLoadCallback) {
        var param = {
            offerSeasonTypeID: context.typeID
        };
        dataService.load(
            context.id,
            param,
            function (data) {
                $scope.data = data.data;
                console.log($scope.data);
                $scope.data.filterBoxYesNo = [{ id: true, name: 'Yes' }, { id: false, name: 'No' }];  
                $scope.data.filterBoxYesNoEditing = [{ id: true, name: 'Yes' }, { id: false, name: 'No' }];
                if (finishedLoadCallback !== null) finishedLoadCallback();
                //
                //show content
                //
                jQuery('#content').show();
            },
            function (error) {
                $scope.data = null;
            }
        );
    };

    //
    //update offer season
    //
    $scope.updateOfferSeason = function () {
        dataService.update(
            context.id,
            $scope.data.offerSeasonDTO,
            function (data) {
                if (data.message.type !== 0) {
                    //show error
                    jsHelper.processMessage(data.message);
                }
                if (context.id === 0) {
                    //refresh page
                    var id = data.data.offerSeasonID;
                    window.location = context.refreshUrl + id + "#/" + $scope.getRouter($scope.data.offerSeasonDTO.offerSeasonTypeID);
                }
                else {
                    //refresh data
                    $scope.data.offerSeasonDTO = data.data;
                }               
            },
            function (error) {
                //do need do nothing
            }
        );
    };

    //
    //update offer season detail
    //
    $scope.updateOfferSeasonDetail = function (offerSeasonID, offerSeasonDetailID, dtoOfferSeasonDetail) {    
        if (!offerSeasonID > 0) {
            bootbox.alert("Please save general info of offer before save items !!!");
            return;
        }
        //update offer detail
        dataService.updateOfferSeasonDetail(
            offerSeasonID,
            offerSeasonDetailID,
            dtoOfferSeasonDetail,
            function (data) {
                if (data.message.type !== 0) {
                    //show error
                    jsHelper.processMessage(data.message);
                }
                //refresh dto detail
                jsHelper.autoMapper(data.data, dtoOfferSeasonDetail);                    
            },
            function (error) {
                //do need do nothing
            }
        );
    };

    //
    //update multi offer season detail
    //
    $scope.updateOfferSeasonDetail2 = function () {
        var offerSeasonID = $scope.data.offerSeasonDTO.offerSeasonID;
        var dtoOfferSeasonDetails = [];
        if (!offerSeasonID > 0) {
            bootbox.alert("Please save general info of offer before save items !!!");
            return;
        }
        //get edit item to update
        var offerSeasonDetailDTOs = $scope.data.offerSeasonDetailDTOs.filter(o => o.isEditing);
        angular.forEach(offerSeasonDetailDTOs, function (item) {
            item.rowID = item.offerSeasonDetailID;
            dtoOfferSeasonDetails.push(item);
        });

        //update offer detail
        dataService.updateOfferSeasonDetail2(
            offerSeasonID,
            dtoOfferSeasonDetails,
            function (data) {                
                if (data.message.type !== 0) {
                    //show error
                    jsHelper.processMessage(data.message);
                }
                //update back to frontend
                angular.forEach(offerSeasonDetailDTOs, function (item) {
                    var returnItem = data.data.filter(o => o.rowID === item.rowID);
                    if (returnItem.length > 0) {
                        //refresh dto detail
                        jsHelper.autoMapper(returnItem[0], item);
                    }
                });                
            },
            function (error) {
                //do need do nothing
            }
        );
    };

    //
    //delete offer season detail item
    //
    $scope.deleteOfferSeasonDetail = function (offerSeasonDetailItem) {
        if (offerSeasonDetailItem.offerSeasonDetailID > 0) {
            bootbox.confirm("Are you sure you want to delete this item? All price in quotation request will be deleted !!!", function (result) {
                if (result) {
                    dataService.deleteOfferSeasonDetail(offerSeasonDetailItem.offerSeasonDetailID,
                        function (data) {
                            var index = $scope.data.offerSeasonDetailDTOs.indexOf(offerSeasonDetailItem);
                            $scope.data.offerSeasonDetailDTOs.splice(index, 1);                            
                        },
                        function (error) {
                            bootbox.alert('Item has been put in OTC. You can not delete !!!');
                        });
                }                
            });                       
        }
        else {
            var index = $scope.data.offerSeasonDetailDTOs.indexOf(offerSeasonDetailItem);
            $scope.data.offerSeasonDetailDTOs.splice(index, 1);
        }              
    };

    //
    //autocomplete textbox
    //
    $scope.clientBox = {
        request: {
            url: context.searchClientUrl,
            token: context.token
        },
        data: {
            description: null,
            id: null,
            label: null,
            value: null,

            clientID: null,
            clientUD: null,
            clientNM: null,
            paymentTermNM: null,
            paymentTypeID: null,
            inDays: null,
            paymentMethod: null,
            downValue: null,
            deliveryTermNM: null,
            clientEstimatedAdditionalCostDTOs: null
        },
        selectedItem: function (data) {
            var item = $scope.data.offerSeasonDTO;
            if (data !== null) {
                item.clientID = data.clientID;
                item.clientUD = data.clientUD;
                item.clientNM = data.clientNM;
                item.paymentTermNM = data.paymentTermNM;
                item.paymentTypeID = data.paymentTypeID;
                item.inDays = data.inDays;
                item.paymentMethod = data.paymentMethod;
                item.downValue = data.downValue;
                item.deliveryTermNM = data.deliveryTermNM;
                $scope.data.clientEstimatedAdditionalCostDTOs = data.clientEstimatedAdditionalCostDTOs;
            }
            $scope.$apply();            
        }
    };

    //
    //some function for fob-product-form, fob-sparepart-form, warehouse-form
    //
    $scope.getExRate = function () {
        if ($scope.data === null || $scope.data.masterSettingDTO === null || $scope.data.offerSeasonDTO === null)
            return null;

        var settingItem = $scope.data.masterSettingDTO;
        var exRate = settingItem.exchangeRate;
        return exRate;
    };

    $scope.getRouter = function (offerSeasonTypeID) {
        var router = '';
        switch (offerSeasonTypeID) {
            case 1:
                router = 'edit-warehouse-form';
                break;
            case 2: case 3:
                router = 'edit-fob-product-form';
                break;
            case 4: case 5:
                router = 'edit-fob-sparepart-form';
                break;
            case 6: case 7:
                router = 'edit-fob-sample-form';
                break;
        }
        return router;
    };

    $scope.countItemEditing = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var countItem = $scope.data.offerSeasonDetailDTOs.filter(o => o.isEditing).length;
        return countItem;
    };

    $scope.filterItem = function () {
        $scope.filter.productSearchQuery = $scope.localFilter.productSearchQuery;
        $scope.filter.itemStatusSearchQuery = $scope.localFilter.itemStatusSearchQuery;
        $scope.filter.isClientSelected = $scope.localFilter.isClientSelected;
        $scope.filter.hasQuantity = $scope.localFilter.hasQuantity;
        $scope.filter.isEditingSearchQuery = $scope.localFilter.isEditingSearchQuery;   
        $scope.filter.isRepeatItem = $scope.localFilter.isRepeatItem;   
    };

    $scope.clearFilter = function () {
        $scope.localFilter.productSearchQuery = '';
        $scope.localFilter.itemStatusSearchQuery = null;
        $scope.localFilter.isClientSelected = null;
        $scope.localFilter.hasQuantity = null;
        $scope.localFilter.isEditingSearchQuery = null;
        $scope.localFilter.isRepeatItem = null;
        //filter again
        $scope.filterItem();
    };

    $scope.getRouterWithParam = function (baseRouter) {
        var itemStatus = $scope.filter.itemStatusSearchQuery === null ? -1 : $scope.filter.itemStatusSearchQuery;
        var description = $scope.filter.productSearchQuery === '' ? 'allofferitem' : $scope.filter.productSearchQuery;
        var hasQuantity = $scope.filter.hasQuantity === null ? -1 : $scope.filter.hasQuantity;
        var isClientSelected = $scope.filter.isClientSelected === null ? -1 : $scope.filter.isClientSelected;
        router = "#/" + baseRouter + "/" + itemStatus + "/" + description + "/" + hasQuantity + "/" + isClientSelected;
        return router;
    };
    
    $scope.goToCurrentScrollPosition = function () {
        //go to grid product position
        var offerItemGrid = angular.element('#grdOfferSeasonDetail');
        offerItemGrid.scrollTop($scope.pagePosition.gridProductTop);
        offerItemGrid.scrollLeft($scope.pagePosition.gridProductLeft);
        //go to window position
        window.scrollTo(0, $scope.pagePosition.pageYOffset);
    };

    //
    //init controller
    // 
    //$scope.load();

}]);
