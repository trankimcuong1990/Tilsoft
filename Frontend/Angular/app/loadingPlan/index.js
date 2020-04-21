
    
    jQuery('.search-filter').keypress(function(e){
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    //angular
    //    .module('tilsoftApp', ['widgets', 'ngCookies', 'avs-directives'])
    //    .controller('tilsoftController', ['$scope', '$cookieStore', LoadingPlanController]);

var tilsoftApp = angular.module('tilsoftApp', ['widgets', 'ngCookies', 'avs-directives']);
    tilsoftApp.filter('sumFunc', function () {
        return function (data, key) {
            if (angular.isUndefined(data) || angular.isUndefined(key))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (v, k) {
                sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
            });
            return sum;
        }
    });
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', LoadingPlanController]);

function LoadingPlanController($scope, $cookieStore) {

        //#region property
        $scope.data = [];
        $scope.filters = {
            LoadingPlanUD: '',
            ClientUD: '',
            ProformaInvoiceNo: '',
            FactoryUD: '',
            BookingUD: '',
            BLNo: '',
            ContainerNo: '',
            ArticleCode: '',
            Description: '',
            IsUploadingImageFinish: '',
            FromDate: '',
            ToDate:''
        };

        $('input[name="datefilter"]').daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            }
        });

        $('input[name="datefilter"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
            $scope.filters.FromDate = picker.startDate.format('DD/MM/YYYY');
            $scope.filters.ToDate = picker.endDate.format('DD/MM/YYYY');
        });

        $('input[name="datefilter"]').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });

        $scope.supportData = {
            ImageFinishStatuses: [{id: 'false', name: 'PROCESSING'}, {id: 'true', name: 'FINISHED'}]
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        //
        // get filter from cookies
        //
        var cookieValue = $cookieStore.get(context.cookieId);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        //
        // grid handler
        //
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    jsonService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                jsonService.searchFilter.sortedDirection = sortedDirection;
                $scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                jsonService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //#endregion property
        
        //#region event
        $scope.event = {
            search: search,
            delete: deleteItem,
            init: init,
            reload: reload,
            clearFilter: clearFilter
        };
        //#endregion event

        //#region methods
        function reload() {
            $scope.data = [];
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        };

        function search(isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            if (!isLoadMore) {
                $scope.data = [];
                $scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
            }

            $scope.gridHandler.isTriggered = false;
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {                    
                    Array.prototype.push.apply($scope.data, data.data.data);
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = [];
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );

            function injectShowLinks(data) {
                if (data === null || typeof (data) === "undefined") return;

                data.forEach(function (item) {
                    item.showUpdatorLink = item.updatorID !== null && item.updatorName !== null;
                    item.showConfirmerLink = item.confirmerID !== null && item.confirmerName !== null;
                    item.showConfirmerLink = item.confirmerID !== null && item.confirmerName !== null;
                });
            }
        };

        function clearFilter() {
            $scope.filters = {
                LoadingPlanUD: '',
                ClientUD: '',
                ProformaInvoiceNo: '',
                FactoryUD: '',
                BookingUD: '',
                BLNo: '',
                ContainerNo: '',
                ArticleCode: '',
                Description: '',
                FromDate: '',
                ToDate: ''
            };
            $scope.event.reload();
        };

        function deleteItem(id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].loadingPlanID == data.data) {
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
        }

    function init() {
            if (context.piParam !== null && context.piParam !== "" && context.piParam !== undefined) {
                $scope.filters.ProformaInvoiceNo = context.piParam;
            }
            $scope.event.search();
        }
        //#endregion methods

        // #region init
        $scope.event.init();
        // #endregion init
    }