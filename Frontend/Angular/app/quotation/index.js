//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.grid', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.cellNav']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.filters = {
        QuotationUD: '',
        FactoryOrderUD: '',
        FactoryUD: '',
        Season: '',
        ProformaInvoiceNo: '',
        ClientUD: '',
        ArticleCode: '',
        Description: ''
    };
    $scope.seasons = null;
    $scope.factories = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // grid config
    //
    $scope.grdSearchResult = {
        enableSorting: false,
        enableColumnResizing: true,
        columnDefs: [
            { name: 'Action', width: 120, enableHiding: false, enableSorting: false, enableColumnMenu: false, cellClass: 'cell-align-center', cellTemplate: '<a target="_blank" class="btn btn-primary btn-xs font-11" href="' + context.editUrl + '/{{row.entity.quotationID}}?f=0&s=&o=[]" title="Edit"><i class="fa fa-pencil"></i> Edit</a> <a class="btn btn-danger btn-xs font-11" href="javascript:void(0)" title="Delete" ng-click="grid.appScope.event.delete(row.entity.quotationID)"><i class="fa fa-times"></i> Delete</a>' },
            { name: 'Season', field: 'season', width: 100, enableHiding: false },
            { name: 'Quotation', field: 'quotationUD', width: 200, enableHiding: false },
            { name: 'Factory', field: 'factoryUD', width: 100, enableHiding: false },
            { name: 'Delivery Term', field: 'deliveryTermNM', width: 150, enableHiding: false },
            { name: 'Payment Term', field: 'paymentTermNM', width: 150, enableHiding: false },
            { name: 'Updated By', field: 'updatorName', width: 150, enableHiding: false, cellTemplate: '<a ng-show="(row.entity.updatedBy!=null && row.entity.employeeName!=null)" href="' + context.viewUrl + '/{{row.entity.updatedBy}}" da data-featherlight="iframe" data-featherlight-iframe-allowfullscreen="true" data-featherlight-iframe-width="800" data-featherlight-iframe-height="600"><i class="fa fa-user"></i> {{row.entity.employeeName}}</a><span ng-show="(row.entity.updatedBy!=null && row.entity.employeeName==null && row.entity.updatorName!=null)">{{row.entity.updatorName}}</span>' },
            { name: 'Updated Date', field: 'updatedDate', width: 150, enableHiding: false }
        ],
        data: null
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.grdSearchResult.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                        jQuery('#searchResult').find('span.more-info').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                        jQuery('#searchResult').find('span.more-info').css('display', 'inline-block');
                    }

                    jQuery('#content').show();
                    //searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.grdSearchResult.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );
        },
        delete: function (id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].quotationID == data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                            }

                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        init: function () {
            jsonService.getsearchfilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.factories = data.data.factories;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.factories = data.data.factories;
                    $scope.$apply();
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