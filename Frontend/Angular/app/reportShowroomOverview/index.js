//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.search();
    }
});


var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        articleCode: '',
        description: '',
        showroomAreaID: null
    };
    $scope.data = null;

    $scope.transferItem = {
        showroomItemID: null,
        fromShowroomAreaID: null,
        toShowroomAreaID: null,
        quantity: null,
    }

    $scope.transferData = [];

    $scope.showroomAreas = null;

    //
    //total function
    //
    $scope.method = {

        calTotalPhysical: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.physicalQnt) == 'null' ? parseInt('0') : parseInt(item.physicalQnt));
            }, total);
            return total;
        },
    }


    //
    // event
    //
    $scope.event = {
        search: function () {
            jsonService.getShowroomOverview($scope.filters,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                    jQuery('#content').show();

                    //assign drag and drop event for every row
                    angular.forEach($scope.data, function (item) {
                        var $tabs = $('#' + item.showroomAreaID);
                        $("tbody.t_sortable").sortable({
                            connectWith: ".t_sortable",
                            items: "> tr:not(:first)",
                            appendTo: $tabs,
                            helper: "clone",
                            zIndex: 999990
                        }).disableSelection();

                        var $tab_items = $($tabs).droppable({
                            accept: ".t_sortable tr",
                            hoverClass: "ui-state-hover",
                            drop: function (event, ui) {

                                showroomItemID = parseInt(ui.draggable[0].cells[0].innerHTML);
                                fromAreaID = parseInt(ui.draggable[0].parentNode.parentNode.id);
                                toAreaID = parseInt(event.target.id);

                                //process transfer
                                if (fromAreaID == toAreaID) return; // from same as to -->do nothing

                                if ($scope.transferData.length > 0) {
                                    j = -1;
                                    for (i = 0; i < $scope.transferData.length; i++) {
                                        var item = $scope.transferData[i];
                                        if (item.showroomItemID == showroomItemID && item.fromShowroomAreaID == toAreaID && item.toShowroomAreaID == fromAreaID) {
                                            j = i;
                                            break;
                                        }
                                    }
                                    if (j >= 0) {
                                        $scope.transferData.splice(j, 1);
                                    }
                                    else {
                                        $scope.transferData.push({
                                            showroomItemID: showroomItemID,
                                            fromShowroomAreaID: fromAreaID,
                                            toShowroomAreaID: toAreaID,
                                            quantity: 1
                                        });
                                    }
                                }
                                else
                                {
                                    $scope.transferData.push({
                                        showroomItemID: showroomItemID,
                                        fromShowroomAreaID: fromAreaID,
                                        toShowroomAreaID: toAreaID,
                                        quantity: 1
                                    });
                                }
                                $scope.$apply();
                            }
                        });
                    });
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

        transferMultiItem: function ($event) {
            $event.preventDefault();           
            if ($scope.transferData == null || $scope.transferData.length == 0)
            {
                jsHelper.showMessage('info','No area was transfer')
                return;
            }
            //transfer area
            jsonService.serviceUrl = context.transferShowroomAreaServiceUrl;
            jsonService.transferMultiItem($scope.transferData,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $scope.transferData = [];
                        $scope.$apply();
                        $scope.event.search();
                    }
                    else if (data.message.type == 2) {
                        //alert('error');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            jsonService.serviceUrl = context.mainServiceUrl;
        },

        getSupportList: function () {

            //input
            filters = {
                filters: {
                    searchQuery : '',
                }
            }

            //search area
            supportService.quicksearchShowroomArea(
                filters,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.showroomAreas = data.data;
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );

        },

        printHangTag: function () {
            var keyIDs = '';
            angular.forEach($scope.data, function (item) {
                angular.forEach(item.showroomOverviews, function (overviewItem) {
                    if (overviewItem.isSelected)
                    {
                        keyIDs += overviewItem.keyID + ',';
                    }                    
                });

            }, keyIDs);

            if (keyIDs.length == 0)
            {
                bootbox.alert("You need select showroom item to print !");
                return;
            }

            jsonService.printHangTag(keyIDs,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.location = context.reportUrl + data.data + '.xlsm';
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });

        },

        printShowroomOverview: function () {
            jsonService.printShowroomOverview(
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.location = context.reportUrl + data.data + '.xlsm';
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });

        }
    }
    $scope.event.getSupportList();
    $scope.event.search();
}]);