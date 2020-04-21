angular.module('tilsoftApp').controller('itemDetalNeedAttentionController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', '$http', function ($scope, $rootScope, $timeout, $filter, $interval, $http) {
    $scope.itemDetalNeedAttention = {};

    //
    // filter property
    //
    $scope.itemDetalNeedAttention.data = [];
    $scope.itemDetalNeedAttention.searchParam = {
        pageIndex: 1,
        totalRows: 0,
        totalPage: 0
    };
    $scope.itemDetalNeedAttention.filters = {
        Season: jsHelper.getCurrentSeason(),
        Description: '',
        ClientNM: '',
        OfferUD: '',
        ProformaInvoiceNo: '',
        PIStatus: null,
        OrderQnt: null,
        OrderQnt40HC: null,
        Delta5Percent: null,
        pageIndex: $scope.itemDetalNeedAttention.pageIndex,
        orderBy: 'Delta5Percent',
        orderDirection: 'DESC'
    };
    $scope.itemDetalNeedAttention.PIStatusFilters = [
        { value: -1, text: 'NOT AVAILABLE' },
        { value: 0, text: 'PENDING' },
        { value: 1, text: 'CONFIRMED' }
    ];
    $scope.itemDetalNeedAttention.OrderQntFilters = [
        { value: 1, text: '>= 50' },
        { value: 2, text: '>= 100' },
        { value: 3, text: '>= 150' }
    ];
    $scope.itemDetalNeedAttention.OrderQnt40HCFilters = [
        { value: 1, text: '>= 0,9' }
    ];
    $scope.itemDetalNeedAttention.Delta5PercentFilters = [
        { value: 0, text: '< 0.0%' },
        { value: 1, text: '> 0.0% AND <= 10.0%' },
        { value: 2, text: '> 10% AND <= 15%' }
    ];

    //
    // grid handler
    //
    $scope.itemDetalNeedAttention.gridHandler = {
        loadMore: function () {
            $scope.itemDetalNeedAttention.event.search();
            console.log('load more');
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.itemDetalNeedAttention.filters.orderBy = sortedBy;
            $scope.itemDetalNeedAttention.filters.orderDirection = sortedDirection;
            //console.log($scope.itemDetalNeedAttention.filters);
            $scope.itemDetalNeedAttention.event.reload();
        },
        isTriggered: false
    };    

    //
    //event
    //
    $scope.itemDetalNeedAttention.event = {
        init: function () {
            $scope.itemDetalNeedAttention.event.reload();
        },
        reload: function () {
            $scope.itemDetalNeedAttention.gridHandler.isTriggered = false;
            $scope.itemDetalNeedAttention.data = [];
            $scope.itemDetalNeedAttention.searchParam.pageIndex = 1;
            $scope.itemDetalNeedAttention.filters.pageIndex = 1;
            $scope.itemDetalNeedAttention.method.search(
                function (data) {
                    Array.prototype.push.apply($scope.itemDetalNeedAttention.data, data.data.data);
                    $scope.itemDetalNeedAttention.searchParam.totalPage = Math.ceil(data.data.totalRows / 50);
                    $scope.itemDetalNeedAttention.searchParam.totalRows = data.data.totalRows;
                    $scope.itemDetalNeedAttention.gridHandler.isTriggered = true;
                },
                function (error) {
                }
            );
        },
        search: function () {
            console.log($scope.itemDetalNeedAttention.searchParam.pageIndex);
            console.log($scope.itemDetalNeedAttention.searchParam.totalPage);

            if ($scope.itemDetalNeedAttention.searchParam.pageIndex < $scope.itemDetalNeedAttention.searchParam.totalPage) {
                $scope.itemDetalNeedAttention.gridHandler.isTriggered = false;
                $scope.itemDetalNeedAttention.searchParam.pageIndex++;
                $scope.itemDetalNeedAttention.filters.pageIndex = $scope.itemDetalNeedAttention.searchParam.pageIndex;
                $scope.itemDetalNeedAttention.method.search(
                    function (data) {
                        Array.prototype.push.apply($scope.itemDetalNeedAttention.data, data.data.data);
                        $scope.itemDetalNeedAttention.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    }
                );
            }
        },
        triggerReload: function (e) {
            if (e.keyCode === 13) {
                $scope.itemDetalNeedAttention.event.reload();
            }
        }
    };

    //
    //method
    //
    $scope.itemDetalNeedAttention.method = {
        loading: function (isBusy) {
            if (isBusy) {
                $('#widget-itemDetalNeedAttention-container').hide();
                $('#widget-itemDetalNeedAttention-loading').show();
            }
            else {
                $('#widget-itemDetalNeedAttention-container').show();
                $('#widget-itemDetalNeedAttention-loading').hide();

                $timeout(function () {
                    $('#widget-itemDetalNeedAttention-loading').height($('#widget-itemDetalNeedAttention-container')[0].clientHeight + 'px');
                }, 500);                
            }
        },
        search: function (iSuccessCallback, iErrorCallback) {
            $scope.itemDetalNeedAttention.method.loading(true);

            $http({
                method: 'POST',
                //contentType: "application/json",
                url: itemDetalNeedAttentionContext.serviceUrl + 'get-item-delta-need-attention',
                data: JSON.stringify({
                        filters: $scope.itemDetalNeedAttention.filters,
                        sortedBy: null,
                        sortedDirection: null,
                        pageSize: null,
                        pageIndex: null
                    }),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + itemDetalNeedAttentionContext.token
                }
            }).then(function successCallback(response) {
                $scope.itemDetalNeedAttention.method.loading(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                $scope.itemDetalNeedAttention.method.loading(false);
                console.log(response);
                iErrorCallback(response);
            });
        }
    };

    //init
    $scope.itemDetalNeedAttention.event.init();
}]);