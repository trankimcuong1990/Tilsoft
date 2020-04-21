//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        supplierID: null,
        bookingID: null,
        season: null,
        type: '1',

        booking: null
    };
    $scope.suppliers = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.suppliers = data.data.suppliers;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.suppliers = null;
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.data.supplierID || $scope.data.supplierID == '' || typeof $scope.data.supplierID == 'undefined') {
                alert('Supplier is invalid !');
                return;
            }

            if ($scope.data.type == '1') {
                if (!$scope.data.bookingID || $scope.data.bookingID == '' || typeof $scope.data.bookingID == 'undefined') {
                    alert('Booking is invalid !');
                    return;
                }
                $scope.data.season = $scope.data.booking.season;                
            }
            else {
                if (!$scope.data.season || $scope.data.season == '' || typeof $scope.data.season == 'undefined') {
                    alert('Season is invalid !');
                    return;
                }
                $scope.data.bookingID = -1;
            }

            window.location = context.nextURL + 'b=' + $scope.data.bookingID + '&s=' + $scope.data.supplierID + '&se=' + $scope.data.season;
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
    $scope.$watch('data.supplierID', function (newValue, oldValue) {
        $scope.data.bookingID = null;        
        $scope.data.booking = null;

        jQuery('#booking-autocomplete').val('');
    });

    $scope.$watch('data.type', function (newValue, oldValue) {
        $scope.data.supplierID = null;
        $scope.data.bookingID = null;
        $scope.data.booking = null;
        $scope.data.season = null;

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
        params: { supplierid: function () { return scope.data.supplierID; } },
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
});