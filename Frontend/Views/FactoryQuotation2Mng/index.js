jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']).controller('tilsoftController', factoryQuotation2MngController);
    factoryQuotation2MngController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function factoryQuotation2MngController($scope, $cookieStore, factoryQuotation2MngService, $timeout) {
        $scope.data = [];
        $scope.additional = null;
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.seasons = jsHelper.getSeasons();
        $scope.itemTypes = [
            { id: 1, name: 'F/O PRODUCT' },
            { id: 0, name: 'F/O SPAREPART' },
            { id: 6, name: 'F/O SAMPLE' },
            { id: 5, name: 'OFFER ITEM' },
            { id: 2, name: 'OFFER SAMPLE' },
            { id: 7, name: 'OFFER ITEM (Estimated)' },
            { id: 3, name: 'CATALOGUE ITEM' },
            { id: 4, name: 'REQUESTED ITEM' }
        ];
        $scope.businessDataStatuses = [
            { id: 2, name: 'MISSING PURCHASING PRICE & LOAD ABILITY' },
            { id: 3, name: 'MISSING PURCHASING PRICE' },
            { id: 4, name: 'MISSING LOAD ABILITY' }
        ];
        $scope.waitStatuses = [
            { id: 1, name: 'WAIT FOR EUROFAR' },
            { id: 2, name: 'WAIT FOR FACTORY' }
        ];
        $scope.deadLines = [
            { id: 0, name: 'NO' },
            { id: 1, name: 'YES' }
        ];
        $scope.repeatItems = [
            { id: 0, name: 'NO' },
            { id: 1, name: 'YES' }
        ];
        $scope.isSelectAll= false;
        $scope.historyData = null;
        $scope.isFirstLoadProcessed = false;
        $scope.status= {
            total: null,
            confirmed: null,
            pending: null,
            waitEurofar: null,
            waitFactory: null,

            totalContainer: null,
            totalConfirmedContainer: null,
            totalPendingContainer: null,
            totalContainerWaitForEurofar: null,
            totalContainerWaitForFactory: null,

            isLoaded: false,

            waitForFactoryConlusion: null,
            waitForFactoryConlusionSummary: null,
            waitForFactoryProductionConlusion: null,
            waitForFactoryProductionConlusionSummary: null
        };

        $scope.filters = {
            Season: context.autoSeason ? context.autoSeason : jsHelper.getCurrentSeason(),
            ClientUD: '',
            Description: context.autoArtCode ? context.autoArtCode : '',
            FactoryUD: '',
            ItemTypeID: context.autoType !== null ? context.autoType : null,
            StatusID: context.autoStatus !== null ? context.autoStatus : null,
            QuotationOfferDirectionID: context.autoWaitStatus !== null ? context.autoWaitStatus : null,
            ProformaInvoiceNo: '',
            BusinessDataStatusID: null,
            UserID: context.userID,
            IsDeadLine: context.autoOverdue !== null ? context.autoOverdue : null,
            IsRepeatItem: null
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

                factoryQuotation2MngService.searchFilter.sortedDirection = sortedDirection;
                factoryQuotation2MngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                factoryQuotation2MngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            reload: reload,
            clear: clear,
            loadMore: function () {
                if ($scope.pageIndex.isTriggered) return;
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    factoryQuotation2MngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.isTriggered = true;
                    $scope.event.search();
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                $scope.pageIndex = 1;
                factoryQuotation2MngService.searchFilter.sortedDirection = sortedDirection;
                factoryQuotation2MngService.searchFilter.pageIndex = $scope.pageIndex;
                factoryQuotation2MngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },

            updateTarget: function () {
                var dataToBeUpdated = [];
                angular.forEach($scope.data, function (item) {
                    if (item.newPurchasingPrice || item.quotationStatusNM === 'WAITING REJECT') {
                        dataToBeUpdated.push(item);
                    }
                });

                factoryQuotation2MngService.updateTarget(
                    dataToBeUpdated,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.event.reload();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },            
            exportExcel: function () {   
                factoryQuotation2MngService.searchFilter.filters = $scope.filters;
                factoryQuotation2MngService.exportExcel(
                    factoryQuotation2MngService.searchFilter
                    ,
                    function (data) {
                        window.location = context.serviceReport + data.data;
                    },
                    function (error) {
                    });
            },
            
            openHistory: function (item) {
                $scope.historyData = null;
                factoryQuotation2MngService.getHistory(
                    item.quotationDetailID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.historyData = data.data;
                            $('#frmHistory').modal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            reject: function (quotationDetailID) {
                angular.forEach($scope.data.filter(s => s.quotationDetailID === quotationDetailID), function (item) {
                    item.quotationStatusNM = 'WAITING REJECT';
                });              
            },
            //get additional condition client data
            openAdditional: function (item) {
                debugger;
                $scope.clientAdditionalCondition = [];
                $scope.clientAdditionalConditionData = [];
                factoryQuotation2MngService.getAdditionalCondition(
                item.quotationDetailID,
                function (data) {
                    $scope.clientAdditionalCondition = data.data;    
                    for (var j = 0; j < $scope.clientAdditionalCondition.length; j++) {
                        var item2 = $scope.clientAdditionalCondition[j];
                        if (item.clientID === item2.clientID && item2.isSelected === true && item2.activeFactory == true) {
                            $scope.clientAdditionalConditionData.push({
                                additionalConditionID: item2.additionalConditionID,
                                additionalConditionNM: item2.additionalConditionNM,
                                additionalConditionTypeNM: item2.additionalConditionTypeNM,
                                clientAdditionalConditionID: item2.clientAdditionalConditionID,
                                clientID: item2.clientID,
                                clientUD: item2.clientUD,
                                isSelected: item2.isSelected,
                                remark: item2.remark,
                                remarkClient: item2.remarkClient,
                                attachFile: item2.attachFile,
                                attachFileURL: item2.attachFileURL,
                                attachFileFriendlyName: item2.attachFileFriendlyName,
                                updateName1: item2.updateName1,
                                updateName2: item2.updateName2,
                                updatedCheckBy: item2.updatedCheckBy,
                                updatedCheckDate: item2.updatedCheckDate,
                                updatedUnCheckBy: item2.updatedUnCheckBy,
                                updatedUnCheckDate: item2.updatedUnCheckDate,
                                activeFactory: item2.activeFactory
                            });
                        }
                    }
                        
                    $('#frmAdditionCondition').modal();
                },
                function (error) {
                    jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            seasonShortCut: function (season) {
                return season.substr(2, 3) + season.substr(7, 2);   
            },
            //overdueSelected: function (IsDeadLine) {
            //    if (IsDeadLine === 0 || IsDeadLine === 1) {
            //        $scope.filters.ItemTypeID = 5;
            //    }
            //},
            //typeSelected: function (ItemTypeID) {
            //    if (ItemTypeID !== 5) {
            //        $scope.filters.IsDeadLine = null;
            //    }
            //}
        };

        //
        //import from excel BOM file
        //
        $scope.importFromExcel = {
           
            importExcel: function () {      
                
                var input = document.createElement("input");
                input.setAttribute("type", "file");
                input.setAttribute("accept", ".xlsx");              
                input.addEventListener("change", function (e) {    
                   
                    //get file content
                    var file = e.target.files[0];
                    var f = e.target.files[0];
                    var reader = new FileReader();                 

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
                        $scope.lstImport = [];
                     
                        for (var i = 1; i < result.length; i++) {
                            var item = result[i];
                            console.log(item);                             
                            var addItem = {
                                quotationDetailID: item.QuotationDetailID,                               
                                factoryPrice: item.FactoryPrice,
                                newComment: item.NewComment,
                                season: item.Season
                            };

                            $scope.lstImport.push(addItem);                            
                        }

                        factoryQuotation2MngService.import(
                            $scope.lstImport,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type === 0) {
                                    $scope.event.reload();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error.message.message);
                            }
                        );
                    };
                    reader.readAsArrayBuffer(f);

                }, false);
                input.click();
            },
        };

        function init() {
            factoryQuotation2MngService.searchFilter.pageSize = context.pageSize;
            factoryQuotation2MngService.serviceUrl = context.serviceUrl;
            factoryQuotation2MngService.token = context.token;
            factoryQuotation2MngService.getSearchFilter(
                function (data) {
                    $scope.quotationStatuses = data.data.quotationStatusDTOs;
                    $scope.event.reload();
                },
                function (error) {
                    // reset data
                }
            );
        };

        function search(isLoadMore) {

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            factoryQuotation2MngService.searchFilter.filters = $scope.filters;
            factoryQuotation2MngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    if (data.totalRows >= 0) {
                        $scope.totalPage = Math.ceil(data.totalRows / factoryQuotation2MngService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        //$scope.status.total = data.data.totalItem;
                        //$scope.status.confirmed = data.data.totalConfirmedItem;
                        //$scope.status.waitEurofar = data.data.totalWaitForEurofar;
                        //$scope.status.totalContainer = data.data.totalContainer;
                        //$scope.status.totalConfirmedContainer = data.data.totalConfirmedContainer;
                        //$scope.status.totalContainerWaitForEurofar = data.data.totalContainerWaitForEurofar;

                        $scope.status.total = data.data.totalItem;
                        $scope.status.confirmed = data.data.totalConfirmedItem;
                        $scope.status.pending = data.data.totalPendingItem;
                        $scope.status.waitEurofar = data.data.totalWaitForEurofar;
                        $scope.status.waitFactory = data.data.totalWaitForFactory;
                        $scope.status.totalContainer = data.data.totalContainer;
                        $scope.status.totalConfirmedContainer = data.data.totalConfirmedContainer;
                        $scope.status.totalPendingContainer = data.data.totalPendingContainer;
                        $scope.status.totalContainerWaitForEurofar = data.data.totalContainerWaitForEurofar;
                        $scope.status.totalContainerWaitForFactory = data.data.totalContainerWaitForFactory;
                        $scope.status.isLoaded = true;

                        // wait for factory offer conclusion
                        $scope.status.waitForFactoryConlusion = data.data.waitForFactoryConclusionDTOs;
                        $scope.status.waitForFactoryConlusionSummary = {
                            totalPendingItem: 0,
                            totalItem: 0,
                            overDue1Day: 0,
                            overDue2Day: 0,
                            overDue3Day: 0,
                            overDue4DayOrMore: 0
                        };
                        if (data.data.waitForFactoryConclusionDTOs && data.data.waitForFactoryConclusionDTOs.length > 0) {
                            $scope.status.waitForFactoryConlusionSummary.totalPendingItem = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.totalPendingItem, 0);
                            $scope.status.waitForFactoryConlusionSummary.totalItem = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.totalItem, 0);
                            $scope.status.waitForFactoryConlusionSummary.overDue1Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue1Day, 0);
                            $scope.status.waitForFactoryConlusionSummary.overDue2Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue2Day, 0);
                            $scope.status.waitForFactoryConlusionSummary.overDue3Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue3Day, 0);
                            $scope.status.waitForFactoryConlusionSummary.overDue4DayOrMore = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue4DayOrMore, 0);
                        }         
                        // wait for factory production conclusion
                        $scope.status.waitForFactoryProductionConlusion = data.data.waitForFactoryProductionConclusionDTOs;
                        $scope.status.waitForFactoryProductionConlusionSummary = {
                            totalPendingItem: 0,
                            overLDS: 0,
                            lds: 0,
                            oneToTwoDays: 0,
                            threeToFourDays: 0,
                            fiveToSixDays: 0,
                            moreThanSixDays: 0
                        };
                        if (data.data.waitForFactoryProductionConclusionDTOs && data.data.waitForFactoryProductionConclusionDTOs.length > 0) {
                            $scope.status.waitForFactoryProductionConlusionSummary.totalPendingItem = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.totalPendingItem, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.overLDS = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.overLDS, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.lds = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.lds, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.oneToTwoDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.oneToTwoDays, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.threeToFourDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.threeToFourDays, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.fiveToSixDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.fiveToSixDays, 0);
                            $scope.status.waitForFactoryProductionConlusionSummary.moreThanSixDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.moreThanSixDays, 0);
                        }          
                    }

                    // show content and trigger full screen mode in the first load
                    if (!$scope.isFirstLoadProcessed) {
                        $scope.isFirstLoadProcessed = true;
                        jQuery('#content').show();
                        $('#pnlSearchResult').find('.jarviswidget-fullscreen-btn').click();
                    }  

                    if ($scope.pageIndex === 1) {
                        $('#pnlSearchResult').find('.tilsoft-table-wrapper').scrollTop(0);
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function reload() {
            $scope.status.isLoaded = false;
            $scope.data = [];
            $scope.pageIndex = 1;

            factoryQuotation2MngService.searchFilter.pageIndex = 1;
            if ($scope.filters.IsDeadLine !== null) {
                $scope.filters.ItemTypeID = 5;
                $scope.filters.StatusID = 1;
                $scope.filters.QuotationOfferDirectionID = 2;
            } 

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                Season: jsHelper.getCurrentSeason(),
                ClientNM: '',
                Description: '',
                FactoryUD: '',
                ItemTypeID: '',
                StatusID: '',
                QuotationOfferDirectionID: '',
                ProformaInvoiceNo: '',
                BusinessDataStatusID: null,
                UserID: context.userID,
                IsDeadLine: null
            };
            $scope.event.reload();
        };
        $timeout(function () { $scope.event.init(); }, 0);   
    };
})();
