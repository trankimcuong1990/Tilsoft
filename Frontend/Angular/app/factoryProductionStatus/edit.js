var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives']);
tilsoftApp.filter('sumFunction', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    }
});
tilsoftApp.filter('orderDetailFilter', function () {
    return function (dataSource, articleCode, description) {
        var items = {
            articleCode: articleCode,
            description: description,
            out: []
        }
        angular.forEach(dataSource, function (value, key) {
            if (this.articleCode != '' || this.description != '') {
                if ((value.articleCode.toUpperCase().indexOf(this.articleCode.toUpperCase()) >= 0) || (value.description.toUpperCase().indexOf(this.description.toUpperCase()) >= 0)) {
                    this.out.push(value);
                }
            }
            else {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
});
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.newID = -100;
    $scope.weekSeasons = [];

    $scope.gridFilter = {
        clientUD: '',
        proformaInvoiceNo: '',
        lds: '',
    }

    $scope.detailFilter = {
        searchQuery : ''
    }

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            //do need do nothing
        },
        sort: function (sortedBy, sortedDirection) {
            datasource = $scope.data.factoryProductionStatusDetailDTOs;
            if (sortedDirection == 'asc') {
                datasource.sort(function (a, b) {
                    return a[sortedBy] > b[sortedBy] ? 1 : -1;
                });
            }
            else if (sortedDirection == 'desc') {
                datasource.sort(function (a, b) {
                    return a[sortedBy] < b[sortedBy] ? 1 : -1;
                });
            }
            $scope.$apply();
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getFactoryProductionStatus(
                context.id, context.factoryID, context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.weekSeasons = data.data.weekSeasons;
                    $scope.$apply();
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    $("#weekNo").select2();
                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }

                    //gridHandler
                    $scope.gridHandler.refresh();
                    $scope.gridHandler.goTop();
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryProductionStatusID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    $scope.data.factoryProductionStatusID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        viewDetail: function ($event, item) {
            $event.preventDefault();
            if ($("#icon-view-pi-" + item.factoryOrderID).hasClass('fa-plus-square-o')) {
                $("#" + item.factoryOrderID).toggle();
                $("#icon-view-pi-" + item.factoryOrderID).removeClass('fa-plus-square-o');
                $("#icon-view-pi-" + item.factoryOrderID).addClass('fa-minus-square-o');
            }
            else if ($("#icon-view-pi-" + item.factoryOrderID).hasClass('fa-minus-square-o')) {
                $("#" + item.factoryOrderID).toggle();
                $("#icon-view-pi-" + item.factoryOrderID).removeClass('fa-minus-square-o');
                $("#icon-view-pi-" + item.factoryOrderID).addClass('fa-plus-square-o');
            }
        },

        getOrderDetail: function ($event, item) {
            $event.preventDefault();
            var excludeFactoryOrderDetailIDs = '';
            angular.forEach(item.factoryProductionStatusOrderDetailDTOs, function (sItem) {
                excludeFactoryOrderDetailIDs += sItem.factoryOrderDetailID + ',';
            });
            jsonService.getOrderDetail(item.factoryOrderID, excludeFactoryOrderDetailIDs,
                function (data) {
                    angular.forEach(data.data, function (sItem) {
                        var x = item.factoryProductionStatusOrderDetailDTOs.filter(function (o) { return o.factoryOrderDetailID == sItem.factoryOrderDetailID });
                        if (x == null || x.length == 0) {
                            item.factoryProductionStatusOrderDetailDTOs.push({
                                factoryProductionStatusOrderDetail: $scope.method.getNewID(),
                                factoryOrderDetailID: sItem.factoryOrderDetailID,
                                articleCode: sItem.articleCode,
                                description: sItem.description,
                                orderQnt: sItem.orderQnt,
                                qnt40HC: sItem.qnt40HC,
                                orderInCont: sItem.orderInCont,
                            });
                        }
                    })
                    $scope.$apply();
                },
                function (error) {
                    $scope.$apply();
                }
            );
        },

        changeProducedQnt: function (sItem,item) {
            sItem.producedCont = sItem.producedQnt / sItem.qnt40HC;
            sItem.outputProducedQnt = sItem.producedQnt;
            //get total
            var total = parseFloat(0);
            angular.forEach(item.factoryProductionStatusOrderDetailDTOs, function (x) {
                total += (x.producedCont == null ? 0 : parseFloat(x.producedCont));
            });
            item.producedContainerQnt = total;

            $scope.event.changeOutputProducedQnt(sItem, item);
        },

        changeOutputProducedQnt: function (sItem, item) {
            //sItem.producedCont = sItem.producedQnt / sItem.qnt40HC;
            //sItem.outputProducedQnt = sItem.producedQnt;
            //get total
            var total = parseInt(0);
            angular.forEach(item.factoryProductionStatusOrderDetailDTOs, function (x) {
                total += (x.outputProducedQnt == null ? 0 : parseFloat(x.outputProducedQnt));
            });
            item.totalOutputProducedThisWeek = total;
        },


    };
    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        //getTotalProducedContContByOrder: function (item) {
        //    var total = parseFloat(0);
        //    angular.forEach(item.factoryProductionStatusOrderDetailDTOs, function (sItem) {
        //        total += (sItem.producedCont == null ? 0 : parseFloat(sItem.producedCont));
        //    });
        //    return total;
        //},
        //getTotalProducedCont: function () {
        //    var total = parseFloat(0);
        //    if ($scope.data != null) {
        //        angular.forEach($scope.data.factoryProductionStatusDetailDTOs, function (item) {
        //            angular.forEach(item.factoryProductionStatusOrderDetailDTOs, function (sItem) {
        //                total += (sItem.producedCont == null ? 0 : parseFloat(sItem.producedCont));
        //            });
        //        });
        //    }
        //    return total;
        //},

    };
    //
    // init
    //
    $scope.event.load();
}]);