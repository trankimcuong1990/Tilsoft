//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        season: null,
        factoryId: null,
        factoryOrderID: null,

        factoryOrder: null
    };
    $scope.factories = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init : function(){
            jsonService.getInitData(
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.factories = null;
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {   
            if (!$scope.filters.season || $scope.filters.season == '' || typeof $scope.filters.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }

            if (!$scope.filters.factoryId || $scope.filters.factoryId == '' || typeof $scope.filters.factoryId == 'undefined') {
                alert('Factory is invalid !');
                return;
            }

            if (!$scope.filters.factoryOrderID || $scope.filters.factoryOrderID == '' || typeof $scope.filters.factoryOrderID == 'undefined') {
                alert('Factory order is invalid !');
                return;
            }

            window.location = context.nextURL + '&f=' + $scope.filters.factoryId + '&s=' + $scope.filters.season + '&fo=' + $scope.filters.factoryOrderID;
        },
        goBack: function ($event) {
            $event.preventDefault();

            window.location = context.backURL;
        }
    }

    //
    // init
    //
    $scope.event.init();

    // 
    // watch function
    //
    $scope.$watch('filters.season', function (newValue, oldValue) {
        $scope.filters.factoryOrderID = null;
        $scope.filters.factoryOrder = null;

        jQuery('#factoryorder-autocomplete').val('');
    });

    $scope.$watch('filters.factoryId', function (newValue, oldValue) {
        $scope.filters.factoryOrderID = null;
        $scope.filters.factoryOrder = null;

        jQuery('#factoryorder-autocomplete').val('');
    });
}]);

//
// JQUERY EXTENSION DECLARATION
//
jQuery(document).ready(function () {
    jQuery('#factoryorder-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchfactoryorder',
        dataType: 'json',
        minChars: 3,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        params: { factoryId: function () { return scope.filters.factoryId; }, season: function () { return scope.filters.season; } },
        onSelect: function (suggestion) {
            scope.filters.factoryOrderID = suggestion.data.factoryOrderID;
            scope.filters.factoryOrder = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.itemName, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.filters.factoryOrderID = null;
            scope.filters.factoryOrder = null;
            scope.$apply();
            jQuery('#factoryorder-autocomplete').val('');
        },
        showNoSuggestionNotice: true,
        noCache: true,
        onSearchStart: function (query) {
            jsHelper.loadingSwitch(true);
        },
        onSearchComplete: function (query, suggestions) {
            jsHelper.loadingSwitch(false);
        }
    });
});