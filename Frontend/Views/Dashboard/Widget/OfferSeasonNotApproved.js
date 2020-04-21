// object name  : offerSeasonNotApproved
tilsoftApp.filter('offerSeasonNotApprovedCustomFilter', function () {
    return function (data, isOfferApproved, isOfferNotApproved, offerSeasonUD, clientNM, saleID, approvedUser) {
        if (isOfferApproved === null && isOfferNotApproved === null && offerSeasonUD === '' && clientNM === '' && saleID === null && approvedUser === '') return data;
        var result = [];
        angular.forEach(data, function (item) {
            var isValid = true;
            if (isOfferApproved !== null) {
                isValid = isValid && item.isOfferApproved === isOfferApproved;
            }
            if (isOfferNotApproved !== null) {
                isValid = isValid && item.isOfferNotApproved === isOfferNotApproved;
            }
            if (offerSeasonUD !== '') {
                offerSeasonUD = offerSeasonUD.toUpperCase();
                isValid = isValid && item.offerSeasonUD.toUpperCase().indexOf(offerSeasonUD) >= 0;
            }
            if (clientNM !== '') {
                clientNM = clientNM.toUpperCase();
                isValid = isValid && item.clientNM.toUpperCase().indexOf(clientNM) >= 0;
            }
            if (saleID !== null) {
                isValid = isValid && item.saleID === saleID;
            }
            if (approvedUser !== '') {
                approvedUser = approvedUser.toUpperCase();
                isValid = isValid && item.approvedUser.toUpperCase().indexOf(approvedUser) >= 0;
            }
            //result filter
            if (isValid) {
                result.push(item);
            }
        });
        return result;
    };
});

angular.module('tilsoftApp').controller('offerSeasonNotApprovedController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', function ($scope, $rootScope, $timeout, $filter, $interval) {
    $scope.offerSeasonNotApproved = {};

    //
    //filter property
    //
    $scope.offerSeasonNotApproved.offerNotApprovedFilter = {
        isOfferApproved: null,
        isOfferNotApproved: null,
        offerSeasonUD: '',
        clientNM: '',
        saleID: null,
        approvedUser: ''
    };

    $scope.offerSeasonNotApproved.offerApprovedFilter = {
        isOfferApproved: null,
        isOfferNotApproved: null,
        offerSeasonUD: '',
        clientNM: '',
        saleID: null,
        approvedUser: ''
    };

    //
    //grid handler
    //
    $scope.offerSeasonNotApproved.offerNotApprovedHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.offerSeasonNotApproved.offerNotApprovedHandler.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false,
        orderByColumn: ''
    };

    $scope.offerSeasonNotApproved.offerApprovedHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.offerSeasonNotApproved.offerApprovedHandler.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false,
        orderByColumn: ''
    };
    

    //
    //property
    //
    $scope.offerSeasonNotApproved.offerNotApprovedData = [];
    $scope.offerSeasonNotApproved.deltaApproved = null;
    $scope.offerSeasonNotApproved.deltaNotApproved = null;

    //
    //event
    //
    $scope.offerSeasonNotApproved.event = {
        init: function () {
            // load data
            $scope.offerSeasonNotApproved.method.loadData(
                offerSeasonNotApprovedContext.currentSeason,
                function (data) { 
                    $scope.offerSeasonNotApproved.offerNotApprovedData = data.data.data;
                    $scope.offerSeasonNotApproved.deltaApproved = data.data.deltaApproved;
                    $scope.offerSeasonNotApproved.deltaNotApproved = data.data.deltaNotApproved;

                    //get list sale                        
                    $scope.offerSeasonNotApproved.sales = [];
                    var saleIDs = [];
                    angular.forEach($scope.offerSeasonNotApproved.offerNotApprovedData, function (item) {
                        if (saleIDs.indexOf(item.saleID) < 0 && item.saleID !== null) {
                            saleIDs.push(item.saleID);
                            $scope.offerSeasonNotApproved.sales.push({
                                saleID: item.saleID,
                                saleNM: item.saleNM
                            });
                        }
                    });

                    $('#widget-offerSeasonNotApproved-loading').hide();
                    $('#widget-offerSeasonNotApproved-container').show();
                    $('#widget-offerSeasonApproved-loading').hide();
                    $('#widget-offerSeasonApproved-container').show();
                    $scope.$apply();
                },
                function (error) {
                    console.log(error);
                    $scope.offerSeasonNotApproved.offerNotApprovedData = [];
                    $scope.$apply();
                }
            );
        }
    };

    

    //
    //method
    //
    $scope.offerSeasonNotApproved.method = {
        loadData: function (season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + offerSeasonNotApprovedContext.token
                },
                type: "POST",
                dataType: 'json',
                url: offerSeasonNotApprovedContext.serviceUrl + 'getoffernotapprovedyet?season=' + season,
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
        //$scope.offerSeasonNotApproved.event.init();
    }, 900000); //15' refesh auto

    //init
    $scope.offerSeasonNotApproved.event.init();
}]);