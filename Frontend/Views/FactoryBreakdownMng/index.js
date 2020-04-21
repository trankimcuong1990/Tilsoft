jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.refresh();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', factoryBreakdownMngController);
    factoryBreakdownMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function factoryBreakdownMngController($scope, $cookieStore, factoryBreakdownMngService) {
        $scope.data = [];
        $scope.statistic = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.factoryBreakdown = [];

        $scope.filters = {
            ClientUD: '',
            IsConfirmed: '',
            SampleOrderUD: '',
            SampleProductUD: '',
            ArticleDescription: '',
            UserID: '',
            Season: null,
            FactoryID: '',
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    factoryBreakdownMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                factoryBreakdownMngService.searchFilter.sortedDirection = sortedDirection;
                factoryBreakdownMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                factoryBreakdownMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            exportExcel: exportExcel,
            //remove: remove
            getFilterData: getFilterData,
            importExcel: importExcel,
            getfactoryNM: getfactoryNM
        };

        function getFilterData() {
            factoryBreakdownMngService.getFilterData(
                function (data) {
                    $scope.season = data.data.seasons;
                    $scope.factory = data.data.factories;
                    $scope.event.search();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function init() {
            factoryBreakdownMngService.searchFilter.pageSize = context.pageSize;
            factoryBreakdownMngService.serviceUrl = context.serviceUrl;
            factoryBreakdownMngService.token = context.token;
            //factoryBreakdownMngService.getInit(
            //    function (data) {
            //        $scope.event.search();
            //        jQuery('#content').show();
            //    },
            //    function (error) {
            //        jsHelper.showMessage('warning', error);
            //    }
            //);
            $scope.event.getFilterData();
            $scope.event.search();
        };

        function search(isLoadMore) {
            $scope.filters.UserID = context.userID;
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            factoryBreakdownMngService.searchFilter.filters = $scope.filters;
            factoryBreakdownMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.statistic = data.data.statistic;
                    $scope.totalPage = Math.ceil(data.totalRows / factoryBreakdownMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();                  

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );        
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            factoryBreakdownMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                ClientUD: '',
                IsConfirmed: '',
                SampleOrderUD: '',
                ArticleDescription: '',
                UserID: ''
            };
            $scope.event.refresh();

        };

        function exportExcel() {
            if ($scope.filters.ClientUD === null) {
                $scope.filters.ClientUD = '';
            }
            if ($scope.filters.SampleOrderUD === null) {
                $scope.filters.SampleOrderUD = '';
            }
            if ($scope.filters.ArticleDescription === null) {
                $scope.filters.ArticleDescription = '';
            }
            if ($scope.filters.UserID === null) {
                $scope.filters.UserID = 0;
            }
            if ($scope.filters.IsConfirmed === null) {
                $scope.filters.IsConfirmed = 'any';
            }
            factoryBreakdownMngService.exportexcellistfactorybreakdown(
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };
        function importExcel() {
            var input = document.createElement("input");
            input.setAttribute("type", "file");
            input.setAttribute("accept", ".xlsx");
            input.addEventListener("change", function (e) {
                //get file content
                var file = e.target.files[0];
                //
                var f = e.target.files[0];
                var reader = new FileReader();
                //var name = f.name;
                reader.onload = function (e) {
                    var data = e.target.result;

                    var result;
                    var workbook = XLSX.read(data, { type: 'binary' });

                    var sheet_name_list = workbook.SheetNames;
                    sheet_name_list.forEach(function (y) { /* iterate through sheets */
                        //Convert the cell value to Json
                        var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y], { raw: true });
                        if (roa.length > 0) {
                            result = roa;
                        }
                    });
                    console.log(result);

                    //
                    //Import data
                    //
                    $scope.factoryBreakdown = [];
                    var indexItem = 0;
                    for (var i = 2; i < result.length; i++) {
                        var item = result[i];
                        if (item.status === "" || item.status === null || item.status === undefined) {
                            if (indexItem !== item.factoryBreakdownID) {
                                indexItem = item.factoryBreakdownID;
                                var addItem = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    indicatedPrice: parseFloat(item.indicatedPrice),
                                    packingDimensionL: item.packingDimensionL,
                                    packingDimensionW: item.packingDimensionW,
                                    packingDimensionH: item.packingDimensionH,
                                    cushionDimensionL: item.cushionDimensionL,
                                    cushionDimensionW: item.cushionDimensionW,
                                    cushionDimensionH: item.cushionDimensionH,
                                    remark: item.remark,
                                    factoryBreakdownDetail: []
                                };
                                var addItemDetail = null;
                                //40HC
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 11,
                                    unitNM: item.unit40HC,
                                    quantity: parseFloat(item.qnt40HC)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Foam
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 10,
                                    unitNM: item.unitFoam,
                                    quantity: parseFloat(item.qntFoam)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Fabric
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 9,
                                    unitNM: item.unitFabric,
                                    quantity: parseFloat(item.qntFabric)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Alu Frame
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 1,
                                    unitNM: item.unitAluFrame,
                                    quantity: parseFloat(item.qntAluFrame)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Wood
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 5,
                                    unitNM: item.unitWood,
                                    quantity: parseFloat(item.qntWood)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Rope
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 4,
                                    unitNM: item.unitRope,
                                    quantity: parseFloat(item.qntRope)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Glass
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 6,
                                    unitNM: item.unitGlass,
                                    quantity: parseFloat(item.qntGlass)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Steel
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 2,
                                    unitNM: item.unitSteel,
                                    quantity: parseFloat(item.qntSteel)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Polystone
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 8,
                                    unitNM: item.unitPolystone,
                                    quantity: parseFloat(item.qntPolystone)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Wicker
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 3,
                                    unitNM: item.unitWicker,
                                    quantity: parseFloat(item.qntWicker)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);

                                //Duranite
                                addItemDetail = {
                                    factoryBreakdownID: item.factoryBreakdownID,
                                    factoryBreakdownCategoryID: 7,
                                    unitNM: item.unitDuranite,
                                    quantity: parseFloat(item.qntDuranite)
                                };
                                addItem.factoryBreakdownDetail.push(addItemDetail);


                                $scope.factoryBreakdown.push(addItem);
                            }
                        }
                    }
                    factoryBreakdownMngService.importUpdateData(
                        $scope.factoryBreakdown,
                        function (data) {
                            jsHelper.processMessage(data.message); 
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        });
                };
                reader.readAsArrayBuffer(f);

            }, false);
            input.click();
        };

        function getfactoryNM(id) {
            var FactoryName = "";
            angular.forEach($scope.factory, function (item) {
                if (item.factoryID === id) {
                    FactoryName = item.factoryUD;
                }
            });
            return FactoryName;
        };

        //function remove(item) {
        //    factoryBreakdownMngService.delete(
        //        item.factoryBreakdownID,
        //        null,
        //        function (data) {
        //            jsHelper.processMessage(data.message);

        //            if (data.message.type === 0) {
        //                var index = $scope.data.indexOf(item);

        //                $scope.data.splice(index, 1);
        //                $scope.totalRows = $scope.totalRows - 1;
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //};

        $scope.event.init();
    };
})();
