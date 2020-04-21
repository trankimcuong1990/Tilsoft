//
// APP START
//
function formatData(data) {
    //console.log(data.data);
    return $.map(data.data, function (item) {
        //console.log(item);
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.factories = null;
    $scope.sampleProductStatuses = null;
    $scope.sampleOrderStatuses = null;
    $scope.selection = {
        season: null,
        clientId: '',
        vnFactoryId: '',
        nlFactoryId: null,
        sampleProductStatusID: '',
        sampleOrderStatusID: '',
        sampleOrderID: '',
        sampleOrderUD: '',
        clientUD: ''
    };

    // autocomplete
    $scope.ngInitParam = null;
    $scope.ngItemValue = {
        id: null,
        label: null,
        description: null
    };
    $scope.ngRequestParam = {
        url: jsonService.supportUrl + 'getClient2',
        token: jsonService.token,
    };
    $scope.ngItemSelected = {
        id: null,
        label: null,
        description: null
    };

    $scope.selectedClient = {
        id: null,
        data: null
    }
    $scope.selectedClients = [];

    //
    // event
    //
    $scope.event = {
        init: function () {
            jQuery('#txtxSearchClient').val('');
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.factories = data.data.factories;
                    $scope.sampleProductStatuses = data.data.sampleProductStatuses;
                    $scope.sampleOrderStatuses = data.data.sampleOrderStatuses;
                    $scope.selection.season = jsHelper.getCurrentSeason();

                    $scope.$apply();

                    jQuery('#content').show();

                    jQuery('#sampleOrderUD').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + jsonService.token
                                },
                                type: 'POST',
                                data: JSON.stringify({
                                    filters: {
                                        sampleOrderUD: request.term,
                                        season: $scope.selection.season,
                                        vnFactoryId: $scope.selection.vnFactoryId,
                                        sampleProductStatusID: $scope.selection.sampleProductStatusID,
                                        sampleOrderStatusID: $scope.selection.sampleOrderStatusID,
                                        clientID: $scope.selectedClient.id
                                    }
                                }),
                                dataType: 'json',
                                url: jsonService.serviceUrl + 'getsampleorder',
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
                            $scope.selection.sampleOrderID = ui.item.id;
                            $scope.$apply();
                        }
                    });
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.factories = null;
                    $scope.sampleProductStatuses = null;
                    $scope.sampleOrderStatuses = null;
                    $scope.$apply();
                }
            );
        },
        searchClient: function () {
            $scope.selectedClient.id = null;
            $scope.selectedClient.data = null;
            jQuery('#txtSearchClient').val('');
            jQuery('#frmSearchClient').modal('show');
        },
        selectClient: function () {
            $scope.selectedClient.id = $scope.ngItemValue.id;
            
            if (!$scope.selectedClient.id) {
                alert('Please select client!');
                return;
            }
            index = jsHelper.getArrayIndex($scope.selectedClients, 'clientID', $scope.selectedClient.id);
            if (index < 0) {
                $scope.selectedClients.push({ clientID: $scope.selectedClient.id, clientUD: $scope.ngItemValue.description });
            }
            jQuery('#frmSearchClient').modal('hide');
        },
        removeClient: function (item) {
            $scope.selectedClients.splice($scope.selectedClients.indexOf(item), 1);
        },
        generateExcel: function () {
            if (!$scope.selection.season) {
                alert('Please select season!');
                return;
            }
            $scope.selection.clientId = '';
            angular.forEach($scope.selectedClients, function (value, key) {
                if (this.clientId) {
                    this.clientId += ',' + value.clientID;
                }
                else {
                    this.clientId += value.clientID;
                }                
            }, $scope.selection);

            if ($scope.selection.vnFactoryId === '') {
                $scope.selection.vnFactoryId = null;
            }

            if ($scope.selection.sampleProductStatusID === '') {
                $scope.selection.sampleProductStatusID = null;
            }
            
            if ($scope.selection.sampleOrderID === '') {
                $scope.selection.sampleOrderID = null;
            }

            jsonService.getExcelReport(
                $scope.selection.season,
                $scope.selection.clientId,
                $scope.selection.vnFactoryId,
                $scope.selection.nlFactoryId,
                $scope.selection.sampleProductStatusID,
                $scope.selection.sampleOrderStatusID,
                $scope.selection.sampleOrderID,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        excelVersion2: function () {
            if (!$scope.selection.season) {
                alert('Please select season!');
                return;
            }
            $scope.selection.clientId = '';
            angular.forEach($scope.selectedClients, function (value, key) {
                if (this.clientId) {
                    this.clientId += ',' + value.clientID;
                }
                else {
                    this.clientId += value.clientID;
                }
            }, $scope.selection);

            if ($scope.selection.vnFactoryId === '') {
                $scope.selection.vnFactoryId = null;
            }

            if ($scope.selection.sampleProductStatusID === '') {
                $scope.selection.sampleProductStatusID = null;
            }

            if ($scope.selection.sampleOrderID === '') {
                $scope.selection.sampleOrderID = null;
            }

            jsonService.getExcelSampleProcess(
                $scope.selection.season,
                $scope.selection.clientId,
                $scope.selection.vnFactoryId,
                $scope.selection.nlFactoryId,
                $scope.selection.sampleProductStatusID,
                $scope.selection.sampleOrderStatusID,
                $scope.selection.sampleOrderID,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // method
    //

    //
    // init
    //
    $scope.event.init();
}]);