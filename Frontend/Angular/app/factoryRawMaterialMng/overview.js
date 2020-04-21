//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;

    $scope.certificateTypes = [
        { 'typeID': 1, 'typeNM': 'BSCI' },
        { 'typeID': 2, 'typeNM': 'ISO-9001 / ISO-14001' },
        { 'typeID': 3, 'typeNM': 'QMS' },
        { 'typeID': 4, 'typeNM': 'SR' },
        { 'typeID': 5, 'typeNM': 'CTPAT' },
        { 'typeID': 6, 'typeNM': 'FSC' }
    ];
    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getOverview(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    jQuery('#content').show();
                    
                    // Turnover input manually by Factory data
                    var arr = [context.CurrentSeason, context.PrevSeason1, context.PrevSeason2, context.PrevSeason3];

                    // Without turnover data in current Factory -> Create new data
                    if ($scope.data.factoryRawMaterialTurnovers.length == 0) {
                        for (i = 0; i < arr.length; i++) {
                            $scope.data.factoryRawMaterialTurnovers.push({
                                factoryRawMaterialTurnoverID: $scope.method.getNewID(),
                                factoryRawMaterialID: context.id,
                                season: arr[i],
                                amount: 0
                            });
                        }
                    } else {
                        var existingSeason = [];
                        angular.forEach($scope.data.factoryRawMaterialTurnovers, function (item) {
                            existingSeason.push(item.season);
                        })
                        for (i = 0; i < arr.length; i++) {
                            if (jQuery.inArray(arr[i], existingSeason) == -1) {
                                $scope.data.factoryRawMaterialTurnovers.push({
                                    factoryRawMaterialTurnoverID: $scope.method.getNewID(),
                                    factoryRawMaterialID: context.id,
                                    season: arr[i],
                                    amount: 0
                                });
                            }
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        }
    };
    $scope.method = {
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
    };

    //
    // init
    //
    $scope.event.init();
}]);