//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        offerLineId: null,
        offerLineSparepartId: null
    };
    $scope.factories = null;
    $scope.items = null;
    $scope.filters = {
        factoryID: null
    };

    //
    // event
    //
    $scope.event = {
        init : function(){
            jsonService.getInitData(
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.items = data.data.items;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.factories = null;
                    $scope.items = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function (bookingID, factoryID, offerLineID, offerLineSparepartID) {
            console.log(bookingID);
            console.log(factoryID);
            console.log(offerLineID);
            console.log(offerLineSparepartID);

            if (!bookingID || bookingID == '' || typeof bookingID == 'undefined') {
                alert('Booking is invalid !');
                return;
            }
            if (!factoryID || factoryID == '' || typeof factoryID == 'undefined') {
                alert('Factory is invalid !');
                return;
            }

            if (!offerLineID || offerLineID == '' || typeof offerLineID == 'undefined') {
                offerLineID = -1;
            }
            if (!offerLineSparepartID || offerLineSparepartID == '' || typeof offerLineSparepartID == 'undefined') {
                offerLineSparepartID = -1;
            }
            if (offerLineID <= 0 && offerLineSparepartID <= 0) {
                alert('Item is invalid !');
                return;
            }            

            window.location = context.nextURL + 'b=' + bookingID + '&f=' + factoryID + '&o=' + offerLineID + '&os=' + offerLineSparepartID;
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
}]);