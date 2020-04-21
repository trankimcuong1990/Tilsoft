// object name  : pendingOfferItemPrice
tilsoftApp.filter('pendingOfferItemPriceCustomFilter', function () {   
    return function (data, clientUD, factoryUD, description, waitingStatusID) {
        
        if (clientUD === '' && factoryUD === '' && description === '' && waitingStatusID === '') return data;
        var result = [];
        angular.forEach(data, function (item) {
            var isValid = true;
         
            if (clientUD !== '') {
                clientUD = clientUD.toUpperCase();              
                isValid = isValid && item.clientUD.toUpperCase().indexOf(clientUD) >= 0;
            }
          
            if (factoryUD !== '') {
                factoryUD = factoryUD.toUpperCase();
                isValid = isValid && item.factoryUD.toUpperCase().indexOf(factoryUD) >= 0;
            }

            if (description !== '') {
                description = description.toUpperCase();
                isValid = isValid && (item.description.toUpperCase().indexOf(description) >= 0 || item.articleCode.toUpperCase().indexOf(description) >= 0);
            }

            if (waitingStatusID !== null) {
                isValid = isValid && item.waitingStatusID === waitingStatusID;
            }
            //result filter
            if (isValid) {
                result.push(item);
            }
        });
        return result;
    };
});

angular.module('tilsoftApp').controller('pendingOfferItemPriceController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', function ($scope, $rootScope, $timeout, $filter, $interval) {
   
    $scope.pendingOfferItemPrice = {};

    //
    //filter property
    //
    $scope.pendingOfferItemPrice.pendingOfferItemPriceFilter = {
        clientUD: '',
        factoryUD: '',       
        description: '',
        waitingStatusID: null
    };

    //
    //grid handler
    //
    $scope.pendingOfferItemPrice.pendingOfferItemPriceHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.pendingOfferItemPrice.offerNotApprovedOrderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false,
        orderByColumn: ''
    };
   
    //
    //property
    //
    $scope.pendingOfferItemPrice.pendingOfferItemPriceData = [];    
    $scope.pendingOfferItemPrice.waitStatuses = [
        { id: 1, name: 'WAIT FOR EUROFAR' },
        { id: 2, name: 'WAIT FOR FACTORY' }
    ];

    //
    //event
    //
    $scope.pendingOfferItemPrice.event = {
        init: function () {
            // load data
           
            $scope.pendingOfferItemPrice.method.loadData(
                pendingOfferItemPriceContext.currentSeason,
                function (data) {
                   
                    $scope.pendingOfferItemPrice.pendingOfferItemPriceData = data.data;                 

                    $('#widget-pendingOfferItemPrice-loading').hide();
                    $('#widget-pendingOfferItemPrice-container').show();
                    $('#widget-offerSeasonApproved-loading').hide();
                    $('#widget-offerSeasonApproved-container').show();
                    $scope.$apply();
                },
                function (error) {
                    
                    console.log(error);
                    $scope.pendingOfferItemPrice.pendingOfferItemPriceData = [];
                    $scope.$apply();
                }
            );
        }
    };



    //
    //method
    //
    $scope.pendingOfferItemPrice.method = {
        loadData: function (season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + pendingOfferItemPriceContext.token
                },
                type: "POST",
                dataType: 'json',
                url: pendingOfferItemPriceContext.serviceUrl + 'get-pending-offer-item-price?season=' + season,
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

    //auto load after 15'
    $interval(function () {
        $scope.pendingOfferItemPrice.event.init();
    }, 900000); //15' refesh auto

    //init
    $scope.pendingOfferItemPrice.event.init();
}]);