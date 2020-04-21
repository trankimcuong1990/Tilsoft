function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', qcReportMngController);
    qcReportMngController.$inject = ['$scope', 'dataService'];

    function qcReportMngController($scope, qcReportMngService) {
        // variable
        $scope.data = [];
        $scope.factories = null;

        // variables for event quick-search with Factory.
        $scope.factory = {
            id: null,
            label: null,
            description: null
        };
        $scope.requestFactory = {
            url: context.supportServiceUrl + 'getFactory2',
            token: context.token,
        };

        $scope.greaterThan = function (prop, val) {
            return function (item) {
                return (item[prop] !== '' && item[prop] > val);
            }
        }
        // event
        $scope.event = {
            get: get,
            selectFactory: selectFactory,
            checkInitInfo: checkInitInfo
        };

        function checkInitInfo($event) {
            if (!$scope.editForm.$valid || $scope.data.factoryID == null) {
                $event.preventDefault();
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
            var check = 0;
            for (var i = 0; i < $scope.quicksearchPI.listFactoryOrder.length; i++) {
                var item = $scope.quicksearchPI.listFactoryOrder[i];
                if (item.isSelected) {
                    check = 1;
                    break;
                }
            }
            if (check === 0) {
                $event.preventDefault();
                jsHelper.showMessage('warning', 'Please check one or much PI.');
            }


            //	List int array
            for (let j = 0; j < $scope.quicksearchPI.listFactoryOrder.length; j++) {
                let jItem = $scope.quicksearchPI.listFactoryOrder[j];
                if (jItem.isSelected) {
                    if ($scope.quicksearchPI.itemFactoryOrderLink !== '') {
                        $scope.quicksearchPI.itemFactoryOrderLink += ',';
                    }
                    $scope.quicksearchPI.itemFactoryOrderLink += jItem.factoryOrderDetailID;
                }
            }
        };

        function get() {
            qcReportMngService.serviceUrl = context.serviceUrl;
            qcReportMngService.token = context.token;
            qcReportMngService.supportServiceUrl = context.supportServiceUrl;
            qcReportMngService.getInit(
                function (data) {
                    $scope.factories = data.data.factories;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );

        };

        function selectFactory(factory) {
            $scope.data.factoryID = factory.id;
            $scope.data.factoryUD = factory.label;
            $scope.data.factoryName = factory.description;
            $scope.$apply();
        };


        $scope.searchPITimer = null;
        $scope.quicksearchPI = {
            data: null,
            listFactoryOrder: null,
            itemFactoryOrderLink: '',
            filters: {
                filters: {
                    searchQuery: '',
                    factoryID: 1,
                },
            },

            method: {
                searchPI: function () {
                    qcReportMngService.searchFilterPI(
                        $scope.quicksearchPI.filters.filters.searchQuery,
                        $scope.data.factoryID,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quicksearchPI.data = data.data.data;
                                $scope.$apply();

                                jQuery('#model-popup').show();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            alert('error');
                        }
                    );
                },
            },
            event: {
                searchBoxKeyUp: function ($event) {
                    if ($scope.data.factoryID !== undefined) {
                        if ($event.keyCode === 13) {
                            $scope.quicksearchPI.filters.filters.searchQuery = jQuery('#txtPI').val();
                            $scope.quicksearchPI.filters.pageIndex = 1;
                            $scope.quicksearchPI.method.searchPI();
                        }
                        else if (jQuery('#txtPI').val().length >= 3) {
                            clearTimeout($scope.searchPITimer);
                            $scope.searchPITimer = setTimeout(
                                function () {
                                    $scope.quicksearchPI.filters.filters.searchQuery = jQuery('#txtPI').val();
                                    // $scope.quicksearchPI.filters.pageIndex = 1;
                                    $scope.quicksearchPI.method.searchPI();
                                },
                                500);
                        }
                    }
                    else {
                        $event.preventDefault();
                        jsHelper.showMessage('warning', 'Invalid input factory, please check');
                    }
                },
                clickSearch: function () {
                    $scope.quicksearchPI.filters.filters.searchQuery = jQuery('#txtPI').val();
                    //$scope.quicksearchPI.filters.pageIndex = 1;
                    $scope.quicksearchPI.method.searchPI();
                },

                close: function ($event, clearSearchBox) {
                    jQuery('#model-popup').hide();
                    if (clearSearchBox) {
                        $scope.quicksearchPI.modelNM = null;
                    }
                    $scope.quicksearchPI.data = null;
                    $event.preventDefault();
                },

                selectPI: function ($event, id, code) {

                    for (var i = 0; i < $scope.quicksearchPI.data.length; i++) {
                        debugger
                        if ($scope.quicksearchPI.data[i].factoryID === id && $scope.quicksearchPI.data[i].articleCode === code) {
                            $scope.articleCode = $scope.quicksearchPI.data[i].articleCode;
                            $scope.description = $scope.quicksearchPI.data[i].description;
                            $scope.saleOrderDetailID = $scope.quicksearchPI.data[i].saleOrderDetailID;
                            $scope.customer = $scope.quicksearchPI.data[i].clientUD;
                            $scope.customerID = $scope.quicksearchPI.data[i].clientID;
                            $scope.qty = $scope.quicksearchPI.data[i].quantity;
                            $scope.factoryID = $scope.quicksearchPI.data[i].factoryID;
                            $scope.quicksearchPI.listFactoryOrder = [];
                            qcReportMngService.getListPIFromFactoryOrderDTO(
                                $scope.quicksearchPI.data[i].articleCode,
                                $scope.quicksearchPI.data[i].clientID,
                                $scope.quicksearchPI.data[i].factoryID,
                                function (data) {
                                    if (data.message.type === 0) {
                                        $scope.quicksearchPI.listFactoryOrder = data.data;
                                        debugger;
                                        for (var j = 0; j < $scope.quicksearchPI.listFactoryOrder.length; j++) {
                                            var item = $scope.quicksearchPI.listFactoryOrder[j];
                                            if (item.saleOrderDetailID === $scope.saleOrderDetailID && item.factoryID === $scope.factoryID && item.clientID === $scope.customerID) {
                                                item.isSelected = true;
                                            }
                                        }
                                    }
                                    $scope.$apply();
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                    alert('error');
                                }
                            );
                            break;
                        }
                    }

                    $scope.quicksearchPI.event.close($event, true);
                },

                changeSelect: function (item) {
                    if (item.isSelected) {
                        $scope.qty += item.orderQnt;
                    }
                    else {
                        $scope.qty -= item.orderQnt;
                    }
                }
            }
        }

        // default event
        $scope.event.get();
    }
})();