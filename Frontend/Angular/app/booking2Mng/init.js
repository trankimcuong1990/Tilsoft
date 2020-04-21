//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        season: jsHelper.getCurrentSeason(),
        supplierID: null,
        clientID: null,

        client: null
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
            if (!$scope.data.clientID || $scope.data.clientID == '' || typeof $scope.data.clientID == 'undefined') {
                alert('Client is invalid !');
                return;
            }
            if (!$scope.data.supplierID || $scope.data.supplierID == '' || typeof $scope.data.supplierID == 'undefined') {
                alert('Supplier is invalid !');
                return;
            }
            if (!$scope.data.season || $scope.data.season == '' || typeof $scope.data.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }

            window.location = context.nextURL + 's=' + $scope.data.supplierID + '&c=' + $scope.data.clientID + '&se=' + $scope.data.season;
        },
        goBack: function () {
            window.location = context.backURL;
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);

//
// JQUERY EXTENSION DECLARATION
//
jQuery(document).ready(function () {
    $('#client-autocomplete').autocomplete({
        source: function (request, response) {
            jsHelper.loadingSwitch(true);
            jQuery.ajax({
                url: jsonService.supportServiceUrl + 'searchclient',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + jsonService.token
                },
                type: 'GET',
                dataType: 'json',
                data: {
                    query: request.term
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    response(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    jsHelper.loadingSwitch(false);
                    console.log(jqXHR);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            event.preventDefault();
            scope.$apply(function(){
                scope.data.clientID = ui.item.clientID;
                scope.data.client = ui.item;
                $('#client-autocomplete').val(ui.item.clientUD);
            });
        },
    })
    .data("ui-autocomplete")._renderItem = function (ul, item) {
	    return jQuery("<li>")
		    .data('item.autocomplete', item)
		    .append('<a href="javascript:void(0)">' + item.clientUD + '</a>')
		    .appendTo(ul);
    };
});