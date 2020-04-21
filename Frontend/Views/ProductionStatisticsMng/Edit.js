(function () {
    'use strict';
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
        $scope.screen = {
            _MAIN: "main form",
            _ADD_ITEM: "add planning production team"
        }
        $scope.currentScreen = $scope.screen._MAIN;

        //grid handle
        $scope.gridHandler = {
            loadMore: function () {
                // Do nothing
            },
            sort: function (sortedBy, sortedDirection) {
                // Do nothing
            },
            isTriggered: false
        };
        $scope.gridHandler_AddItem = {
            loadMore: function () {
                // Do nothing
            },
            sort: function (sortedBy, sortedDirection) {
                // Do nothing
            },
            isTriggered: false
        };

        //
        // event
        //
        $scope.event = {
            init: function () {
                if (context.id === 0) {
                    $scope.initForm.load();
                }
                else {
                    $scope.event.load();
                }
            },

            load: function () {
                var param = {
                    workCenterID: $scope.initForm.workCenterID,
                    producedDate: $scope.initForm.producedDate,
                }
                dataService.load(
                    context.id,
                    param,
                    function (data) {
                        $scope.data = data.data.data;
                       
                        //show to page
                        jQuery('#content').show();
                        $scope.$watch('data', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );
            },

            update: function () {
                //update data
                if ($scope.editForm.$valid) {
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            $scope.method.refresh(data.data.productionStatisticsID);
                        },
                        function (error) {
                            //do need do nothing
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', context.errMsg);
                }
            },

            addItem: function () {
                $scope.addPlanningProductionTeamForm.load($scope.data.workCenterID, $scope.data.producedDate);
            },

            removeItem: function (item) {
                var index = $scope.data.productionStatisticsDetailDTOs.indexOf(item);
                $scope.data.productionStatisticsDetailDTOs.splice(index,1);
            }

            
        }

        //
        // method
        //
        $scope.method = {
            refresh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refreshUrl + id;
            },
        }

        //
        //init form
        //
        $scope.initForm = {
            workCenters: [],
            workCenterID: null,
            producedDate: null,
            load: function () {
                dataService.getInit(
                    function (data) {
                        $scope.initForm.workCenters = data.data.workCenterDTOs;
                        jQuery('#content').show();
                        $('#frmInitForm').modal('show');
                    },
                    function (error) {
                        $scope.initForm.workCenters = [];
                    }
                );
            },
            ok: function () {
                if ($scope.initForm.workCenterID === null) {
                    bootbox.alert('You have to select workcenter');
                    return;
                }
                if ($scope.initForm.month === null) {
                    bootbox.alert('You have to select produce date');
                    return;
                }
                $('#frmInitForm').modal('hide');

                //load edit form
                $scope.event.load();
            },
            cancel: function () {
                window.location = context.indexUrl;
            },
        }

        //
        //add item form
        //
        $scope.addPlanningProductionTeamForm = {
            data: [],
            workCenterID: null,
            producedDate: '',
            load: function (workCenterID, producedDate) {
                $scope.addPlanningProductionTeamForm.workCenterID = workCenterID;
                $scope.addPlanningProductionTeamForm.producedDate = producedDate;
                dataService.getPlanningProductionTeam(
                    workCenterID,
                    producedDate,
                    function (data) {
                        //open screen
                        $scope.currentScreen = $scope.screen._ADD_ITEM;
                        //get data
                        $scope.addPlanningProductionTeamForm.data = [];
                        angular.forEach(data.data, function (item) {
                            //check item is existed in detail & add
                            var x = $scope.data.productionStatisticsDetailDTOs.filter(o => o.planningProductionTeamID === item.planningProductionTeamID);
                            if (x.length === 0) {
                                $scope.addPlanningProductionTeamForm.data.push(item);
                            }
                        });
                    },
                    function (error) {
                        $scope.addPlanningProductionTeamForm.data = [];
                    }
                );
            },
            add: function () {
                var data = $scope.addPlanningProductionTeamForm.data;
                angular.forEach(data, function (item) {
                    if (item.isSelected) {
                        $scope.data.productionStatisticsDetailDTOs.push(item);
                    }
                });
                $scope.currentScreen = $scope.screen._MAIN;
            },
            cancel: function () {
                $scope.currentScreen = $scope.screen._MAIN;
            },
        }

        $timeout(function () {
            $scope.event.init();
        }, 0);


    }]);
})();

