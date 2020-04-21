//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);

tilsoftApp.filter('filterCustom', function () {
    return function (data, season, clientud, clientnm) {
        if ((season === '' || season === null) && clientud === '' && clientnm === '') return data;
        var result = [];
        angular.forEach(data, function (item) {
            var isValid = true;

            //product filter
            if (season !== '') {
                isValid = isValid && (item.season.indexOf(season) >= 0);
            }

            //clientUD filter
            if (clientud !== '') {
                clientud = clientud.toUpperCase();
                isValid = isValid && (item.clientUD.toUpperCase().indexOf(clientud) >= 0);
            }

            //clientNM filter
            if (clientnm !== '') {
                clientnm = clientnm.toUpperCase();
                isValid = isValid && (item.clientNM.toUpperCase().indexOf(clientnm) >= 0);
            }

            //result filter
            if (isValid) {
                result.push(item);
            }
        });
        return result;
    };
});

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.clientCountries = [];
    $scope.clientCities = [];
    $scope.amountByClientAndSeasons = null;
    $scope.amountByClientAndSeasons_Affter = null;
    $scope.seasons = [];

    $scope.filter = {
        season: '',
        clientUD: '',
        clientNM: ''
    };

    //
    // grid handler
    //
    $scope.gridHandler = {

    };
   
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.clientCountries = data.data.clientCountryDTOs;
                    $scope.clientCities = data.data.clientCitiesDTOs;
                    $scope.seasons = data.data.seasons;
                    $scope.amountByClientAndSeasons = data.data.amountByClientAndSeasons;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        console.log(data);
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.data.agentID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },

        sumAmountPI: function () {
            var result = 0;
            angular.forEach($scope.amountByClientAndSeasons_Affter, function (item) {
                result = result + item.amountPI;
            });
            return result;
        },
        sumComAmountPI: function () {
            var result = 0;
            angular.forEach($scope.amountByClientAndSeasons_Affter, function (item) {
                result = result + item.comAmountPI;
            });
            return result;
        },
        sumAmountCI: function () {
            var result = 0;
            angular.forEach($scope.amountByClientAndSeasons_Affter, function (item) {
                result = result + item.amountCI;
            });
            return result;
        },
        sumComAmountCI: function () {
            var result = 0;
            angular.forEach($scope.amountByClientAndSeasons_Affter, function (item) {
                result = result + item.comAmountCI;
            });
            return result;
        }

    };

    //
    // init
    //
    $scope.event.load();
}]);