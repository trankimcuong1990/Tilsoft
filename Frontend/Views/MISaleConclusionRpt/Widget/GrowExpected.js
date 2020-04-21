// object name  : miSaleConclusionRpt
angular.module('tilsoftApp').filter('sumFunc', function () {
    return function (data, field) {
        if (angular.isUndefined(data) || angular.isUndefined(field))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[field] === null ? 0 : v[field]);
        });
        return sum;
    };
});
angular.module('tilsoftApp').controller('MiSaleConclusionRpt_GrowExpectedController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleConclusionRpt = {};
    //
    // property
    //
    $scope.miSaleConclusionRpt.data = null;
    $scope.miSaleConclusionRpt.param = miSaleConclusionRpt_GrowExpected_Context.param;
    $scope.miSaleConclusionRpt.growData = [];
    $scope.miSaleConclusionRpt.top10ClientByCountry = [];

    $scope.miSaleConclusionRpt.filter = {
        clientCountryUD: '',
        clientCountryNM: ''        
    };

    $scope.miSaleConclusionRpt.sortValue = {
        colName: '-totalAmount'
    };

    //
    // events
    //
    $scope.miSaleConclusionRpt.event = {
        init: function () {
            $scope.miSaleConclusionRpt.method.getData(
                $scope.miSaleConclusionRpt.param.season,
                function (data) {
                    $scope.miSaleConclusionRpt.data = data.data;
                    $scope.$apply();
                    //parse data
                    var growData = [];
                    var clientCountryIDs = [];
                    for (let i = 0; i < $scope.miSaleConclusionRpt.data.length; i++) {
                        let item = $scope.miSaleConclusionRpt.data[i];
                        if (clientCountryIDs.indexOf(item.clientCountryID) < 0) {
                            clientCountryIDs.push(item.clientCountryID);

                            //parse by country
                            var clients = $scope.miSaleConclusionRpt.data.filter(o => o.clientCountryID === item.clientCountryID);
                            var totalAmount = jsHelper.sum(clients, ['totalAmount']);
                            var totalAmountLastYear = jsHelper.sum(clients, ['totalAmountLastYear']);
                            growData.push({
                                clientCountryID: item.clientCountryID,
                                clientCountryNM: item.clientCountryNM,
                                growAmount: totalAmount - totalAmountLastYear,
                                growPercent: (totalAmount - totalAmountLastYear) * 100 / totalAmountLastYear,
                                clients: clients,
                                isNegative: totalAmount - totalAmountLastYear < 0 ? true : false,
                                totalAmount: totalAmount //use for Top 10
                            });
                        }
                    }
                    $scope.miSaleConclusionRpt.growData = growData;

                    //get data use for top 10 client by country
                    var top10ClientByCountry = growData.sort(function (a, b) { return b.totalAmount - a.totalAmount; });
                    var i = 0;
                    angular.forEach(top10ClientByCountry, function (item) {
                        if (i < 5) {
                            var x = angular.copy(item);
                            x.clients = [];

                            //sort client
                            var j = 0;
                            var clients = item.clients.sort(function (a, b) { return b.totalAmount - a.totalAmount; });                            
                            angular.forEach(clients, function (cItem) {
                                if (j < 10) {
                                    x.clients.push(cItem);
                                }
                                j++;
                            });

                            $scope.miSaleConclusionRpt.top10ClientByCountry.push(x);
                        }
                        i++;
                    });                    
                },
                function (error) {
                });
        }
    };


    $scope.miSaleConclusionRpt.method = {
        getData: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + miSaleConclusionRpt_GrowExpected_Context.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleConclusionRpt_GrowExpected_Context.serviceUrl + 'get-expected-by-client?season=' + season + '&saleID=-1',
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
        },

        sortData: function (key, field) {
            var currentSetting = $scope.miSaleConclusionRpt.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.miSaleConclusionRpt.sortValue[key] = '-' + field;
                }
                else {
                    $scope.miSaleConclusionRpt.sortValue[key] = field;
                }
            }
            else {
                $scope.miSaleConclusionRpt.sortValue[key] = field;
            }
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.miSaleConclusionRpt.event.init();
    }, 0);
    
}]);