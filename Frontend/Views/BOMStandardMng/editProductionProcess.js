//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {

    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.opSequences = [];
   
    //
    // event
    //

    $scope.event = {
        load: load,
        update: update,        
    }

    //
    // method
    //
    $scope.method = {
        refresh: refresh,
        assignProductAutoComplete: assignProductAutoComplete,
    };

    //
    // init
    //
    $scope.event.load();

    //
    // implement function for event
    //
    function load() {
        dataService.getProductionProcessData(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    //get support list
                    $scope.opSequences = data.data.opSequences;

                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //assign autocomplete
                    $scope.method.assignProductAutoComplete();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
    }

    function update() {
        if ($scope.editForm.$valid) {
            dataService.updateProductionProcess(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    $scope.method.refresh(data.data);
                },
                function (error) {
                    //do nothing
                }
            );
        }
        else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    }

    //
    // implement function for method 
    //
    function refresh(id) {
        jsHelper.loadingSwitch(true);
        window.location = context.refreshUrl + id;
    }

    function assignProductAutoComplete() {
        $('#product').autocomplete({
            source: function (request, response) {
                $.ajax({
                    cache: false,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + context.token
                    },
                    type: "POST",
                    data: JSON.stringify({
                        filters: {
                            searchQuery: request.term
                        }
                    }),
                    dataType: 'json',
                    url: context.getProductUrl,
                    beforeSend: function () {
                        jsHelper.loadingSwitch(true);
                    },
                    success: function (data) {
                        jsHelper.loadingSwitch(false);
                        response(data.data);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        jsHelper.loadingSwitch(false);
                        errorCallBack(xhr.responseJSON.exceptionMessage);
                    }
                });
            },
            minLength: 3,
            select: function (event, ui) {
                $scope.data.productID = ui.item.productID;
                $scope.data.productArticleCode = ui.item.articleCode;
                $scope.data.modelID = ui.item.modelID;
                $scope.$apply();
            }
        });
    }

}]);