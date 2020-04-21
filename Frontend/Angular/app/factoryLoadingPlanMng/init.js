//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        factoryID: null,
        bookingID: null,
        parentID: null,

        booking: null,
        loadingPlan: null
    };
    $scope.factories = null;

    $scope.type = 'S';

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.factories = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.data.factoryID || $scope.data.factoryID == '' || typeof $scope.data.factoryID == 'undefined') {
                alert('Factory is invalid !');
                return;
            }

            if ($scope.type == 'S') {
                if (!$scope.data.bookingID || $scope.data.bookingID == '' || typeof $scope.data.bookingID == 'undefined') {
                    alert('Booking is invalid !');
                    return;
                }
                window.location = context.nextURL + 'b=' + $scope.data.bookingID + '&f=' + $scope.data.factoryID + '&p=0';
            }
            else {
                if (!$scope.data.parentID || $scope.data.parentID == '' || typeof $scope.data.parentID == 'undefined') {
                    alert('Parent is invalid !');
                    return;
                }
                window.location = context.nextURL + 'b=0' + '&f=' + $scope.data.factoryID + '&p=' + $scope.data.parentID;
            }
        },
        goBack: function () {
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
    $scope.$watch('data.factoryID', function (newValue, oldValue) {
        $scope.data.bookingID = null;        
        $scope.data.booking = null;

        jQuery('#booking-autocomplete').val('');
    });
}]);

//
// JQUERY EXTENSION DECLARATION
//
jQuery(document).ready(function () {
    jQuery('#booking-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchbooking',
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
        params: { factoryid: function () { return scope.data.factoryID; } },
        onSelect: function (suggestion) {
            scope.data.bookingID = suggestion.data.bookingID;
            scope.data.booking = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.bookingUD, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.data.bookingID = null;
            scope.data.booking = null;
            scope.$apply();
            jQuery('#booking-autocomplete').val('');
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

    jQuery('#parent-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchparent',
        dataType: 'json',
        minChars: 17,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        onSelect: function (suggestion) {
            scope.data.parentID = suggestion.data.loadingPlanID;
            scope.data.loadingPlan = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.loadingPlanUD, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.data.parentID = null;
            scope.data.loadingPlan = null;
            scope.$apply();
            jQuery('#parent-autocomplete').val('');
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