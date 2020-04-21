//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        offerLineID: null,
        offerLineSparepartID: null,
        factoryID: null,
        bookingID: null,

        booking: null,
        item: null
    };
    $scope.factories = null;

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
            if (!$scope.data.bookingID || $scope.data.bookingID == '' || typeof $scope.data.bookingID == 'undefined') {
                alert('Booking is invalid !');
                return;
            }
            if (!$scope.data.factoryID || $scope.data.factoryID == '' || typeof $scope.data.factoryID == 'undefined') {
                alert('Factory is invalid !');
                return;
            }

            if (!$scope.data.offerLineID || $scope.data.offerLineID == '' || typeof $scope.data.offerLineID == 'undefined') {
                $scope.data.offerLineID = -1;
            }
            if (!$scope.data.offerLineSparepartID || $scope.data.offerLineSparepartID == '' || typeof $scope.data.offerLineSparepartID == 'undefined') {
                $scope.data.offerLineSparepartID = -1;
            }
            if ($scope.data.offerLineID <= 0 && $scope.data.offerLineSparepartID <= 0) {
                alert('Item is invalid !');
                return;
            }

            window.location = context.nextURL + 'b=' + $scope.data.bookingID + '&f=' + $scope.data.factoryID + '&o=' + $scope.data.offerLineID + '&os=' + $scope.data.offerLineSparepartID;
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
        $scope.data.offerLineID = null;
        $scope.data.offerLineSparepartID = null;
        $scope.data.item = null;

        jQuery('#booking-autocomplete').val('');
        jQuery('#item-autocomplete').val('');
    });
    $scope.$watch('data.bookingID', function (newValue, oldValue) {
        $scope.data.offerLineID = null;
        $scope.data.offerLineSparepartID = null;
        $scope.data.item = null;

        jQuery('#item-autocomplete').val('');
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

    jQuery('#item-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchitem',
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
        params: { factoryid: function () { return scope.data.factoryID; }, bookingid: function () { return scope.data.bookingID; } },
        onSelect: function (suggestion) {
            if (suggestion.data.offerLineID > 0) {
                scope.data.offerLineID = suggestion.data.offerLineID;
                scope.data.offerLineSparepartID = null;
            }
            else {
                scope.data.offerLineID = null;
                scope.data.offerLineSparepartID = suggestion.data.offerLineSparepartID;
            }
            scope.data.item = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.description, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.data.offerLineID = null;
            scope.data.offerLineSparepartID = null;
            scope.$apply();
            jQuery('#item-autocomplete').val('');
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